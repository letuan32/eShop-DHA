using eShop_DHA.Settings;
using Newtonsoft.Json;

namespace eShop_DHA.Salesforce;

public class AuthenProvider : IAuthenProvider
{
    private AuthenResponse? _oauthResponse;
    private SalesforceSetting _settings = new SalesforceSetting();
    internal static readonly TimeSpan MaxTokenLifetime = TimeSpan.FromMinutes(30);

    public AuthenProvider(IConfiguration configuration)
    {
        configuration.GetSection(SalesforceSetting.Section).Bind(_settings);
    }

    public async Task<AuthenResponse> Authen()
    {
        if (_oauthResponse is null || ShouldRefresh(_oauthResponse))
        {
            using (var client = new HttpClient())
            {
                HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", _settings.GrantType},
                    {"client_id", _settings.ClientId},
                    {"client_secret", _settings.ClientSecret},
                    {"username", _settings.Username},
                    {"password", _settings.Password}
                });
                HttpResponseMessage message = await client.PostAsync(_settings.LoginEndpoint, content);
                var response = message.Content.ReadAsStringAsync().Result;
                _oauthResponse = JsonConvert.DeserializeObject<AuthenResponse>(response);
            }
        }
        return _oauthResponse;


    }
    private bool ShouldRefresh(AuthenResponse oauthResponse)
    {
        if (string.IsNullOrWhiteSpace(oauthResponse.IssuedAt) ||
            !long.TryParse(oauthResponse.IssuedAt, out var issueAtMillisecs))
        {
            return true;
        }

        var now = DateTime.UtcNow;
        var issue = DateTimeOffset.FromUnixTimeMilliseconds(issueAtMillisecs).DateTime;
        var liveTime = now.Subtract(issue);
        return liveTime >= MaxTokenLifetime;
    }
}
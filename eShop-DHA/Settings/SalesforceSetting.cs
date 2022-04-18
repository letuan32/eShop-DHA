namespace eShop_DHA.Settings;

public class SalesforceSetting
{
    public const string Section = "Salesforce";
    public Uri Endpoint { get; set; } = default!;

    public string ApiVersion { get; set; } = default!;
    public string LoginEndpoint { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string GrantType { get; set; } = string.Empty;
}
using Newtonsoft.Json;

namespace eShop_DHA.Salesforce;

public class AuthenResponse
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("instance_url")]
    public Uri? InstanceUrl { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("token_type")]
    public string? TokenType { get; set; }

    [JsonProperty("issued_at")]
    public string? IssuedAt { get; set; }

    [JsonProperty("signature")]
    public string? Signature { get; set; }
}
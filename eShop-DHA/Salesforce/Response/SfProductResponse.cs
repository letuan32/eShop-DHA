using Newtonsoft.Json;

namespace eShop_DHA.Salesforce.Response;

public class SfProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    [JsonProperty("price__c")]
    public decimal Price { get; set; }
    [JsonProperty("categoryId__r")]
    public IncludeCategory Category { get; set; }
}

public class IncludeCategory
{
    public string Name { get; set; }
}
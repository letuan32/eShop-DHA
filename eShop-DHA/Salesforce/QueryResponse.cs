using Newtonsoft.Json;

namespace eShop_DHA.Salesforce;

public class QueryResponse<T> where T : class
{
    [JsonProperty("records")]
    public IList<T> Records { get; set; } = new List<T>();
}
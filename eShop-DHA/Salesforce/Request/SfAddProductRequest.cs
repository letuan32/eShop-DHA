namespace eShop_DHA.Salesforce.Request;

public class SfAddProductRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string CategoryId { get; set; }
}
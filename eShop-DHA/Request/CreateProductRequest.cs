namespace eShop_DHA.Request;

public class CreateProductRequest
{
    public string? Name { get; set; }
    public string? CategoryId { get; set; }
    
    public double Price { get; set; }
}
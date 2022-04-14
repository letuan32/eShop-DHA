namespace eShop_DHA.Request;

public class UpdateProductRequest
{
    public int Id { get; set; }   
    public string? Name { get; set; }
    public string? CategoryId { get; set; }
}
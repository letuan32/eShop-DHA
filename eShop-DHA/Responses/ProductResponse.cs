namespace eShop_DHA.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public string SfId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

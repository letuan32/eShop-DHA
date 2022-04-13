namespace eShop_DHA.Responses;

public class CategoryResponse
{
    public int Id { get; set; }
    public string ExternalId { get; set; }
    public string SfId { get; set; }
    public string Name { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
using eShop_DHA.Entities;
using eShop_DHA.Queries;
using eShop_DHA.Request;
using eShop_DHA.Responses;

namespace eShop_DHA.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetProductsAsync(ProductQuery query);
    Task<int> AddAsync(CreateProductRequest request);
    Task<int> UpdateAsync(UpdateProductRequest request);
    Task<int> DeleteAsync(Product product);

    Task<Product?> FindById(int id);
}
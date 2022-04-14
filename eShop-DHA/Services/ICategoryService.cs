using eShop_DHA.Entities;
using eShop_DHA.Queries;
using eShop_DHA.Request;
using eShop_DHA.Responses;

namespace eShop_DHA.Services;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetCategoriesAsync(CategoryQuery query);
    Task<int> AddAsync(CreateCategoryRequest request);
    Task<int> UpdateAsync(UpdateCategoryRequest request);
    Task<int> DeleteAsync(int id);
    
    
}



using eShop_DHA.Data;
using eShop_DHA.Entities;
using eShop_DHA.Queries;
using eShop_DHA.Request;
using eShop_DHA.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop_DHA.Services;

public class CategoryService:ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryResponse>> GetCategoriesAsync(CategoryQuery query)
    {
        IQueryable<Category> queryable = _context.Category;
        if (!string.IsNullOrEmpty(query.Name))
        {
            queryable = queryable
                .Where(x => EF.Functions.Like(x.Name, $"%{query.Name}%"));
        }

        var categoriesResponse = await queryable
            .Select(x => new CategoryResponse()
            {
                Id = x.Id,
                ExternalId = x.ExternalId,
                SfId = x.SfId,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate
            }).ToListAsync();

        return categoriesResponse;
    }

    public async Task<int> AddAsync(CreateCategoryRequest request)
    {
        var category = new Category()
        {
            ExternalId = Guid.NewGuid().ToString(),
            Name = request.Name,
            CreatedDate = DateTime.Now
        };
        await _context.Category.AddAsync(category);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(UpdateCategoryRequest request)
    {
        var category = await _context.Category.FindAsync(request.Id);
        if (category != null)
        {
            category.Name = request.Name;
            return await _context.SaveChangesAsync();
        }

        return 0;
    }

    public async Task<int> DeleteAsync(int id)
    {
       
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return 0;
            }
            _context.Category.Remove(category);
            return await _context.SaveChangesAsync();




    }
}
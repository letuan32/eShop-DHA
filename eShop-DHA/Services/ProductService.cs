using eShop_DHA.Data;
using eShop_DHA.Entities;
using eShop_DHA.Queries;
using eShop_DHA.Request;
using eShop_DHA.Responses;
using Microsoft.EntityFrameworkCore;

namespace eShop_DHA.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponse>> GetProductsAsync(ProductQuery query)
    {
        var queryable = _context.Product.AsQueryable();

        if (!string.IsNullOrEmpty(query.CategoryId))
            queryable = queryable.Where(p => p.CategoryExternalId == query.CategoryId);

        if (!string.IsNullOrEmpty(query.Name))
            queryable = queryable
                .Where(x => EF.Functions.Like(x.Name, $"%{query.Name}%"));

        return await queryable
            .Include(p => p.Category)
            .AsSplitQuery()
            .AsNoTracking()
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                ExternalId = p.ExternalId,
                SfId = p.SfId,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate
            }).ToListAsync();
    }

    public async Task<int> AddAsync(CreateProductRequest request)
    {
        var categoryInDb = await _context.Category.FirstOrDefaultAsync(x => x.ExternalId == request.CategoryId);
        if (categoryInDb == null)
        {
            throw new KeyNotFoundException();
        }
        var product = new Product
        {
            ExternalId = Guid.NewGuid().ToString(),
            Name = request.Name,
            CategoryExternalId = request.CategoryId,
            Price = request.Price
        };
        await _context.Product.AddAsync(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(UpdateProductRequest request)
    {
        var existedProduct = await _context.Product.FindAsync(request.Id);
        var categoryInDb = await _context.Category.FirstOrDefaultAsync(x => x.ExternalId == request.CategoryId);
        if (categoryInDb == null)
        {
            throw new KeyNotFoundException();
        }
        if (existedProduct != null)
        {
            existedProduct.Name = request.Name;
            existedProduct.CategoryExternalId = request.CategoryId;
            existedProduct.Price = request.Price;
            return await _context.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<int> DeleteAsync(Product product)
    {
        _context.Product.Remove(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<Product?> FindById(int id)
    {
        return await _context.Product.FindAsync(id);
    }
}
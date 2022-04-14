



using eShop_DHA.Data;
using eShop_DHA.Entities;
using eShop_DHA.Queries;
using eShop_DHA.Request;
using eShop_DHA.Responses;
using eShop_DHA.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop_DHA.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController:ControllerBase
{
    private readonly IProductService _productService;
    private readonly ApplicationDbContext _context;

    public ProductController(IProductService productService, ApplicationDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQuery query)
    {
        try
        {
            var responseDate= await _productService.GetProductsAsync(query);
            return Ok(responseDate);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        try
        {
            var isCreated = await _productService.AddAsync(request);
            if (isCreated == 1) return Ok("Created");
            return BadRequest("Create Failed");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductRequest request)
    {
        try
        {
            var isUpdated = await _productService.UpdateAsync(request);
            if (isUpdated == 1) return Ok("Updated");
            return BadRequest("Update Failed");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var existedProduct = await _productService.FindById(id);
        if (existedProduct != null)
        {
            await _productService.DeleteAsync(existedProduct);
            return Ok("Deleted");
        }

        return NotFound();
    }
}
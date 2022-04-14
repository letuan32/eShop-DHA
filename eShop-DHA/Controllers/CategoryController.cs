using eShop_DHA.Queries;
using eShop_DHA.Request;
using eShop_DHA.Responses;
using eShop_DHA.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShop_DHA.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController:ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryQuery>>> GetCategoriesAsync([FromQuery] CategoryQuery query)
    {
        var response = await _categoryService.GetCategoriesAsync(query);
        return Ok(response);
    }

    
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateCategoryRequest request)
    {
        if (!string.IsNullOrEmpty(request.Name))
        {
            var isSuccess = await _categoryService.AddAsync(request);
            if (isSuccess == 1) return Ok("Success");
        }
        return BadRequest("Invalid");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCategoryRequest request)
    {
        if (request.Id != 0)
        {
            var isSuccess = await _categoryService.UpdateAsync(request);
            if (isSuccess == 1) return Ok("Success");
        }
        return BadRequest("Error");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        
        var isDeleted = await _categoryService.DeleteAsync(id);
        if (isDeleted == 0)
        {
            return BadRequest("Error");
        }

        return Ok("Success");
    }

}
using DAL.ODS.Models.Products;
using Microsoft.AspNetCore.Mvc;
using SLL.ODS.DTOs;
using SLL.ODS.Interfaces;

namespace PLL.API.ODS.Controllers
{
    public class CategoriesController : BaseController
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryClass>>> GetAll()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<CategoryClass>>> GetActive()
        {
            return Ok(await _categoryService.GetActiveCategoriesAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryClass>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        //[HttpGet("{id:int}/products")]
        //public async Task<ActionResult<CategoryClass>> GetWithProducts(int id)
        //{
        //    var category = await _categoryService.GetCategoryWithProductsAsync(id);
        //    if (category == null)
        //        return NotFound();

        //    return Ok(category);
        //}
        [HttpGet("{id:int}/products")] //added 1-3-2026
        public async Task<ActionResult<CategoryWithProductsDto>> GetWithProducts(int id)
        {
            var category = await _categoryService.GetCategoryWithProductsAsync(id);
            if (category == null)
                return NotFound();

            var result = new CategoryWithProductsDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                IsActive = category.IsActive,
                Products = category.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    IsActive = p.IsActive,
                    CategoryId = p.CategoryId,
                    CategoryName = category.Name
                }).ToList()
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<CategoryClass>> Create(CategoryClass category)
        {
            if (await _categoryService.CategoryNameExistsAsync(category.Name))
                return BadRequest("Category name already exists.");

            var created = await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CategoryClass category)
        {
            if (id != category.Id)
                return BadRequest();

            if (!await _categoryService.CategoryExistsAsync(id))
                return NotFound();

            await _categoryService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _categoryService.CategoryExistsAsync(id))
                return NotFound();

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }

    }
}

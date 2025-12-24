using DAL.ODS.Models.Products;
using Microsoft.AspNetCore.Mvc;
using SLL.ODS.Interfaces;

namespace PLL.API.ODS.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductClass>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ProductClass>>> GetActive()
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductClass>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/Products/category/5
        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ProductClass>>> GetByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        // GET: api/Products/search?term=phone
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductClass>>> Search([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required.");

            var products = await _productService.SearchProductsAsync(term);
            return Ok(products);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductClass>> Create(ProductClass product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.Id },
                createdProduct
            );
        }

        // PUT: api/Products/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductClass product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch.");

            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }




    }
}

using DAL.ODS.Interfaces;
using DAL.ODS.Models.Products;
using SLL.ODS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductClass>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllWithCategoryAsync();
        }


        public async Task<IEnumerable<ProductClass>> GetActiveProductsAsync()
        {
            return await _productRepository.GetActiveProductsAsync();
        }

        public async Task<ProductClass?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdWithCategoryAsync(id);
        }


        public async Task<IEnumerable<ProductClass>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetByCategoryIdAsync(categoryId);
        }


        public async Task<IEnumerable<ProductClass>> SearchProductsAsync(string searchTerm)
        {
            return await _productRepository.SearchByNameAsync(searchTerm);
        }


        public async Task<ProductClass> CreateProductAsync(ProductClass product)
        {

            product.CreatedAt = DateTime.Now;
            return await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductClass product)
        {
            await _productRepository.UpdateAsync(product);
        }


        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _productRepository.ExistsAsync(p => p.Id == id);
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _productRepository.CountAsync();
        }
    }
}

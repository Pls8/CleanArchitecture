using DAL.ODS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductClass>> GetAllProductsAsync();
        Task<IEnumerable<ProductClass>> GetActiveProductsAsync();
        Task<ProductClass?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductClass>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductClass>> SearchProductsAsync(string searchTerm);


        Task<ProductClass> CreateProductAsync(ProductClass product);
        Task UpdateProductAsync(ProductClass product);
        Task DeleteProductAsync(int id);


        Task<bool> ProductExistsAsync(int id);
        Task<int> GetProductsCountAsync();
    }
}

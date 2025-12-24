using DAL.ODS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Interfaces
{
    public interface IProductRepository : IGenericRepo<ProductClass>
    {
        Task<IEnumerable<ProductClass>> GetAllWithCategoryAsync();

        Task<ProductClass?> GetByIdWithCategoryAsync(int id);

        Task<IEnumerable<ProductClass>> GetByCategoryIdAsync(int categoryId);

        Task<IEnumerable<ProductClass>> GetActiveProductsAsync();

        Task<IEnumerable<ProductClass>> SearchByNameAsync(string searchTerm);
    }
}

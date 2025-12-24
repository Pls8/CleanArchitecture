using Domain.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.BLL.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductClass> GetByIdAsync(int id);
        Task<IEnumerable<ProductClass>> GetAllAsync();
        Task<IEnumerable<ProductClass>> GetFeaturedProductsAsync();
        Task<IEnumerable<ProductClass>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductClass>> SearchAsync(string searchTerm);
        Task AddAsync(ProductClass product);
        Task UpdateAsync(ProductClass product);
        Task DeleteAsync(int id);
    }
}

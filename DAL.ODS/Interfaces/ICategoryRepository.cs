using DAL.ODS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Interfaces
{
    public interface ICategoryRepository : IGenericRepo<CategoryClass>
    {
        Task<CategoryClass?> GetByIdWithProductsAsync(int id);

        Task<IEnumerable<CategoryClass>> GetAllWithProductCountAsync();

        Task<IEnumerable<CategoryClass>> GetActiveCategoriesAsync();

        Task<bool> ExistsByNameAsync(string name);
    }
}

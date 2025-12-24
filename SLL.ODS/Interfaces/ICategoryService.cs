using DAL.ODS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryClass>> GetAllCategoriesAsync();
        Task<IEnumerable<CategoryClass>> GetActiveCategoriesAsync();
        Task<CategoryClass?> GetCategoryByIdAsync(int id);
        Task<CategoryClass?> GetCategoryWithProductsAsync(int id);


        Task<CategoryClass> CreateCategoryAsync(CategoryClass category);
        Task UpdateCategoryAsync(CategoryClass category);
        Task DeleteCategoryAsync(int id);

        
        Task<bool> CategoryExistsAsync(int id);
        Task<bool> CategoryNameExistsAsync(string name);
        Task<int> GetCategoriesCountAsync();
    }
}

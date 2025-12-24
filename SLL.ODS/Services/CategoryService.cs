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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryClass>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CategoryClass>> GetActiveCategoriesAsync()
        {
            return await _categoryRepository.GetActiveCategoriesAsync();
        }

        public async Task<CategoryClass?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<CategoryClass?> GetCategoryWithProductsAsync(int id)
        {
            return await _categoryRepository.GetByIdWithProductsAsync(id);
        }

        public async Task<CategoryClass> CreateCategoryAsync(CategoryClass category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryClass category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.ExistsAsync(c => c.Id == id);
        }

        public async Task<bool> CategoryNameExistsAsync(string name)
        {
            return await _categoryRepository.ExistsByNameAsync(name);
        }

        public async Task<int> GetCategoriesCountAsync()
        {
            return await _categoryRepository.CountAsync();
        }
    }
}

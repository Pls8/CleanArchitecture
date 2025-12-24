using BLL.ODS.Context;
using DAL.ODS.Interfaces;
using DAL.ODS.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Repositories
{
    public class CategoryRepository : GenericRepo<CategoryClass>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<CategoryClass>> GetActiveCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryClass>> GetAllWithProductCountAsync()
        {
            return await _dbSet
            .Include(c => c.Products)
            .ToListAsync();
        }

        public async Task<CategoryClass?> GetByIdWithProductsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

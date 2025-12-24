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
    public class ProductRepository : GenericRepo<ProductClass>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductClass>> GetAllWithCategoryAsync()
        {
            return await _dbSet
                .Include(p => p.Category) 
                .ToListAsync();
        }

        public async Task<ProductClass?> GetByIdWithCategoryAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductClass>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductClass>> GetActiveProductsAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductClass>> SearchByNameAsync(string searchTerm)
        {
            return await _dbSet
                .Where(p => p.Name.Contains(searchTerm))
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}

using BLL.ODS.Context;
using DAL.ODS.Interfaces;
using DAL.ODS.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Repositories
{
    public class OrderRepository : GenericRepo<OrderClass>, IOrderRepository
    {

        public OrderRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<OrderClass?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(o => o.AppUser) 
                .Include(o => o.OrderItems) 
                    .ThenInclude(oi => oi.Product) 
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<OrderClass>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(o => o.AppUser)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate) 
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderClass>> GetByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(o => o.AppUserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderClass>> GetByStatusAsync(OrderStatusEnums status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .Include(o => o.AppUser)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(int orderId, OrderStatusEnums newStatus)
        {
            var order = await GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                await UpdateAsync(order);
            }
        }
    }
}

using DAL.ODS.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Interfaces
{
    public interface IOrderRepository : IGenericRepo<OrderClass>
    {
        Task<IEnumerable<OrderClass>> GetAllWithDetailsAsync();
        Task<OrderClass?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<OrderClass>> GetByStatusAsync(OrderStatusEnums status);
        Task<IEnumerable<OrderClass>> GetByUserIdAsync(string userId);
        Task UpdateStatusAsync(int orderId, OrderStatusEnums status);
    }
}

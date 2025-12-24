using DAL.ODS.Models.Order;
using SLL.ODS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderClass>> GetAllOrdersAsync();
        Task<OrderClass?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderClass>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<OrderClass>> GetOrdersByStatusAsync(OrderStatusEnums status);

        Task<OrderClass> CreateOrderAsync(OrderClass order);
        Task UpdateOrderStatusAsync(int orderId, OrderStatusEnums newStatus);
        Task CancelOrderAsync(int orderId);

        Task<bool> OrderExistsAsync(int id);
        Task<int> GetOrdersCountAsync();
        Task<int> GetPendingOrdersCountAsync();

        // These methods now use SLL.ODS.Models types
        Task<int> GetTotalOrdersCountAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<List<RecentOrderViewModel>> GetRecentOrdersAsync(int count = 5);
    }
}

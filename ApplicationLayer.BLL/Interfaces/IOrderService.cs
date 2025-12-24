using Domain.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderClass> CreateOrderAsync(string userId, List<CartItemClass> cartItems, string shippingAddress);
        Task<bool> CancelOrderAsync(int orderId);
        Task<OrderClass> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<OrderClass>> GetUserOrdersAsync(string userId);
    }
}

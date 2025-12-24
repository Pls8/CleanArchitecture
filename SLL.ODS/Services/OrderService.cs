using DAL.ODS.Interfaces;
using DAL.ODS.Models.Order;
using SLL.ODS.Interfaces;
using SLL.ODS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.ODS.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepo<OrderClass> _orderRepo;
        public OrderService(IOrderRepository orderRepository, IGenericRepo<OrderClass> orderRepo)
        {
            _orderRepository = orderRepository;
            _orderRepo = orderRepo;
        }


        public async Task CancelOrderAsync(int orderId)
        {
            await _orderRepository.UpdateStatusAsync(orderId, OrderStatusEnums.Cancelled);
        }

        public async Task<OrderClass> CreateOrderAsync(OrderClass order)
        {
            order.OrderDate = DateTime.Now;
            order.Status = OrderStatusEnums.Pending;

            order.SubTotal = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
            order.TotalAmount = order.SubTotal - order.Discount;

            await _orderRepository.AddAsync(order);

            return order;
        }

        public async Task<IEnumerable<OrderClass>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllWithDetailsAsync();
        }

        public async Task<OrderClass?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<IEnumerable<OrderClass>> GetOrdersByStatusAsync(OrderStatusEnums status)
        {
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<OrderClass>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetByUserIdAsync(userId);
        }

        public async Task<int> GetOrdersCountAsync()
        {
            return await _orderRepository.CountAsync();
        }

        public async Task<int> GetPendingOrdersCountAsync()
        {
            var pendingOrders = await _orderRepository.GetByStatusAsync(OrderStatusEnums.Pending);
            return pendingOrders.Count();
        }

        public async Task<bool> OrderExistsAsync(int id)
        {
            return await _orderRepository.ExistsAsync(o => o.Id == id);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatusEnums newStatus)
        {
            await _orderRepository.UpdateStatusAsync(orderId, newStatus);
        }




        public async Task<int> GetTotalOrdersCountAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return orders.Count();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return orders.Sum(o => o.TotalAmount);
        }

        public async Task<List<RecentOrderViewModel>> GetRecentOrdersAsync(int count = 5)
        {
            var orders = await _orderRepo.GetAllAsync();

            var recentOrders = orders
                .OrderByDescending(o => o.OrderDate)
                .Take(count)
                .ToList();

            return recentOrders.Select(o => new RecentOrderViewModel
            {
                Id = o.Id,
                CustomerName = GetCustomerName(o),
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                OrderDate = o.OrderDate
            }).ToList();
        }

        private string GetCustomerName(OrderClass order)
        {
            if (order.AppUser != null)
            {
                return $"{order.AppUser.FirstName} {order.AppUser.LastName}";
            }
            return "Customer";
        }
    }
}

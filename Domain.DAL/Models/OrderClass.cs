using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class OrderClass
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = GenerateOrderNumber();
        public DateTime OrderDate { get; set; }
        public OrderStatusEnums Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? Notes { get; set; }

        // Navigation
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUserClass User { get; set; }
        public ICollection<OrderItemClass> OrderItems { get; set; } = new List<OrderItemClass>();


        private static string GenerateOrderNumber()
        {
            return "ORD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}

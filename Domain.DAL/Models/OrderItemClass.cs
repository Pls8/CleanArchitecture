using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class OrderItemClass
    {
        public int Id { get; set; }

        // Properties (Snapshot at time of purchase)
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }  // Price at purchase time
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }


        // Navigation Properties
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public OrderClass Order { get; set; }



        [ForeignKey(nameof(Order))]
        public int ProductId { get; set; }
        public ProductClass Product { get; set; }
    }
}

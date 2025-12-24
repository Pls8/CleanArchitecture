using DAL.ODS.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Order
{
    public class OrderItemClass : BaseEntity
    {
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }


        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public virtual OrderClass? Order { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual ProductClass? Product { get; set; }


        [NotMapped] 
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}

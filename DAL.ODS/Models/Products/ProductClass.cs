using DAL.ODS.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Products
{
    public class ProductClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int NoInStock { get; set; }
        public int NoOfOrders { get; set; }
        public int NumberOfRefund { get; set; }

        public decimal Price { get; set; }
        public bool HasDiscount { get; set; }
        public DisCountEnum DiscountType { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public CategoryClass? Category { get; set; }
    }
}

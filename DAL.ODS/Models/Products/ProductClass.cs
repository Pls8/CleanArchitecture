using DAL.ODS.Models.Order;
using DAL.ODS.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Products
{
    public class ProductClass : BaseEntity
    {
        [Required(ErrorMessage = "Name")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;

        public string? ImageUrl { get; set; }


        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual CategoryClass? Category { get; set; }

        public virtual ICollection<OrderItemClass> OrderItems { get; set; } = new List<OrderItemClass>();
    }
}

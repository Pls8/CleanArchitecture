using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class ProductClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Rating { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign Key
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public CategoryClass Category { get; set; }


        public ICollection<ProductImageClass> ProductImages { get; set; } = new List<ProductImageClass>();
        public ICollection<OrderItemClass> OrderItems { get; set; } = new List<OrderItemClass>();
        public ICollection<CartItemClass> CartItems { get; set; } = new List<CartItemClass>();
        public ICollection<WishlistItemClass> WishlistItems { get; set; } = new List<WishlistItemClass>();
        public ICollection<ReviewClass> Reviews { get; set; }   = new List<ReviewClass>();
    }
}

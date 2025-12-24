using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class ProductImageClass
    {
        public int Id { get; set; }

        // Properties
        public string? ImageUrl { get; set; }
        public int DisplayOrder { get; set; } = 0;  // For ordering images

        // Navigation Property
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ProductClass Product { get; set; }
    }
}

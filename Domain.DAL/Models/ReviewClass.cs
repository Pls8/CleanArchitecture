using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class ReviewClass
    {
        public int Id { get; set; }

        // Properties
        public int Rating { get; set; }  // 1-5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUserClass User { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ProductClass Product { get; set; }
    }
}

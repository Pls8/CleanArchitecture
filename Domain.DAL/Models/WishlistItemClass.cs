using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class WishlistItemClass
    {
        public int Id { get; set; }
        // Properties
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUserClass User { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ProductClass Product { get; set; }


    }
}

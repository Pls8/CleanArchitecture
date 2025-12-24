using DAL.ODS.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Order
{
    public class OrderClass : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatusEnums Status { get; set; } = OrderStatusEnums.Pending;


        [Required(ErrorMessage = "Address")]
        [MaxLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "city")]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "phone")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }


        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; } = string.Empty;

        public virtual AppUserClass? AppUser { get; set; }


        public virtual ICollection<OrderItemClass> OrderItems { get; set; } = new List<OrderItemClass>();
    
}
}

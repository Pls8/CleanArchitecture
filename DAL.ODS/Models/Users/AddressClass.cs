using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Users
{
    public class AddressClass : BaseEntity //weak entity
    {
        [Required(ErrorMessage = "Address")]
        [MaxLength(200)]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City")]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string Country { get; set; } = "Oman";



        public bool IsDefault { get; set; } = false;

        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; } = string.Empty;

        public virtual AppUserClass? AppUser { get; set; }
    }
}

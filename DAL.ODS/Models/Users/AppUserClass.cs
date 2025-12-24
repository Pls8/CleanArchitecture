using DAL.ODS.Models.Order;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Users
{
    public class AppUserClass : IdentityUser
    {
        [Required(ErrorMessage = "Name")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<AddressClass> Addresses { get; set; } = new List<AddressClass>();

        public virtual ICollection<OrderClass> Orders { get; set; } = new List<OrderClass>();
    }
}

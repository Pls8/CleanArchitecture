using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Models
{
    public class AppUserClass : IdentityUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; }


        // Navigation
        public ICollection<OrderClass> Orders { get; set; } = new List<OrderClass>();
        public ICollection<CartItemClass> CartItems { get; set; } = new List<CartItemClass>();
        public ICollection<WishlistItemClass> WishlistItems { get; set; } = new List<WishlistItemClass>();
        public ICollection<ReviewClass> Reviews { get; set; } = new List<ReviewClass>();
    }
}

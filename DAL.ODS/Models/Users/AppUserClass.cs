using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Users
{
    public class AppUserClass : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int NumberOfOrders { get; set; }
        public DateOnly DOB { get; set; }

        public ICollection<AddressClass> Addresses { get; set; }
    }
}

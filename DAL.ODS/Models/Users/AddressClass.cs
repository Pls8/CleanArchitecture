using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Users
{
    public class AddressClass //weak entity
    {
        public int Id { get; set; }
        //Primary Key string cuz of relation with AppUser for concatenation 
        //(AppUser.Id + AddressId) to make it unique
        //Composite key must be configured via Fluent API, not attributes.


        public string Street { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string AddressId { get; set; }
        public AppUserClass AppUser { get; set; }
    }
}

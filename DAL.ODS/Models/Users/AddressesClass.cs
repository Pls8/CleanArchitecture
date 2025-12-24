using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Users
{
    public class AddressesClass
    {
        public string Id { get; set; }

        [ForeignKey(nameof(appUser))]
        public string AppUserId { get; set; }
        public AppUserClass appUser { get; set; }
    }
}

using DAL.ODS.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Config
{
    internal class AddressConfig : IEntityTypeConfiguration<AddressClass>
    {           //(Composite Key)


        public void Configure(EntityTypeBuilder<AddressClass> builder)
        {                           //  string ID | int
            builder
            .HasOne(a => a.AppUser)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

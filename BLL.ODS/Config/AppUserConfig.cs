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
    public class AppUserConfig : IEntityTypeConfiguration<AppUserClass>
    {
        public void Configure(EntityTypeBuilder<AppUserClass> builder)
        {
            builder.Property(u => u.FirstName)
               .IsRequired();

            builder.Property(u => u.LastName)
                   .IsRequired();
        }
    }
}

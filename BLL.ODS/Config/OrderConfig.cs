using DAL.ODS.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Config
{
    public class OrderConfig : IEntityTypeConfiguration<OrderClass>
    {
        public void Configure(EntityTypeBuilder<OrderClass> builder)
        {
            builder
                .HasOne(o => o.AppUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقة بين Order و OrderItem
            builder
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

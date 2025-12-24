using DAL.ODS.Models.Products;
using DAL.ODS.Models.Products.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Config
{
    public class ProductConfig : IEntityTypeConfiguration<ProductClass>
    {
        public void Configure(EntityTypeBuilder<ProductClass> builder)
        {
            builder
           .HasOne(p => p.Category)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.CategoryId)
           .OnDelete(DeleteBehavior.Restrict);

            // العلاقة بين Product و OrderItem
            builder
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index على اسم المنتج
            builder.HasIndex(p => p.Name);




            //// Seed Data (optional)
            //builder.HasData(
            //    new ProductClass
            //    {
            //        Id = 1,
            //        Name = "Laptop",
            //        Price = 1200,
            //        NoInStock = 10,
            //        HasDiscount = false,
            //        DiscountType = DisCountEnum.FixedAmount,
            //        CategoryId = 1,
            //        CreatedAt = DateTime.UtcNow
            //    }
            //);

        }
    }
}

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
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DiscountType)
                   .HasConversion<int>();




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

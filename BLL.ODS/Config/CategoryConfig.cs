using DAL.ODS.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<CategoryClass>
    {
        public void Configure(EntityTypeBuilder<CategoryClass> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired();

            builder.Property(c => c.Description);

            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);



            //// Seed Data (optional)
            //builder.HasData(
            //    new CategoryClass { Id = 1, Name = "Electronics", Description = "Electronic items" },
            //    new CategoryClass { Id = 2, Name = "Books", Description = "Books & magazines" }
            //);

        }
    }
}

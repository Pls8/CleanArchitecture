using Domain.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUserClass>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Product Configuration
            modelBuilder.Entity<ProductClass>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.DiscountPrice).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Rating).HasColumnType("decimal(3,2)");
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Order Configuration
            modelBuilder.Entity<OrderClass>(entity =>
            {
                entity.Property(o => o.OrderNumber).HasMaxLength(20).IsRequired();
                entity.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
                entity.Property(o => o.ShippingCost).HasColumnType("decimal(18,2)");
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(o => o.OrderDate).HasDefaultValueSql("GETUTCDATE()");
            });

            // Category Configuration
            modelBuilder.Entity<CategoryClass>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }




        public DbSet<ProductClass> Products { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }
        public DbSet<OrderClass> Orders { get; set; }
        public DbSet<OrderItemClass> OrderItems { get; set; }
        public DbSet<CartItemClass> CartItems { get; set; }
        public DbSet<WishlistItemClass> WishlistItems { get; set; }
        public DbSet<ReviewClass> Reviews { get; set; }
        public DbSet<ProductImageClass> ProductImages { get; set; }
    }
}

using DAL.ODS.Models.Order;
using DAL.ODS.Models.Products;
using DAL.ODS.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ODS.Context
{
    public class AppDbContext : IdentityDbContext<AppUserClass>
    {                   // ctrl + . to implement constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Config

            #region Config
            //builder.Entity<ProductClass>()
            //   .HasOne(p => p.Category)
            //   .WithMany(c => c.Products)
            //   .HasForeignKey(p => p.CategoryId)
            //   .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<OrderClass>()
            //    .HasOne(o => o.AppUser)
            //    .WithMany(u => u.Orders)
            //    .HasForeignKey(o => o.AppUserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<OrderItemClass>()
            //    .HasOne(oi => oi.Order)
            //    .WithMany(o => o.OrderItems)
            //    .HasForeignKey(oi => oi.OrderId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<OrderItemClass>()
            //    .HasOne(oi => oi.Product)
            //    .WithMany(p => p.OrderItems)
            //    .HasForeignKey(oi => oi.ProductId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<AddressClass>()
            //    .HasOne(a => a.AppUser)
            //    .WithMany(u => u.Addresses)
            //    .HasForeignKey(a => a.AppUserId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<ProductClass>()
            //    .HasIndex(p => p.Name);

            //builder.Entity<CategoryClass>()
            //    .HasIndex(c => c.Name)
            //    .IsUnique();
            #endregion
        }

        public DbSet<ProductClass> products { get; set; }
        public DbSet<CategoryClass> categories { get; set; }
        public DbSet<OrderClass> Orders { get; set; }
        public DbSet<OrderItemClass> OrderItems { get; set; }
        public DbSet<AddressClass> Addresses { get; set; }

    }
}

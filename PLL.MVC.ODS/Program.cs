using BLL.ODS.Context;
using BLL.ODS.Repositories;
using DAL.ODS.Interfaces;
using DAL.ODS.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SLL.ODS.Interfaces;
using SLL.ODS.Services;

namespace PLL.MVC.ODS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //PLL.MVC ? Startup project
        //BLL ? Default project for migrations
        // Database Context
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        // Add Identity
        builder.Services.AddIdentity<AppUserClass, IdentityRole>(options => {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 4;

        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



        // Cookie Configuration
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
        });



        // 1. Register Generic Repository
        builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

        // 2. Register specific repositories you HAVE
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        // 3. Register services you HAVE
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IOrderService, OrderService>(); 




        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Ensure required roles exist and optionally seed an initial admin user
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUserClass>>();

            var roles = new[] { "Admin", "Customer" };

            foreach (var role in roles)
            {
                var exists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if (!exists)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }

            // Optional admin seed - credentials can be overridden via configuration (see sample below)
            var adminEmail = builder.Configuration["AdminUser:Email"] ?? "admin@omandigitalshop.local";
            var adminPassword = builder.Configuration["AdminUser:Password"] ?? "Admin123!"; // must meet Identity password policy

            var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (admin == null)
            {
                var adminUser = new AppUserClass
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now
                };

                var createResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (createResult.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                }
                else
                {
                    // Log creation errors to console for debugging (replace with logger if preferred)
                    foreach (var err in createResult.Errors)
                    {
                        Console.WriteLine($"Admin seed error: {err.Code} - {err.Description}");
                    }
                }
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // Add area routing so URLs with an area resolve to /{area}/{controller}/{action}
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

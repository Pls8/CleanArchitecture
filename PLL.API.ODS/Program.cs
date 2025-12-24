
using BLL.ODS.Context;
using BLL.ODS.Repositories;
using DAL.ODS.Interfaces;
using DAL.ODS.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SLL.ODS.Interfaces;
using SLL.ODS.Services;


namespace PLL.API.ODS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        //___________________________________________________________________________________________
        builder.Services.AddDbContext<AppDbContext>(options =>                                       //
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));    //
                                                                                                     //
                                                                                                     //
        builder.Services.AddIdentity<AppUserClass, IdentityRole>(options =>                          //
        {                                                                                            //
            options.Password.RequireDigit = true;                                                    //
            options.Password.RequireUppercase = true;                                                //
            options.Password.RequiredLength = 6;                                                     //
            options.Password.RequireNonAlphanumeric = false;                                         //
                                                                                                     //
            options.User.RequireUniqueEmail = true;                                                  //
                                                                                                     //
        })                                                                                           //
        .AddEntityFrameworkStores<AppDbContext>()                                                    //
        .AddDefaultTokenProviders();                                                                 //
                                                                                                     //
                                                                                                     //
        //AddScoped is used to register services for Dependency Injection (DI)                       //
        //in an ASP.NET Core API, and it controls how long an instance of a service lives.           //
        builder.Services.AddScoped<IProductRepository, ProductRepository>();                         //
        builder.Services.AddScoped<IProductService, ProductService>();                               //
        //Lifetime	        Description	                    Typical Use                              //
        //AddTransient	    New instance every time	        Lightweight, stateless services          //
        //AddScoped	One     instance per request	        Repositories, business services          //
        //AddSingleton	    One instance for app lifetime	Caching, configuration                   //
        //-------------------------------------------------------------------------------------------//



        //builder.Services.AddFluentValidationAutoValidation();
        //builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();



        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

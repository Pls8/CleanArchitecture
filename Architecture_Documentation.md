# .NET Clean Architecture Documentation - Oman Digital Shop Platform

This document provides a comprehensive overview of the Clean Architecture implementation used in the Oman Digital Shop Platform. It is designed to be used as a blueprint for generating similar .NET 8 projects using AI agents.

## 🏗️ Architectural Overview

The solution follows a structured Clean Architecture pattern, ensuring separation of concerns, maintainability, and testability. It is divided into five main projects:

1.  **DAL (Data Access Layer)**: Domain Entities, Repository Interfaces, and Base Models.
2.  **BLL (Business Logic Layer)**: Infrastructure/Persistence, DbContext, Concrete Repositories, and Migrations.
3.  **SLL (Service Logic Layer)**: Application Services, Business Logic processing, and Service Interfaces.
4.  **PLL.MVC (Presentation Logic Layer)**: ASP.NET Core MVC Web Application for Frontend/Admin.
5.  **Pll.Api (Presentation Logic Layer)**: ASP.NET Core Web API for RESTful services.

---

## 📁 Folder Structure & Responsibilities

### 1. DAL.OmanDigitalShop (Domain & Abstractions)
*   **Purpose**: Contains the core domain models and repository interfaces. This layer has no dependencies on other projects.
*   **Key Folders**:
    *   `Models/`: Entity models (e.g., `Product`, `Category`, `Order`).
    *   `Models/Users/`: Identity-related models like `AppUser` (inherits from `IdentityUser`).
    *   `Interfaces/`: Repository interfaces (e.g., `IGenericRepository<T>`, `IProductRepository`).
*   **Key Files**:
    *   `BaseEntity.cs`: Abstract base class with `Id` property.

### 2. BLL.OmanDigitalShop (Infrastructure & Persistence)
*   **Purpose**: Implements the repository interfaces and manages database interactions via Entity Framework Core.
*   **Key Folders**:
    *   `Context/`: Contains `ApplicationDbContext`.
    *   `Repositories/`: Concrete implementations of repositories (e.g., `GenericRepository<T>`, `ProductRepository`).
    *   `Migrations/`: EF Core database migrations.
*   **Dependencies**: References `DAL`.

### 3. SLL.OmanDigitalShop (Application Services)
*   **Purpose**: Acts as a bridge between the Presentation layer and the Data layer. It contains the business logic.
*   **Key Folders**:
    *   `Interfaces/`: Service interfaces (e.g., `IProductService`).
    *   `Services/`: Concrete implementations (e.g., `ProductService`).
*   **Dependencies**: References `DAL` and `BLL`.

### 4. PLL.MVC (Web Presentation)
*   **Purpose**: The user-facing MVC web application.
*   **Key Folders**:
    *   `Areas/Admin/`: Admin dashboard and management controllers/views.
    *   `Controllers/`: Main application controllers (Home, Account).
    *   `ViewModels/`: Models specifically for views (Login, Register).
    *   `Views/`: Razor views.
*   **Dependencies**: References `SLL`, `BLL`, `DAL`.

### 5. Pll.Api (API Presentation)
*   **Purpose**: Provides RESTful endpoints for external consumers or mobile apps.
*   *Key Folders**:
    *   `Controllers/`: API Controllers (Auth, Products, etc.).
    *   `DTOs/`: Data Transfer Objects for API requests/responses.
*   **Dependencies**: References `SLL`, `BLL`, `DAL`.

---

## 💉 Dependency Injection & Configuration

All services are registered in `Program.cs` of the presentation projects.

### Core Service Registration
```csharp
// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));

// Identity Configuration
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Repository DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Service DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
```

### API Security (JWT)
The API project is configured with JWT Bearer authentication to secure endpoints.
```csharp
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? builder.Configuration["JWT:Secret"]))
    };
});
```

---

## 📦 Required NuGet Packages

### Data Layer (DAL & BLL)
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`

### API Layer (Pll.Api)
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Swashbuckle.AspNetCore` (Swagger)

---

## 🛠️ Key Implementation Patterns

### Generic Repository Pattern
A base repository handling standard CRUD operations to minimize code duplication.
```csharp
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public GenericRepository(ApplicationDbContext context) {
        _context = context;
        _dbSet = context.Set<T>();
    }
    // Implementation of GetAllAsync, GetByIdAsync, AddAsync, etc.
}
```

### Service Pattern
Services encapsulate business logic and use repositories to interact with data.
```csharp
public class ProductService : IProductService {
    private readonly IProductRepository _productRepo;
    public ProductService(IProductRepository productRepo) {
        _productRepo = productRepo;
    }
    // Business logic methods
}
```

### Role & Admin Seeding
Automatic creation of "Admin" and "Customer" roles, and a default admin user on application startup.

---

## 🤖 AI Prompt Template for Future Projects

Copy and paste this prompt to an AI agent to generate a project with this architecture:

> "Create a .NET 8 solution following Clean Architecture with 5 projects:
> 1. **DAL**: Contains `BaseEntity` (Id), Domain Models, and Repository Interfaces.
> 2. **BLL**: Contains `ApplicationDbContext` (EF Core), Migrations, and Concrete Repositories implementing a `GenericRepository<T>`.
> 3. **SLL**: Contains Service Interfaces and Services that wrap Repositories for business logic.
> 4. **PLL.MVC**: An ASP.NET Core MVC project with Identity, Admin Area, and Account management.
> 5. **PLL.Api**: A Web API project with JWT Authentication and Swagger.
> 
> Use SQL Server. Implement Identity with a custom `AppUser` class. Ensure Dependency Injection is correctly set up in both PLL projects. Include a Seeding mechanism for 'Admin' and 'Customer' roles.
> 
> My idea for the specific domain is: [INSERT YOUR IDEA HERE]"

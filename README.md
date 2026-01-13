![image alt](https://github.com/Pls8/CleanArchitecture/blob/master/CA_RepoCover_00000.jpg?raw=true)

# Clean Architecture Implementation 

### **Step 1: Create Blank Solution**
- **Action**: Visual Studio 2022 → New Project → Blank Solution
- **Purpose**: Foundation container for all projects

### **Step 2: Create Data Access Layer (DAL)**
- **Action**: Right-click Solution → Add → _Class Library_
- **Original Name in your steps**: DAL (data access layer)
- **Clean Architecture Naming**: **Domain Layer** (Core/Business Entities)
- **Note**: This is the innermost layer containing entities, enums, exceptions, interfaces

### **Step 3: Create Business Logic Layer (BLL)**
- **Action**: Right-click Solution → Add → _Class Library_
- **Original Name in your steps**: BLL (business logic layer)
- **Clean Architecture Naming**: **Application Layer**
- **Purpose**: Contains business rules, use cases, DTOs, interfaces
- **Note**: This layer depends on (DAL)Domain Layer

### **Step 4: Create Service/Infrastructure Layer**
- **Action**: Right-click Solution → Add → _Class Library_
- **Original Name in your steps**: SLL (service logic layer)
- **Clean Architecture Naming**: **Infrastructure Layer**
- **Purpose**: External concerns (database, email, file storage, APIs)
- The Below Not Yet!
- **Additional Layers** (optional separation):
  - **Infrastructure.Data** (Persistence)
  - **Infrastructure.Identity** (Authentication)
  - **Infrastructure.FileStorage**
  - **Payment Layer** (as you mentioned)
- **Note**: This layer depends on (BLL)Application Layer

### **Step 5: Create Presentation Layers (PLL)**
- **Action**: Right-click Solution → Add → Project
- **Options**:
  1. **ASP.NET Core Web App (Model-View-Controller)** → PLL.MVC
  2. **ASP.NET Core Web API** → PLL.API
  3. **Optional**: Mobile projects (Xamarin, MAUI, etc.)
- **Original Name**: PLL (Presentation Logic Layer)
- **Clean Architecture Naming**: **Presentation/Web Layer(s)**
- **Important**: Check ✅ "*Enlist in .NET Aspire"

### **Step 6: Setup Project References (Dependency Flow)**
**Hierarchy Direction**: **Inner → Outer** (Dependencies flow inward)

```
Domain Layer (DAL) ← Application Layer (BLL) ← Infrastructure Layer (SLL) ← Presentation Layers (PLL)
```

**Setup References**:
1. **Application Layer (BLL)** → Add Project Reference → **Domain Layer (DAL)**
2. **Infrastructure Layer (SLL)** → Add Project Reference → **Application Layer (BLL)**
3. **Presentation Layers (PLL)** → Add Project References → **Application Layer (BLL)** AND **Infrastructure Layer (SLL)**

### **Step 7: Domain Layer (DAL aka Data Access Layer) Setup**
- **Action**: Add folder `Models`
- **Subfolders**: Create per domain entity (Users, Products, Orders, etc.)
  - **Dependencies**: Add NuGet packages:
   - `Microsoft.EntityFrameworkCore`
   - `Microsoft.EntityFrameworkCore.SqlServer`
   - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`


### **Step 8: Application Layer (BLL aka Business Logic Layer) Setup**
- **Folder Structure**:
  - `Context/` (ApplicationDbContext)
  - `configuration/` (Models Class "tables")
  - **DbContext**: Implement `ApplicationDbContext` inheriting from `IdentityDbContext`
  - **Configurations**: Create `IEntityTypeConfiguration<T>` for each entity(Models)
  - The Below Not Yet!
  - `Features/` (CQRS pattern: Commands, Queries, Handlers)
  - `Common/` (Interfaces, Behaviors, Exceptions)
  - `DTOs/` (Data Transfer Objects)
  - `Mappings/` (AutoMapper profiles)


### **Step 9: Infrastructure Layer (SLL aka Service Logic Layer)**
- **Folder Structure**:
  - The Below Not Yet!
  - `Persistence/` (DbContext, Configurations, Migrations)
  - `Services/` (External service implementations)
  - `Repositories/` (if using Repository pattern)


### **Step 10: Presentation Layer Configuration**
**For both _MVC_ and _API_ projects:**

1. **Set as Startup Project**: Right-click → Set as Startup Project
2. **appsettings.json**: Add connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=...;Database=...;Trusted_Connection=True;"
   }
   ```
3. **Program.cs Configuration**:
   - Add DbContext with SQL Server
   - Add Identity with custom options
   - Configure authentication/authorization pipeline
   - Add necessary middleware
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUserClass, IdentityRole>(options => {
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

app.UseAuthentication();
app.UseAuthorization();
```

### **Step 11: Database Migrations**
**Important**: Install `Microsoft.EntityFrameworkCore.Tools` in **Presentation Layer** (API or MVC)

**Commands**:
```powershell
# Package Manager Console (set Default Project = [ BLL ] / Infrastructure)
PM > Add-Migration "Initial"
PM > Update-Database
```

### **Step 12: Additional Layers (Optional)**
**Cross-Cutting Concerns** (can be separate projects):
1. **Shared Kernel**: Common code used by multiple layers
2. **Common Library**: Utilities, extensions, constants
3. **Tests Projects**:
   - `Domain.Tests`
   - `Application.Tests`
   - `Infrastructure.Tests`
   - `Presentation.Tests`

## Naming Summary Table

| Your Naming | Clean Architecture Standard | Purpose |
|-------------|----------------------------|---------|
| DAL | **Domain Layer** | Business entities, rules, interfaces |
| BLL | **Application Layer** | Use cases, business logic, DTOs |
| SLL | **Infrastructure Layer** | External services, persistence, email |
| PLL.MVC | **Web/UI Layer (MVC)** | User interface (MVC pattern) |
| PLL.API | **Web/UI Layer (API)** | REST API endpoints |
| Payment Layer | **Infrastructure.Payments** | Payment gateway implementations |

## Key Principles to Remember

1. **Dependency Rule**: Dependencies point inward (Domain(DAL) ← Application(BLL) ← Infrastructure(SLL) ← Presentation(PLL))
2. **Separation of Concerns**: Each layer has single responsibility
3. **Testability**: Each layer can be tested independently
4. **Maintainability**: Changes in one layer don't affect others unnecessarily
5. **Scalability**: Easy to add new features or presentation layers



---
---
---
---

## What “Design Pattern” Means

A design pattern:

* Solves a **recurring problem**
* Is **language-agnostic**
* Improves **maintainability, scalability, and readability**
* Provides a **shared vocabulary** for developers

---

## Common Design Pattern Categories in Full-Stack Dev

### 1. **Architectural Patterns** (High-level structure)

Used across the whole stack.

| Pattern                         | Description                                 | Where Used                |
| ------------------------------- | ------------------------------------------- | ------------------------- |
| **MVC (Model-View-Controller)** | Separates data, UI, and logic               | Backend + Frontend        |
| **MVVM**                        | View bound to ViewModel (data binding)      | Frontend (React, Angular) |
| **Layered Architecture**        | UI → Service → Data layers                  | Backend                   |
| **Microservices**               | Independent services communicating via APIs | Backend                   |
| **Monolithic**                  | All components in one application           | Backend                   |
| **Client-Server**               | Frontend consumes backend APIs              | Full-stack                |

---

### 2. **Backend Design Patterns**

Used in server-side development.

| Pattern                | Purpose                               |
| ---------------------- | ------------------------------------- |
| **Repository Pattern** | Abstract database logic               |
| **Service Pattern**    | Business logic layer                  |
| **Singleton**          | Single instance (e.g., DB connection) |
| **Factory**            | Create objects dynamically            |
| **Observer**           | Event-driven systems                  |
| **Strategy**           | Swap algorithms at runtime            |

Example:

```text
Controller → Service → Repository → Database
```

---

### 3. **Frontend Design Patterns**

Used in UI development.

| Pattern                          | Purpose                      |
| -------------------------------- | ---------------------------- |
| **Component-Based Architecture** | Reusable UI components       |
| **Container / Presentational**   | Logic vs UI separation       |
| **Flux / Redux**                 | Predictable state management |
| **Atomic Design**                | UI hierarchy (atoms → pages) |
| **Observer**                     | UI reacts to state changes   |

---

### 4. **API & Communication Patterns**

Connect frontend and backend.

| Pattern                        | Description                           |
| ------------------------------ | ------------------------------------- |
| **REST**                       | Resource-based APIs                   |
| **GraphQL**                    | Flexible data querying                |
| **BFF (Backend for Frontend)** | Backend tailored per frontend         |
| **CQRS**                       | Separate read/write operations        |
| **Event-Driven**               | Async communication (Kafka, RabbitMQ) |

---

## Why Design Patterns Matter in Full-Stack Dev

They help you:

* Scale applications 
* Reduce bugs 
* Improve collaboration 
* Write testable code 
* Make architectural decisions faster

---

## Example (Real-World Full-Stack Pattern)

**React + Node.js App**

* Frontend: Component-based + Redux
* Backend: MVC + Service + Repository
* API: REST
* Database: Repository Pattern
* Auth: Singleton for JWT service

---

## One-Line Definition D/P

> *A design pattern in full-stack development is a reusable architectural or structural solution that helps organize frontend, backend, and data flow in a scalable and maintainable way.*

---


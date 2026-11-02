#  E-Commerce API

A modular **E-Commerce RESTful API** built with **ASP.NET Core Web API and .NET 8**, following Clean Architecture principles. The project provides essential online shopping functionalities, including product catalog management, advanced product filtering, authentication and authorization, basket management, order processing, Redis caching, and Stripe payment integration.

##  Features

- **Product Catalog:** Manage products, brands, and product types.
- **Advanced Product Filtering:** Search, filter, sort, and paginate products.
- **Authentication & Authorization:** Secure user authentication using ASP.NET Core Identity and JWT Bearer tokens.
- **Role-Based Authorization:** Control access to protected endpoints based on user roles.
- **Shopping Basket:** Manage customer baskets with Redis caching.
- **Order Management:** Create and manage orders with delivery method selection.
- **Payment Integration:** Process payments using Stripe.
- **Database Management:** Entity Framework Core migrations and database seeding.
- **Object Mapping:** Map entities to DTOs using AutoMapper.
- **API Documentation:** Interactive API documentation using Swagger/OpenAPI.
- **Static Files:** Support for serving product images and other static content.

## Architecture

The solution follows a layered architecture inspired by **Clean Architecture**, separating responsibilities into four main projects:

| Layer | Responsibility |
|---|---|
| **E_Commerce.API** | API controllers, filters, middleware, Swagger configuration, and application startup. |
| **E_Commerce.Application** | Application services, DTOs, interfaces, specifications, and mapping profiles. |
| **E_Commerce.Domain** | Core domain entities, contracts, and shared abstractions. |
| **E_Commerce.Infrastructure** | Database access, repositories, Identity, Redis, Stripe integration, migrations, and data seeding. |

This separation promotes maintainability, scalability, testability, and independence between business logic and external infrastructure.

##  Design Patterns & Principles

The project applies several software design patterns and principles:

- **Clean Architecture:** Separates business logic from external dependencies and infrastructure.
- **Repository Pattern:** Encapsulates data access through generic repositories.
- **Unit of Work Pattern:** Coordinates repository operations and manages database transactions through a shared DbContext.
- **Specification Pattern:** Encapsulates reusable query criteria, filtering, sorting, eager loading, and pagination.
- **Dependency Injection:** Manages application services and their dependencies.
- **DTO Pattern:** Separates API data contracts from domain entities.
- **Result Pattern:** Provides a consistent structure for handling successful and failed operations.
- **Data Mapper Pattern:** Uses AutoMapper for entity-to-DTO mapping.
- **Filter Pattern:** Implements RedisCacheAttribute to intercept API requests and cache responses.
- **Abstraction & Dependency Inversion:** Uses application-level contracts to decouple infrastructure implementations, including caching and payment services.

## 🛠️ Technologies & Tools

| Category | Technologies |
|---|---|
| Backend | C#, .NET 8, ASP.NET Core Web API |
| ORM | Entity Framework Core 8 |
| Database | Microsoft SQL Server |
| Authentication | ASP.NET Core Identity, JWT |
| Caching | Redis, StackExchange.Redis |
| Payment | Stripe.NET |
| Mapping | AutoMapper |
| API Documentation | Swagger, OpenAPI, Swashbuckle |
| Architecture | Clean Architecture, SOLID Principles |
| Tools | Visual Studio 2022, Git, GitHub |

##  Project Structure

```text
E_Commerce/
│
├── E_Commerce.API/
│   ├── Controllers/
│   ├── Extensions/
│   ├── Helpers/
│   └── Program.cs
│
├── E_Commerce.Application/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Specifications/
│   └── MappingProfiles/
│
├── E_Commerce.Domain/
│   ├── Entities/
│   ├── Contracts/
│   └── Exceptions/
│
└── E_Commerce.Infrastructure/
    ├── Data/
    ├── Repositories/
    ├── Identity/
    ├── Services/
    └── Migrations/
```

*Note: The folder structure above illustrates the main project organization. Adjust subfolder names to match the actual repository.*

##  Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Microsoft SQL Server
- Redis Server
- Visual Studio 2022 or another .NET-compatible IDE
- A Stripe account and API keys (for payment functionality)

### 1. Clone the Repository

```bash
git clone https://github.com/nonoriz/E_Commerce.git
cd E_Commerce
```

### 2. Configure Application Settings

Configure the required settings in the API project's configuration or through environment variables:

- SQL Server connection string
- JWT authentication settings
- Redis connection settings
- Stripe API keys
- Application URLs

**Important:** Never commit sensitive credentials, production connection strings, JWT signing keys, or Stripe secrets to GitHub.

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Build the Solution

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run --project E_Commerce.API
```

### 6. Explore the API

Once the application is running, open the Swagger URL displayed in the console to explore and test the available endpoints.

Database migrations and data seeding are configured to run during application startup when pending migrations are detected.

##  Security

- JWT-based authentication.
- Role-based authorization.
- ASP.NET Core Identity for user management.
- Secure configuration of sensitive application settings.

Use environment variables, .NET User Secrets, or a dedicated secret manager to store sensitive configuration values.

##  Future Improvements

Potential areas for further development include:

- Unit and integration testing.
- Docker containerization.
- CI/CD pipeline automation.
- Advanced logging and monitoring.
- Deployment to a cloud hosting platform.

##  Author

**Nour Ahmed**

- GitHub: [@nonoriz](https://github.com/nonoriz)

---

*Built as a practical backend development project to apply modern ASP.NET Core technologies, software architecture principles, and design patterns.*

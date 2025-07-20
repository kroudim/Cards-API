# Cards-API

## Overview

**Cards-API** is a backend RESTful API built with ASP.NET Core for creating, managing, and querying card entities. This project is designed for extensibility and maintainability, following clean architecture principles, and is suitable for use in games, collections, or any scenario requiring card management.

---

## Technology Stack

- **C# / .NET 8**  
  Primary language and runtime for backend development.

- **ASP.NET Core Web API**  
  Framework for building RESTful HTTP services.

- **Entity Framework Core**  
  ORM for database access and migrations.

- **Automapper**  
  Library for mapping between domain models and data transfer objects (DTOs).

- **Dependency Injection (.NET Core DI)**  
  Built-in dependency injection for managing services.

- **Swagger / OpenAPI**  
  For API documentation and interactive testing.

- **xUnit / Moq**  
  For unit and integration testing.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Supported database (e.g., SQL Server, SQLite)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/kroudim/Cards-API.git
   cd Cards-API
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Configure your database connection**
   - Update the connection string in `appsettings.json` to match your environment.

5. **Apply migrations and update the database**
   ```bash
   dotnet ef database update
   ```

6. **Run the API**
   ```bash
   dotnet run --project Cards.API
   ```

7. **Access Swagger UI**
   - Visit [http://localhost:5000/swagger](http://localhost:5000/swagger) to explore and test the API.

---

## Project Structure

```
Cards-API/
├── Cards.API            # ASP.NET Core Web API entry point
├── Cards.Application    # Business logic and application services
├── Cards.Domain         # Domain models and entities
├── Cards.Infrastructure # Data access and external dependencies
├── Cards.Tests          # Unit and integration tests
```


## License

This project is licensed under the MIT License.

---

**Happy coding!**a

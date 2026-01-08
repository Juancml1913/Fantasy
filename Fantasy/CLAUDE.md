# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Run Commands

```bash
# Build entire solution
dotnet build Fantasy.sln

# Run backend API (starts on https://localhost:7128)
dotnet run --project Fantasy.Backend

# Run frontend Blazor WebAssembly app
dotnet run --project Fantasy.Fronted

# Run tests
dotnet test Fantasy.Test

# Run a single test
dotnet test Fantasy.Test --filter "FullyQualifiedName~TestMethodName"

# EF Core migrations (from solution root)
dotnet ef migrations add MigrationName --project Fantasy.Backend
dotnet ef database update --project Fantasy.Backend
```

## Architecture

This is a **Fantasy Soccer Predictions** application built with .NET 8 using a three-tier architecture:

### Projects

- **Fantasy.Backend**: ASP.NET Core Web API with Entity Framework Core and SQL Server (LocalDB)
- **Fantasy.Fronted**: Blazor WebAssembly standalone SPA client
- **Fantasy.Shared**: Shared entities and localized resources used by both backend and frontend
- **Fantasy.Test**: MSTest unit tests

### Key Patterns

**Backend API Structure:**
- Controllers follow REST conventions at `api/[controller]` routes
- `DataContext` in `Data/` folder manages EF Core DbSets
- Entities live in `Fantasy.Shared/Entities/` and are shared with the frontend

**Frontend Architecture:**
- Pages use code-behind pattern (`.razor` + `.razor.cs` files)
- `Repositories/` folder contains `IRepository`/`Repository` for HTTP communication with the backend
- `HttpResponseWrapper<T>` wraps API responses with error handling
- `Shared/GenericList.razor` is a reusable templated component for list displays with loading/empty states

**Localization:**
- String resources defined in `Fantasy.Shared/Resources/Literals.resx`
- Frontend uses `IStringLocalizer<Literals>` for UI text
- Supports multiple languages via `.resx` files

### Configuration

- Backend API base URL hardcoded in `Fantasy.Fronted/Program.cs` as `https://localhost:7128`
- Database connection string `LocalConnection` in `Fantasy.Backend/appsettings.json`

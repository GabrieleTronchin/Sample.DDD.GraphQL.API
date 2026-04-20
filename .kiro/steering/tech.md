# Tech Stack

## Runtime & Language
- .NET 9.0 (net9.0)
- C# with nullable reference types and implicit usings enabled

## Frameworks & Libraries
- ASP.NET Core Minimal API
- Hot Chocolate v15.0.3 (GraphQL server) with filtering, sorting, and cursor-based pagination
- Entity Framework Core 9.0.2 with InMemory provider (SQL Server provider also referenced)
- Bogus v35.6.2 for fake data generation in domain entities
- Swashbuckle / OpenAPI for REST endpoint documentation

## Build System
- .NET SDK / MSBuild
- Solution file: `src/Sample.GraphQL.API.sln`
- Docker support via `Dockerfile` in the API project

## Common Commands

Build the solution:
```
dotnet build src/Sample.GraphQL.API.sln
```

Run the API (launches on http://localhost:5055):
```
dotnet run --project src/Sample.GraphQL.API
```

Restore NuGet packages:
```
dotnet restore src/Sample.GraphQL.API.sln
```

## Key Conventions
- Dependency injection is configured via `ServicesExtensions.cs` files using `IServiceCollection` extension methods (e.g., `AddPersistence()`, `AddPresentationLayer()`)
- GraphQL server is registered through Hot Chocolate's `AddGraphQLServer()` fluent API
- EF Core DbContext is registered as a singleton with InMemory provider
- Database is seeded in `SeedDb.Initialize()` called from `Program.cs` after app build
- No test project exists in the solution currently

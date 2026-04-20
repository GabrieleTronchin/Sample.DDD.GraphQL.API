# Implementation Plan: .NET 10 Upgrade and README Documentation

## Overview

This plan upgrades the Sample GraphQL API solution from .NET 9.0 to .NET 10, updates all NuGet dependencies, removes Swashbuckle/Swagger entirely (the only web UI is the GraphQL playground), updates the Dockerfile, and rewrites the README with comprehensive documentation. The upgrade follows a bottom-up dependency order (Domain → DataModel → Application → API → Dockerfile) to avoid transient build failures.

## Tasks

- [ ] 1. Upgrade Domain project to .NET 10
  - [ ] 1.1 Update `src/Sample.GraphQL.Domain/Sample.GraphQL.Domain.csproj`
    - Change `<TargetFramework>` from `net9.0` to `net10.0`
    - Update `Bogus` from `35.6.2` to `35.6.5`
    - Update `Microsoft.Extensions.DependencyInjection.Abstractions` from `9.0.2` to `10.0.0`
    - Remove the stale `<None Remove="inl2xyl2.wiw~" />` item if present
    - _Requirements: 1.1, 1.3, 2.1, 2.2_

- [ ] 2. Upgrade DataModel project to .NET 10
  - [ ] 2.1 Update `src/Sample.GraphQL.DataModel/Sample.GraphQL.Persistence.csproj`
    - Change `<TargetFramework>` from `net9.0` to `net10.0`
    - Update `HotChocolate.Data.EntityFramework` from `15.0.3` to `15.1.12`
    - Update `Microsoft.EntityFrameworkCore` from `9.0.2` to `10.0.0`
    - Update `Microsoft.EntityFrameworkCore.InMemory` from `9.0.2` to `10.0.0`
    - Update `Microsoft.EntityFrameworkCore.SqlServer` from `9.0.2` to `10.0.0`
    - Remove `Microsoft.AspNetCore.Http.Abstractions` package reference entirely (included in ASP.NET Core shared framework since .NET 6)
    - _Requirements: 1.1, 1.3, 2.1, 2.2, 2.3, 2.4_

- [ ] 3. Upgrade Application project to .NET 10
  - [ ] 3.1 Update `src/Sample.GraphQL.Application/Sample.GraphQL.Application.csproj`
    - Change `<TargetFramework>` from `net9.0` to `net10.0`
    - Update `HotChocolate.AspNetCore` from `15.0.3` to `15.1.12`
    - Remove `Microsoft.AspNetCore.OpenApi` package reference entirely (not needed — only web UI is GraphQL playground)
    - _Requirements: 1.1, 1.3, 2.1, 2.2, 2.3, 2.4_

- [ ] 4. Upgrade API project to .NET 10 and remove Swagger/OpenAPI
  - [ ] 4.1 Update `src/Sample.GraphQL.API/Sample.GraphQL.API.csproj`
    - Change `<TargetFramework>` from `net9.0` to `net10.0`
    - Remove `Microsoft.AspNetCore.OpenApi` package reference entirely
    - Remove `Swashbuckle.AspNetCore` package reference entirely
    - Update `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` to latest stable version
    - _Requirements: 1.1, 1.3, 2.1, 2.2, 2.4_
  - [ ] 4.2 Update `src/Sample.GraphQL.API/Program.cs` to remove Swagger middleware
    - Remove `builder.Services.AddEndpointsApiExplorer();`
    - Remove `builder.Services.AddSwaggerGen();`
    - Remove the entire `if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }` block
    - The GraphQL playground at `/graphql/` (mapped via `app.MapGraphQL()`) is the only web UI
    - _Requirements: 2.4_

- [ ] 5. Checkpoint — Build verification
  - Run `dotnet build src/Sample.GraphQL.API.sln` and confirm zero errors
  - Ensure all tests pass, ask the user if questions arise.
  - _Requirements: 1.1, 1.2, 2.2_

- [ ] 6. Update Dockerfile to .NET 10 base images
  - [ ] 6.1 Update `src/Sample.GraphQL.API/Dockerfile`
    - Change runtime base image from `mcr.microsoft.com/dotnet/aspnet:8.0` to `mcr.microsoft.com/dotnet/aspnet:10.0`
    - Change build SDK image from `mcr.microsoft.com/dotnet/sdk:8.0` to `mcr.microsoft.com/dotnet/sdk:10.0`
    - _Requirements: 3.1, 3.2, 3.3_

- [ ] 7. Rewrite README — Project overview, technologies, and architecture
  - [ ] 7.1 Rewrite `README.md` with title, project overview, and technology list
    - Add a title section with the project name
    - Describe the project as a cinema showtime management API built with ASP.NET Core and Hot Chocolate
    - List key technologies: .NET 10, Hot Chocolate, Entity Framework Core with InMemory provider, Bogus for data generation
    - _Requirements: 4.1, 4.2_
  - [ ] 7.2 Add GraphQL concepts section
    - Explain that GraphQL is a query language allowing clients to declaratively specify exact data needed
    - Explain that Hot Chocolate is an open-source GraphQL server for .NET adhering to latest specs
    - Explain Hot Chocolate's integration with EF Core through IQueryable for filtering and pagination
    - _Requirements: 5.1, 5.2, 5.3_
  - [ ] 7.3 Add architecture section
    - Describe the four-project layered architecture: API, Application, Domain, DataModel
    - Explain dependency direction: API → Application/DataModel → Domain
    - Describe key conventions: private constructors with static `Create()` factory methods, repository interfaces in Domain, implementations in DataModel
    - _Requirements: 4.3, 10.1, 10.2, 10.3_

- [ ] 8. Rewrite README — Getting started and query examples
  - [ ] 8.1 Add getting started section
    - List prerequisites including .NET 10 SDK
    - Provide build command: `dotnet build src/Sample.GraphQL.API.sln`
    - Provide run command: `dotnet run --project src/Sample.GraphQL.API`
    - State GraphQL Playground is available at `http://localhost:5055/graphql/` (the only web UI)
    - Include reference to `assets/SchemaReference.png`
    - _Requirements: 6.1, 6.2, 6.3, 6.4, 6.5_
  - [ ] 8.2 Add basic query examples section
    - Show a basic `all` query retrieving `id`, `movieId`, and nested `movie.title`
    - Explain that the `all` endpoint corresponds to the `GetAll` method in `ShowtimesQuery`
    - Include reference to `assets/SampleResult1.png`
    - _Requirements: 7.1, 7.2, 7.3_
  - [ ] 8.3 Add filtering examples section
    - Show a `showTimes` query with `where` clause filtering by `movie.title`
    - Explain that filtering is enabled by the `[UseFiltering]` attribute on `GetShowTimes`
    - Include reference to `assets/Filters.png`
    - Provide link to official Hot Chocolate filtering documentation
    - _Requirements: 8.1, 8.2, 8.3, 8.4_
  - [ ] 8.4 Add pagination examples section
    - Show cursor-based pagination on `showTimes` using `first`, `after`, `totalCount`, `pageInfo`, `edges`, and `cursor`
    - Show two query examples: first page and navigating to next page with `after` cursor
    - Explain that pagination is enabled by the `[UsePaging]` attribute on `GetShowTimes`
    - Include references to `assets/Pagination_Page1.png` and `assets/Pagination_Page2.png`
    - Provide link to official GraphQL pagination documentation
    - _Requirements: 9.1, 9.2, 9.3, 9.4, 9.5_

- [ ] 9. Final checkpoint — Verify build and review
  - Run `dotnet build src/Sample.GraphQL.API.sln` and confirm zero errors after all changes
  - Verify all `assets/*.png` references in README resolve to existing files
  - Ensure all tests pass, ask the user if questions arise.
  - _Requirements: 1.1, 1.2, 2.2_

## Notes

- No property-based tests or unit tests are included because the changes are configuration edits, Swagger removal, and documentation — none involve pure functions with varying inputs
- The upgrade follows bottom-up dependency order (Domain → DataModel → Application → API) to prevent transient build failures
- Hot Chocolate stays on v15.1.x (v16 is RC, not yet stable)
- `Microsoft.AspNetCore.Http.Abstractions` is removed rather than updated — it's included in the shared framework since .NET 6
- Swashbuckle and `Microsoft.AspNetCore.OpenApi` are removed entirely — the only web UI is the Hot Chocolate GraphQL playground
- The README reuses all existing screenshot assets in the `assets/` folder

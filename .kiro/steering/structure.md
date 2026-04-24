# Project Structure

The solution follows a layered architecture inspired by Domain-Driven Design (DDD), with four projects under `src/`.

```
src/
├── Sample.GraphQL.API.sln              # Solution file
├── Sample.GraphQL.API/                 # Host / entry point (Minimal API)
│   ├── Program.cs                      # App bootstrap, middleware, seed
│   ├── Dockerfile
│   └── Properties/launchSettings.json
├── Sample.GraphQL.Application/         # Application layer (GraphQL + REST)
│   ├── ShowtimesQuery.cs               # GraphQL query type (Hot Chocolate)
│   ├── Endpoints/                      # REST Minimal API endpoint groups
│   │   └── CinemaEndpoint.cs
│   └── ServicesExtensions.cs           # DI registration for GraphQL
├── Sample.GraphQL.Domain/              # Domain layer (entities, interfaces)
│   ├── MovieEntity.cs
│   ├── ShowtimeEntity.cs
│   ├── ShowtimeSeatEntity.cs
│   ├── Seat.cs                         # Value object (record)
│   └── Repository/                     # Repository interfaces
│       ├── IRepository.cs
│       └── IShowtimesRepository.cs
└── Sample.GraphQL.DataModel/           # Persistence layer (EF Core)
    ├── Context/CinemaDbContext.cs       # DbContext
    ├── Configuration/                  # EF entity type configurations
    │   ├── MovieConfiguration.cs
    │   ├── ShowtimeConfiguration.cs
    │   └── ShowtimeSeatConfiguration.cs
    ├── Repository/                     # Repository implementations
    │   └── ShowtimesRepository.cs
    ├── SeedDb.cs                       # Database seeding
    ├── GlobalUsing.cs                  # Shared global usings
    └── ServicesExtensions.cs           # DI registration for persistence
```

## Layer Dependencies

```
API → Application → Domain
API → DataModel  → Domain
```

- **API** references Application and DataModel. It wires everything together in `Program.cs`.
- **Application** references Domain and DataModel. It defines GraphQL queries and REST endpoints.
- **Domain** has no project references. It defines entities with private constructors and static factory methods (`Create()`), repository interfaces, and value objects.
- **DataModel** (assembly name: `Sample.GraphQL.Persistence`) references Domain. It implements repositories, EF configurations, and the DbContext.

## Conventions

- Domain entities use private constructors with static `Create()` factory methods for instantiation.
- Value objects are modeled as C# records (e.g., `Seat`).
- Repository interfaces live in `Domain/Repository/`; implementations live in `DataModel/Repository/`.
- EF entity configurations are in `DataModel/Configuration/` using `IEntityTypeConfiguration<T>`.
- Each layer has a `ServicesExtensions.cs` that exposes a single `IServiceCollection` extension method for DI registration.
- REST endpoints are organized as static extension methods on `IEndpointRouteBuilder` in `Application/Endpoints/`.
- GraphQL query types are plain classes with constructor-injected dependencies, using Hot Chocolate attributes (`[UsePaging]`, `[UseFiltering]`).

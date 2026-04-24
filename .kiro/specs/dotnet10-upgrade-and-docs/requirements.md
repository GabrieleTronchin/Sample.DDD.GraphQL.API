# Requirements Document

## Introduction

This feature covers two goals for the Sample GraphQL API project:

1. **Upgrade to .NET 10**: Migrate the entire solution from .NET 9.0 to .NET 10 and update all NuGet package dependencies to their latest compatible versions, including Hot Chocolate, Entity Framework Core, Bogus, and supporting packages. Swagger/Swashbuckle and OpenAPI packages will be removed entirely since the only web interface needed is the Hot Chocolate GraphQL playground. The Dockerfile should also be updated to use .NET 10 base images.

2. **Improve README documentation**: Rewrite the README.md to make the project more understandable, incorporating content from the author's Medium article about ASP.NET Core GraphQL with Hot Chocolate. The documentation should explain GraphQL concepts, the Hot Chocolate integration, the DDD architecture, and provide clear query examples for selecting, filtering, and pagination.

## Glossary

- **Solution**: The .NET solution file (`Sample.GraphQL.API.sln`) and all four projects it contains.
- **Project_File**: A `.csproj` file that defines a project's target framework and NuGet dependencies.
- **Target_Framework_Moniker**: The `<TargetFramework>` value in a Project_File (e.g., `net9.0`, `net10.0`).
- **NuGet_Package**: A third-party or Microsoft library referenced via `<PackageReference>` in a Project_File.
- **Dockerfile**: The container build definition at `src/Sample.GraphQL.API/Dockerfile`.
- **README**: The `README.md` file at the repository root.
- **GraphQL_Playground**: The Hot Chocolate built-in UI available at `http://localhost:5055/graphql/` for testing queries.
- **Build_System**: The `dotnet build` toolchain used to compile the Solution.
- **Query_Example**: A GraphQL query snippet demonstrating how to use the API.

## Requirements

### Requirement 1: Update Target Framework to .NET 10

**User Story:** As a developer, I want the solution to target .NET 10, so that the project benefits from the latest runtime features, performance improvements, and long-term support.

#### Acceptance Criteria

1. THE Build_System SHALL compile the Solution successfully after all Project_File Target_Framework_Moniker values are changed from `net9.0` to `net10.0`.
2. WHEN the Solution is built with `dotnet build`, THE Build_System SHALL produce zero errors and zero warnings related to the framework upgrade.
3. THE Solution SHALL contain no Project_File with a Target_Framework_Moniker value of `net9.0` after the upgrade is complete.

### Requirement 2: Update All NuGet Dependencies to Latest Versions

**User Story:** As a developer, I want all NuGet package dependencies updated to their latest stable versions compatible with .NET 10, so that the project uses the most current and secure libraries.

#### Acceptance Criteria

1. WHEN the upgrade is performed, THE Project_File for each project SHALL reference the latest stable version of every NuGet_Package currently listed.
2. THE Build_System SHALL compile the Solution successfully after all NuGet_Package versions are updated.
3. WHEN a NuGet_Package has a major version update available (e.g., Hot Chocolate, Entity Framework Core), THE Project_File SHALL reference the latest stable major version compatible with .NET 10.
4. IF a NuGet_Package is deprecated or replaced in .NET 10, THEN THE Project_File SHALL replace the deprecated package with its recommended successor.

### Requirement 3: Update Dockerfile for .NET 10

**User Story:** As a developer, I want the Dockerfile updated to use .NET 10 base images, so that the containerized application runs on the correct runtime.

#### Acceptance Criteria

1. THE Dockerfile SHALL use the `mcr.microsoft.com/dotnet/aspnet:10.0` base image for the runtime stage.
2. THE Dockerfile SHALL use the `mcr.microsoft.com/dotnet/sdk:10.0` base image for the build stage.
3. WHEN the Dockerfile is built with `docker build`, THE Build_System SHALL produce a valid container image without errors.

### Requirement 4: Add Project Overview Section to README

**User Story:** As a new developer exploring the repository, I want the README to contain a clear project overview explaining what the API does and what technologies it uses, so that I can quickly understand the project's purpose.

#### Acceptance Criteria

1. THE README SHALL contain an introduction section that describes the project as a cinema showtime management API built with ASP.NET Core and Hot Chocolate.
2. THE README SHALL list the key technologies used: .NET 10, Hot Chocolate, Entity Framework Core with InMemory provider, and Bogus for data generation.
3. THE README SHALL explain that the project follows a Domain-Driven Design (DDD) layered architecture with four projects: API, Application, Domain, and DataModel.

### Requirement 5: Add GraphQL Concepts Section to README

**User Story:** As a developer unfamiliar with GraphQL, I want the README to explain what GraphQL is and why Hot Chocolate is used, so that I can understand the technology choices.

#### Acceptance Criteria

1. THE README SHALL contain a section explaining that GraphQL is a query language that allows clients to declaratively specify the exact data they need.
2. THE README SHALL explain that Hot Chocolate is an open-source GraphQL server for .NET that adheres to the latest GraphQL specifications.
3. THE README SHALL explain that Hot Chocolate integrates with Entity Framework Core through IQueryable, enabling filtering and pagination at the database level.

### Requirement 6: Add Getting Started Section to README

**User Story:** As a developer cloning the repository, I want clear instructions on how to build and run the project, so that I can start exploring the API immediately.

#### Acceptance Criteria

1. THE README SHALL list the prerequisites needed to run the project, including the .NET 10 SDK.
2. THE README SHALL provide the command to build the solution: `dotnet build src/Sample.GraphQL.API.sln`.
3. THE README SHALL provide the command to run the API: `dotnet run --project src/Sample.GraphQL.API`.
4. THE README SHALL state that the GraphQL_Playground is available at `http://localhost:5055/graphql/` after starting the application.
5. THE README SHALL state that the only web interface is the GraphQL_Playground at `http://localhost:5055/graphql/`; no Swagger or OpenAPI UI is provided.

### Requirement 7: Document GraphQL Query Examples for Basic Selection

**User Story:** As a developer using the API, I want the README to show how to write a basic GraphQL query to select showtime data, so that I can learn how to retrieve data from the API.

#### Acceptance Criteria

1. THE README SHALL contain a Query_Example demonstrating a basic `all` query that retrieves showtime properties including `id`, `movieId`, and nested `movie.title`.
2. THE README SHALL explain that the `all` endpoint corresponds to the `GetAll` method in `ShowtimesQuery` and returns all showtimes without filtering.
3. THE README SHALL include a screenshot or reference to the existing `assets/SampleResult1.png` image showing a sample result.

### Requirement 8: Document GraphQL Filtering Examples

**User Story:** As a developer using the API, I want the README to show how to use GraphQL filtering with the `where` clause, so that I can query specific subsets of data.

#### Acceptance Criteria

1. THE README SHALL contain a Query_Example demonstrating filtering on the `showTimes` endpoint using a `where` clause with nested entity filtering (e.g., filtering by `movie.title`).
2. THE README SHALL explain that filtering is enabled by the `[UseFiltering]` attribute on the `GetShowTimes` method in `ShowtimesQuery`.
3. THE README SHALL include a reference to the existing `assets/Filters.png` image showing a filtering result.
4. THE README SHALL provide a link to the official Hot Chocolate filtering documentation.

### Requirement 9: Document GraphQL Pagination Examples

**User Story:** As a developer using the API, I want the README to show how cursor-based pagination works, so that I can navigate through large result sets.

#### Acceptance Criteria

1. THE README SHALL contain a Query_Example demonstrating cursor-based pagination on the `showTimes` endpoint using `first`, `after`, `totalCount`, `pageInfo`, `edges`, `nodes`, and `cursor` fields.
2. THE README SHALL show two Query_Example instances: one for the first page and one for navigating to the next page using the `after` cursor parameter.
3. THE README SHALL explain that pagination is enabled by the `[UsePaging]` attribute on the `GetShowTimes` method in `ShowtimesQuery`.
4. THE README SHALL include references to the existing `assets/Pagination_Page1.png` and `assets/Pagination_Page2.png` images.
5. THE README SHALL provide a link to the official GraphQL pagination documentation.

### Requirement 10: Document Project Architecture in README

**User Story:** As a developer contributing to the project, I want the README to describe the layered architecture and project structure, so that I know where to find and place code.

#### Acceptance Criteria

1. THE README SHALL contain a section describing the four-project layered architecture: API, Application, Domain, and DataModel.
2. THE README SHALL explain the dependency direction: API references Application and DataModel; Application and DataModel reference Domain; Domain has no project references.
3. THE README SHALL describe the key conventions: domain entities use private constructors with static `Create()` factory methods, repository interfaces live in Domain, and implementations live in DataModel.

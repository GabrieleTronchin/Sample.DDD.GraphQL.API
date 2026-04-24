# Requirements Document

## Introduction

This feature covers a set of improvements to the Sample GraphQL API project, a .NET 10 cinema showtime management API built with Hot Chocolate and EF Core InMemory. The improvements span six areas:

1. **HTTP file for GraphQL testing**: Add a comprehensive `.http` file containing all GraphQL queries (basic selection, filtering, pagination) for quick testing in Visual Studio / VS Code.
2. **Dockerfile evaluation and removal**: Assess whether the Dockerfile adds value for a demo/sample project with an in-memory database, and remove it along with Docker-related configuration if it does not.
3. **Launch settings cleanup**: Clean up `launchSettings.json` to remove stale Swagger references, the Docker profile, and the IIS Express profile, and point the launch URL to the GraphQL playground.
4. **NuGet package updates**: Update all NuGet packages across all four projects to their latest stable versions.
5. **License audit**: Verify all NuGet dependencies are free and open-source with permissive licenses (MIT, Apache 2.0, etc.).
6. **Unit test project**: Create a new xUnit test project with meaningful tests for existing domain logic (entity factory methods, seat reservation rules, repository behavior).

## Glossary

- **Solution**: The .NET solution file (`Sample.GraphQL.API.sln`) and all projects it contains.
- **Project_File**: A `.csproj` file that defines a project's target framework and NuGet dependencies.
- **NuGet_Package**: A third-party or Microsoft library referenced via `<PackageReference>` in a Project_File.
- **HTTP_File**: A `.http` file that contains HTTP requests executable by Visual Studio, VS Code REST Client, or the built-in .NET HTTP file support.
- **GraphQL_Endpoint**: The Hot Chocolate GraphQL server endpoint at `http://localhost:5055/graphql/`.
- **Dockerfile**: The container build definition at `src/Sample.GraphQL.API/Dockerfile`.
- **Docker_Configuration**: All Docker-related files and settings including the Dockerfile, `.dockerignore`, and `DockerDefaultTargetOS` property in the API Project_File.
- **Build_System**: The `dotnet build` toolchain used to compile the Solution.
- **Test_Project**: An xUnit test project added to the Solution for unit testing domain logic.
- **Domain_Entity**: A class in the Domain layer that uses a private constructor with a static `Create()` factory method (e.g., `MovieEntity`, `ShowtimeEntity`, `ShowtimeSeatEntity`).
- **Seat**: A value object (C# record) representing a row number and seat number in the cinema.
- **License_Audit**: A review of all NuGet_Package dependencies to confirm their license types.
- **Launch_Settings**: The `launchSettings.json` file at `src/Sample.GraphQL.API/Properties/launchSettings.json` that defines launch profiles for the API project.

## Requirements

### Requirement 1: HTTP File with Basic Selection Query

**User Story:** As a developer testing the API, I want an HTTP file containing a basic GraphQL selection query, so that I can quickly test the `all` endpoint without opening the GraphQL playground.

#### Acceptance Criteria

1. THE HTTP_File SHALL exist at `src/Sample.GraphQL.API/Sample.GraphQL.API.http` and contain a GraphQL POST request targeting the GraphQL_Endpoint.
2. THE HTTP_File SHALL contain a basic selection query that retrieves showtime properties including `id`, `sessionDate`, `movieId`, and nested `movie` fields (`title`, `stars`, `releaseDate`, `imdbId`).
3. WHEN the basic selection query is sent to the GraphQL_Endpoint, THE GraphQL_Endpoint SHALL return a JSON response containing the requested showtime data.

### Requirement 2: HTTP File with Filtering Query

**User Story:** As a developer testing the API, I want an HTTP file containing a GraphQL filtering query, so that I can quickly test the `showTimes` endpoint with `where` clause filtering.

#### Acceptance Criteria

1. THE HTTP_File SHALL contain a GraphQL query that uses the `showTimes` endpoint with a `where` clause to filter results by a nested entity field (e.g., filtering by `movie.title`).
2. THE HTTP_File SHALL demonstrate the Hot Chocolate filtering syntax with the `where` input parameter containing a nested object filter expression.
3. WHEN the filtering query is sent to the GraphQL_Endpoint, THE GraphQL_Endpoint SHALL return only showtimes matching the filter criteria.

### Requirement 3: HTTP File with Pagination Query

**User Story:** As a developer testing the API, I want an HTTP file containing GraphQL pagination queries, so that I can quickly test cursor-based pagination on the `showTimes` endpoint.

#### Acceptance Criteria

1. THE HTTP_File SHALL contain a GraphQL query that uses the `showTimes` endpoint with cursor-based pagination parameters including `first` and `after`.
2. THE HTTP_File SHALL include pagination metadata fields in the query: `totalCount`, `pageInfo` (with `hasNextPage`, `endCursor`), and `edges` with `cursor` and `node` fields.
3. THE HTTP_File SHALL contain at least two pagination queries: one for the first page (using `first` only) and one demonstrating navigation to the next page (using `first` and `after` with a cursor value).

### Requirement 4: HTTP File Format and Compatibility

**User Story:** As a developer, I want the HTTP file to follow standard `.http` file conventions, so that it works with Visual Studio, VS Code REST Client, and the built-in .NET HTTP file support.

#### Acceptance Criteria

1. THE HTTP_File SHALL use `###` separators between individual requests.
2. THE HTTP_File SHALL define a variable for the base host address and use the variable in all request URLs.
3. THE HTTP_File SHALL set the `Content-Type` header to `application/json` for all GraphQL requests.
4. THE HTTP_File SHALL use `POST` method for all GraphQL requests with the query in the JSON request body.

### Requirement 5: Evaluate and Remove Dockerfile

**User Story:** As a project maintainer, I want the Dockerfile removed if it does not add value for a sample/demo project with an in-memory database, so that the project contains only relevant files.

#### Acceptance Criteria

1. WHEN the Dockerfile is determined to not add value for a sample project with an in-memory database, THE Solution SHALL have the Dockerfile at `src/Sample.GraphQL.API/Dockerfile` removed.
2. WHEN the Dockerfile is removed, THE Solution SHALL have the `.dockerignore` file at the repository root removed.
3. WHEN the Dockerfile is removed, THE Project_File for the API project SHALL have the `DockerDefaultTargetOS` property removed from its `PropertyGroup`.
4. WHEN the Dockerfile is removed, THE Project_File for the API project SHALL have the `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` NuGet_Package reference removed.
5. WHEN the Dockerfile is removed, THE Build_System SHALL compile the Solution successfully with zero errors.

### Requirement 6: Clean Up Launch Settings

**User Story:** As a developer, I want the launch settings cleaned up to remove stale references and unnecessary profiles, so that the development experience is consistent and correct.

#### Acceptance Criteria

1. THE Launch_Settings SHALL NOT contain any `launchUrl` value referencing `swagger` (Swagger has been removed from the project).
2. THE Launch_Settings SHALL set the `launchUrl` to `graphql` for all remaining profiles, so the browser opens the GraphQL playground on launch.
3. THE Launch_Settings SHALL NOT contain a `Docker` profile (the Dockerfile is being removed).
4. THE Launch_Settings SHALL NOT contain an `IIS Express` profile or `iisSettings` section (not needed for a sample project using Kestrel).
5. THE Launch_Settings SHALL retain the `http` profile with `applicationUrl` set to `http://localhost:5055`.
6. THE Launch_Settings SHALL retain the `https` profile with both HTTPS and HTTP URLs.

### Requirement 7: Update All NuGet Packages to Latest Stable Versions

**User Story:** As a developer, I want all NuGet packages updated to their latest stable versions, so that the project uses the most current and secure libraries.

#### Acceptance Criteria

1. WHEN the update is performed, THE Project_File for each project SHALL reference the latest stable version of every NuGet_Package currently listed.
2. THE Build_System SHALL compile the Solution successfully after all NuGet_Package versions are updated.
3. IF a NuGet_Package has a newer stable major version available, THEN THE Project_File SHALL reference the latest stable major version.
4. IF a NuGet_Package is deprecated or no longer needed, THEN THE Project_File SHALL remove the deprecated package reference.

### Requirement 8: Verify All Packages Are Free and Open-Source

**User Story:** As a project maintainer, I want all NuGet dependencies audited for licensing, so that I can confirm the project uses only free and open-source packages.

#### Acceptance Criteria

1. THE License_Audit SHALL examine every NuGet_Package referenced across all Project_File instances in the Solution.
2. THE License_Audit SHALL verify that each NuGet_Package uses a permissive open-source license (MIT, Apache 2.0, BSD, or equivalent).
3. IF a NuGet_Package uses a restrictive or commercial license, THEN THE License_Audit SHALL flag the package with its license type and a recommendation for replacement or removal.
4. THE License_Audit results SHALL be documented in a section of the requirements or design document listing each package and its license.

### Requirement 9: Create Unit Test Project

**User Story:** As a developer, I want a unit test project added to the solution, so that the domain logic has automated test coverage.

#### Acceptance Criteria

1. THE Test_Project SHALL be an xUnit test project targeting the same framework as the Solution (net10.0).
2. THE Test_Project SHALL be added to the Solution file (`Sample.GraphQL.API.sln`).
3. THE Test_Project SHALL reference the Domain project (`Sample.GraphQL.Domain`).
4. WHEN `dotnet test` is run against the Solution, THE Build_System SHALL discover and execute all tests in the Test_Project.

### Requirement 10: Unit Tests for MovieEntity Factory Method

**User Story:** As a developer, I want unit tests for the `MovieEntity.Create()` factory method, so that I can verify movie entities are created correctly.

#### Acceptance Criteria

1. THE Test_Project SHALL contain tests that verify `MovieEntity.Create()` assigns the provided `title`, `stars`, `imdbId`, and `releaseDate` to the corresponding properties of the created entity.
2. THE Test_Project SHALL contain a test that verifies `MovieEntity.Create()` assigns a non-empty `Guid` to the `Id` property.
3. THE Test_Project SHALL contain a test that verifies two calls to `MovieEntity.Create()` produce entities with distinct `Id` values.

### Requirement 11: Unit Tests for ShowtimeEntity Factory Method and Seat Reservation

**User Story:** As a developer, I want unit tests for `ShowtimeEntity.Create()` and the seat reservation logic, so that I can verify showtime creation and business rules are enforced.

#### Acceptance Criteria

1. THE Test_Project SHALL contain tests that verify `ShowtimeEntity.Create()` assigns the provided `movie` and `sessionDate` to the created entity.
2. THE Test_Project SHALL contain a test that verifies `ShowtimeEntity.Create()` throws `ArgumentNullException` when the `movie` parameter is null.
3. THE Test_Project SHALL contain a test that verifies `ShowtimeEntity.ReserveSeats()` throws `InvalidOperationException` when seats span multiple rows.
4. THE Test_Project SHALL contain a test that verifies `ShowtimeEntity.ReserveSeats()` throws `InvalidOperationException` when seat numbers are not contiguous within the same row.

### Requirement 12: Unit Tests for ShowtimeSeatEntity Business Rules

**User Story:** As a developer, I want unit tests for `ShowtimeSeatEntity` reservation and purchase logic, so that I can verify the seat lifecycle business rules are enforced.

#### Acceptance Criteria

1. THE Test_Project SHALL contain a test that verifies `ShowtimeSeatEntity.Create()` initializes `Purchased` to `false` and `ReservationTime` to `null`.
2. THE Test_Project SHALL contain a test that verifies `ShowtimeSeatEntity.SetReserved()` sets the `ReservationTime` to a non-null value.
3. THE Test_Project SHALL contain a test that verifies `ShowtimeSeatEntity.SetPurchased()` throws `InvalidOperationException` when the seat is already purchased.
4. THE Test_Project SHALL contain a test that verifies `ShowtimeSeatEntity.SetReserved()` throws `InvalidOperationException` when the seat is already purchased.
5. THE Test_Project SHALL contain a test that verifies `ShowtimeSeatEntity.SetReserved()` throws `InvalidOperationException` when called within the 10-minute reservation cooldown period.

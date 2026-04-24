# Tasks

## Task 1: Rewrite HTTP File with GraphQL Queries
- [x] 1.1 Replace the contents of `src/Sample.GraphQL.API/Sample.GraphQL.API.http` with a `@host` variable set to `http://localhost:5055`
- [x] 1.2 Add a basic selection query using `POST {{host}}/graphql` that calls the `all` query and retrieves `id`, `sessionDate`, `movieId`, and nested `movie { title, stars, releaseDate, imdbId }`
- [x] 1.3 Add a filtering query using the `showTimes` endpoint with `where: { movie: { title: { eq: "Dune Part 1" } } }`
- [x] 1.4 Add a first-page pagination query using `showTimes(first: 2)` with `totalCount`, `pageInfo { hasNextPage, endCursor }`, and `edges { cursor, node { ... } }`
- [x] 1.5 Add a next-page pagination query using `showTimes(first: 2, after: "<cursor>")` demonstrating cursor-based navigation
- [x] 1.6 Ensure all requests use `###` separators, `POST` method, and `Content-Type: application/json` header

## Task 2: Remove Dockerfile and Docker Configuration
- [x] 2.1 Delete `src/Sample.GraphQL.API/Dockerfile`
- [x] 2.2 Delete `.dockerignore` from the repository root
- [x] 2.3 Remove `<DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>` from `src/Sample.GraphQL.API/Sample.GraphQL.API.csproj`
- [x] 2.4 Remove the `<PackageReference Include="Microsoft.VisualStudio.Azure.Containers.Tools.Targets" ... />` from `src/Sample.GraphQL.API/Sample.GraphQL.API.csproj`
- [x] 2.5 Run `dotnet build src/Sample.GraphQL.API.sln` and verify zero errors

## Task 3: Clean Up Launch Settings
- [x] 3.1 Remove the `Docker` profile from `src/Sample.GraphQL.API/Properties/launchSettings.json`
- [x] 3.2 Remove the `IIS Express` profile and the `iisSettings` section
- [x] 3.3 Change `launchUrl` from `swagger` to `graphql` in the `http` and `https` profiles
- [x] 3.4 Verify the `http` profile retains `applicationUrl` of `http://localhost:5055` and the `https` profile retains `https://localhost:7196;http://localhost:5055`

## Task 4: Verify NuGet Packages Are Up to Date
- [x] 4.1 Check all four `.csproj` files for latest stable NuGet package versions and update if newer versions are available
- [x] 4.2 Run `dotnet build src/Sample.GraphQL.API.sln` and verify zero errors after any updates

## Task 5: Document License Audit
- [x] 5.1 Verify all NuGet packages across the solution use permissive open-source licenses (MIT, Apache 2.0, BSD) and confirm the audit table in the design document is accurate

## Task 6: Create Unit Test Project
- [x] 6.1 Create `src/Sample.GraphQL.Tests/Sample.GraphQL.Tests.csproj` as an xUnit test project targeting `net10.0` with references to `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`, and `FsCheck.Xunit`
- [x] 6.2 Add a `<ProjectReference>` to `Sample.GraphQL.Domain` in the test project
- [x] 6.3 Add the test project to `src/Sample.GraphQL.API.sln` using `dotnet sln add`

## Task 7: Implement MovieEntity Tests
- [x] 7.1 Create `src/Sample.GraphQL.Tests/MovieEntityTests.cs` with a property-based test for Property 1 (Create round-trip: all properties assigned correctly and Id is non-empty)
- [x] 7.2 Add a property-based test for Property 2 (two Create calls produce distinct Ids)
- [x] 7.3 Run `dotnet test src/Sample.GraphQL.API.sln` and verify all MovieEntity tests pass

## Task 8: Implement ShowtimeEntity Tests
- [x] 8.1 Create `src/Sample.GraphQL.Tests/ShowtimeEntityTests.cs` with a property-based test for Property 3 (Create assigns movie and sessionDate)
- [x] 8.2 Add an example-based test that verifies `ShowtimeEntity.Create()` throws `ArgumentNullException` when movie is null
- [x] 8.3 Add a property-based test for Property 4 (ReserveSeats rejects multi-row seats)
- [x] 8.4 Add a property-based test for Property 5 (ReserveSeats rejects non-contiguous seats)
- [x] 8.5 Run `dotnet test src/Sample.GraphQL.API.sln` and verify all ShowtimeEntity tests pass

## Task 9: Implement ShowtimeSeatEntity Tests
- [x] 9.1 Create `src/Sample.GraphQL.Tests/ShowtimeSeatEntityTests.cs` with a property-based test for Property 6 (Create initializes Purchased=false and ReservationTime=null)
- [x] 9.2 Add a property-based test for Property 7 (SetReserved sets ReservationTime to non-null)
- [x] 9.3 Add a property-based test for Property 8 (purchased seat rejects both SetPurchased and SetReserved)
- [x] 9.4 Add an example-based test that verifies `SetReserved()` throws `InvalidOperationException` within the 10-minute cooldown period
- [x] 9.5 Run `dotnet test src/Sample.GraphQL.API.sln` and verify all ShowtimeSeatEntity tests pass

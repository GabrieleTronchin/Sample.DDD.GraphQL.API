# Product Overview

Sample GraphQL API is a cinema showtime management API built with ASP.NET Core and Hot Chocolate. It exposes a GraphQL endpoint for querying movie showtimes with support for filtering, sorting, and cursor-based pagination. A REST endpoint is also available under `/v1/cinema`.

The domain models a cinema system with movies, showtimes, and seat reservations. Seats can be reserved (with a cooldown period) and purchased. The database is in-memory (EF Core InMemory provider) and seeded on startup with sample Dune movie data.

The GraphQL playground is available at `http://localhost:5055/graphql/` during development.

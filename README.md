# Sample GraphQL API

This app is a sample API that exposes GraphQL. 
I utilized the Hot Chocolate NuGet package for this purpose. With this package, we can also filter the endpoints.

The implementation uses Minimal API. 

The server architecture follows a kind of Domain-Driven Design (DDD), and the database is in-memory for testing purposes.

When you start the app, you can use the built-in GraphQL playground by accessing http://localhost:5050/graphql/.

Here's an image of what you'll find:

![GraphQL Playground](image_url_here)

## Selecting and Expanding

To create your first GraphQL query, you can use the following example:

```graphql
{
  all {
    id
    movieId
    movie {
      title
    }
  } 
}


## Filtering

Filtering functionality is yet to be implemented.

You can refer to the Hot Chocolate documentation for guidance on implementing filtering: [Hot Chocolate - Fetching Data: Filtering](https://chillicream.com/docs/hotchocolate/v13/fetching-data/filtering)
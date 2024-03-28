using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sample.GraphQL.Domain.Repository;

namespace Sample.GraphQL.Application.Endpoints;

public static class CinemaEndpoint
{
    public static void AddEnpoints(this IEndpointRouteBuilder app)
    {

        app.MapGet("/Showtimes", async (IShowtimesRepository showtimesRepository) =>
        {
            return await showtimesRepository.GetAsync(null, CancellationToken.None);
        });


    }

}

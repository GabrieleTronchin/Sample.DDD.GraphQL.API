using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Sample.GraphQL.Application.Endpoints;

public static class CinemaEndpoint
{
    public static void AddEnpoints(this IEndpointRouteBuilder app)
    {

        app.MapGet("/Test", async () =>
        {
            throw new NotImplementedException();
        });


    }

}

using Microsoft.Extensions.DependencyInjection;
using Sample.GraphQL.Persistence.Context;


namespace Sample.GraphQL.Application
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            services
            .AddGraphQLServer()
            .RegisterDbContext<CinemaDbContext>()
            .AddQueryType<ShowtimesQuery>()
            .AddFiltering(); 

            return services;

        }

    }
}

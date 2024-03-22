using Microsoft.Extensions.DependencyInjection;


namespace Sample.GraphQL.Application
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            services
            .AddGraphQLServer()
            .AddQueryType<ShowtimesQuery>();

            return services;

        }

    }
}

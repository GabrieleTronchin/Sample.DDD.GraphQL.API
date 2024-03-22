using Microsoft.EntityFrameworkCore.Diagnostics;
using Sample.GraphQL.Domain.Repository;
using Sample.GraphQL.Persistence.Context;
using Sample.GraphQL.Persistence.Repository;

namespace Sample.GraphQL.Persistence
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IShowtimesRepository, ShowtimesRepository>();


            services.AddDbContext<CinemaDbContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            },ServiceLifetime.Singleton);

            return services;

        }

    }
}

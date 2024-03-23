using Sample.GraphQL.Domain;
using Sample.GraphQL.Persistence.Context;

namespace Sample.GraphQL.Persistence;

public static class SeedDb
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CinemaDbContext>() ?? throw new NullReferenceException($"Cannot find any service for {nameof(CinemaDbContext)}");
        context.Database.EnsureCreated();

        var dune2Movie = MovieEntity.Create("Dune Part 2",
            "Timothée Chalamet , Zendaya , Rebecca Ferguson , Josh Brolin , Austin Butler",
            "IMDB-02",
            DateTime.UtcNow.AddDays(-15));

        context.Movies.Add(dune2Movie);


        var dune1Movie = MovieEntity.Create("Dune Part 1",
            "Timothée Chalamet , Zendaya , Rebecca Ferguson , Josh Brolin , Austin Butler",
            "IMDB-01",
            DateTime.UtcNow.AddDays(-10));

        context.Movies.Add(dune1Movie);

        var showTime1 = ShowtimeEntity.Create(dune1Movie, DateTime.UtcNow);
        context.Showtimes.Add(showTime1);

        var showTime2 = ShowtimeEntity.Create(dune2Movie, DateTime.UtcNow);
        context.Showtimes.Add(showTime2);

        var showTime3 = ShowtimeEntity.Create(dune1Movie, DateTime.UtcNow);
        context.Showtimes.Add(showTime3);

        var showTime4 = ShowtimeEntity.Create(dune2Movie, DateTime.UtcNow);
        context.Showtimes.Add(showTime3);

        context.SaveChanges();
    }

    private static List<Seat> GenerateSeats(short rows, short seatsPerRow)
    {
        var seats = new List<Seat>();
        for (short r = 1; r <= rows; r++)
            for (short s = 1; s <= seatsPerRow; s++)
                seats.Add(new Seat(r, s));
        return seats;
    }
}


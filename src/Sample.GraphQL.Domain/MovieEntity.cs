namespace Sample.GraphQL.Domain;

public class MovieEntity
{
    private MovieEntity()
    {

    }

    public static MovieEntity Create(string title, string stars, string imdbId, DateTime releaseDate)
    {
        return new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            ImdbId = imdbId,
            Stars = stars,
            ReleaseDate = releaseDate
        };
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string ImdbId { get; private set; } = string.Empty;
    public string Stars { get; private set; } = string.Empty;
    public DateTime ReleaseDate { get; private set; }
}


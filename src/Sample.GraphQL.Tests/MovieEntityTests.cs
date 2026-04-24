using FsCheck;
using FsCheck.Xunit;
using Sample.GraphQL.Domain;

namespace Sample.GraphQL.Tests;

/// <summary>
/// Property-based tests for MovieEntity.Create() factory method.
/// </summary>
public class MovieEntityTests
{
    /// <summary>
    /// Property 1: MovieEntity.Create() round-trip — all properties assigned correctly and Id is non-empty.
    /// For any valid title, stars, imdbId, and releaseDate, calling Create() produces an entity
    /// where all properties match the inputs and Id != Guid.Empty.
    /// **Validates: Requirements 10.1, 10.2**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool Create_RoundTrip_AllPropertiesAssignedAndIdNonEmpty(
        NonNull<string> title,
        NonNull<string> stars,
        NonNull<string> imdbId,
        DateTime releaseDate)
    {
        var movie = MovieEntity.Create(title.Get, stars.Get, imdbId.Get, releaseDate);

        return movie.Title == title.Get
            && movie.Stars == stars.Get
            && movie.ImdbId == imdbId.Get
            && movie.ReleaseDate == releaseDate
            && movie.Id != Guid.Empty;
    }

    /// <summary>
    /// Property 2: MovieEntity.Create() produces unique Ids.
    /// For any two calls to Create() with arbitrary valid inputs, the resulting entities have distinct Id values.
    /// **Validates: Requirements 10.3**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool Create_TwoCalls_ProduceDistinctIds(
        NonNull<string> title1,
        NonNull<string> stars1,
        NonNull<string> imdbId1,
        DateTime releaseDate1,
        NonNull<string> title2,
        NonNull<string> stars2,
        NonNull<string> imdbId2,
        DateTime releaseDate2)
    {
        var movie1 = MovieEntity.Create(title1.Get, stars1.Get, imdbId1.Get, releaseDate1);
        var movie2 = MovieEntity.Create(title2.Get, stars2.Get, imdbId2.Get, releaseDate2);

        return movie1.Id != movie2.Id;
    }
}

using FsCheck;
using FsCheck.Xunit;
using Sample.GraphQL.Domain;
using Xunit;

namespace Sample.GraphQL.Tests;

/// <summary>
/// Property-based and example-based tests for ShowtimeEntity.
/// </summary>
public class ShowtimeEntityTests
{
    /// <summary>
    /// Property 3: ShowtimeEntity.Create() assigns movie and sessionDate.
    /// For any valid MovieEntity and DateTime sessionDate, calling ShowtimeEntity.Create(movie, sessionDate)
    /// SHALL produce an entity where Movie is the provided movie and SessionDate is the provided date.
    /// **Validates: Requirements 11.1**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool Create_AssignsMovieAndSessionDate(
        NonNull<string> title,
        NonNull<string> stars,
        NonNull<string> imdbId,
        DateTime releaseDate,
        DateTime sessionDate)
    {
        var movie = MovieEntity.Create(title.Get, stars.Get, imdbId.Get, releaseDate);
        var showtime = ShowtimeEntity.Create(movie, sessionDate);

        return showtime.Movie == movie
            && showtime.SessionDate == sessionDate;
    }

    /// <summary>
    /// Example-based test: ShowtimeEntity.Create() throws ArgumentNullException when movie is null.
    /// **Validates: Requirements 11.2**
    /// </summary>
    [Fact]
    public void Create_NullMovie_ThrowsArgumentNullException()
    {
        var sessionDate = DateTime.UtcNow;

        var ex = Assert.Throws<ArgumentNullException>(() => ShowtimeEntity.Create(null!, sessionDate));
        Assert.Equal("movie", ex.ParamName);
    }

    /// <summary>
    /// Property 4: ReserveSeats() rejects multi-row seats.
    /// For any collection of Seat objects spanning two or more distinct RowNumber values,
    /// calling ReserveSeats(seats) SHALL throw InvalidOperationException.
    /// **Validates: Requirements 11.3**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool ReserveSeats_MultipleRows_ThrowsInvalidOperationException(
        PositiveInt row1,
        PositiveInt row2,
        PositiveInt seatNum1,
        PositiveInt seatNum2)
    {
        var r1 = (short)(row1.Get % 100 + 1);
        var r2 = (short)(row2.Get % 100 + 1);

        // Ensure distinct rows
        if (r1 == r2) r2 = (short)(r1 % 100 + 1);
        if (r1 == r2) return true; // Skip degenerate case

        var s1 = (short)(seatNum1.Get % 50 + 1);
        var s2 = (short)(seatNum2.Get % 50 + 1);

        var seats = new List<Seat>
        {
            new Seat(r1, s1),
            new Seat(r2, s2)
        };

        var movie = MovieEntity.Create("Test", "Stars", "tt0000001", DateTime.UtcNow);
        var showtime = ShowtimeEntity.Create(movie, DateTime.UtcNow);

        try
        {
            showtime.ReserveSeats(seats);
            return false; // Should have thrown
        }
        catch (InvalidOperationException)
        {
            return true;
        }
    }

    /// <summary>
    /// Property 5: ReserveSeats() rejects non-contiguous seats.
    /// For any collection of Seat objects in the same row where the sorted seat numbers have
    /// at least one gap greater than 1, calling ReserveSeats(seats) SHALL throw InvalidOperationException.
    /// **Validates: Requirements 11.4**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool ReserveSeats_NonContiguousSeats_ThrowsInvalidOperationException(
        PositiveInt row,
        PositiveInt baseSeat,
        PositiveInt gap)
    {
        var r = (short)(row.Get % 100 + 1);
        var s1 = (short)(baseSeat.Get % 40 + 1);
        var gapVal = gap.Get % 9 + 2; // gap between 2 and 10
        var s2 = (short)(s1 + gapVal);

        var seats = new List<Seat>
        {
            new Seat(r, s1),
            new Seat(r, s2)
        };

        var movie = MovieEntity.Create("Test", "Stars", "tt0000001", DateTime.UtcNow);
        var showtime = ShowtimeEntity.Create(movie, DateTime.UtcNow);

        try
        {
            showtime.ReserveSeats(seats);
            return false; // Should have thrown
        }
        catch (InvalidOperationException)
        {
            return true;
        }
    }
}

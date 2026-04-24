using FsCheck;
using FsCheck.Xunit;
using Sample.GraphQL.Domain;
using Xunit;

namespace Sample.GraphQL.Tests;

/// <summary>
/// Property-based and example-based tests for ShowtimeSeatEntity.
/// </summary>
public class ShowtimeSeatEntityTests
{
    /// <summary>
    /// Property 6: ShowtimeSeatEntity.Create() initializes default state.
    /// For any valid Seat and Guid showtimeId, calling Create(seat, showtimeId)
    /// SHALL produce an entity where Purchased == false and ReservationTime == null.
    /// **Validates: Requirements 12.1**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool Create_InitializesPurchasedFalseAndReservationTimeNull(
        short rowNumber,
        short seatNumber,
        Guid showtimeId)
    {
        var seat = new Seat(rowNumber, seatNumber);
        var entity = ShowtimeSeatEntity.Create(seat, showtimeId);

        return entity.Purchased == false
            && entity.ReservationTime == null;
    }

    /// <summary>
    /// Property 7: SetReserved() activates reservation time.
    /// For any newly created ShowtimeSeatEntity (not purchased, not previously reserved),
    /// calling SetReserved() SHALL set ReservationTime to a non-null value.
    /// **Validates: Requirements 12.2**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool SetReserved_SetsReservationTimeToNonNull(
        short rowNumber,
        short seatNumber,
        Guid showtimeId)
    {
        var seat = new Seat(rowNumber, seatNumber);
        var entity = ShowtimeSeatEntity.Create(seat, showtimeId);

        entity.SetReserved();

        return entity.ReservationTime != null;
    }

    /// <summary>
    /// Property 8: Purchased seat rejects further state changes.
    /// For any ShowtimeSeatEntity that has been purchased, calling SetPurchased()
    /// SHALL throw InvalidOperationException, and calling SetReserved()
    /// SHALL throw InvalidOperationException.
    /// **Validates: Requirements 12.3, 12.4**
    /// </summary>
    [Property(MaxTest = 100)]
    public bool PurchasedSeat_RejectsBothSetPurchasedAndSetReserved(
        short rowNumber,
        short seatNumber,
        Guid showtimeId)
    {
        var seat = new Seat(rowNumber, seatNumber);
        var entity = ShowtimeSeatEntity.Create(seat, showtimeId);
        entity.SetPurchased();

        bool setPurchasedThrew;
        bool setReservedThrew;

        try
        {
            entity.SetPurchased();
            setPurchasedThrew = false;
        }
        catch (InvalidOperationException)
        {
            setPurchasedThrew = true;
        }

        try
        {
            entity.SetReserved();
            setReservedThrew = false;
        }
        catch (InvalidOperationException)
        {
            setReservedThrew = true;
        }

        return setPurchasedThrew && setReservedThrew;
    }

    /// <summary>
    /// Example-based test: SetReserved() throws InvalidOperationException within the 10-minute cooldown period.
    /// **Validates: Requirements 12.5**
    /// </summary>
    [Fact]
    public void SetReserved_WithinCooldownPeriod_ThrowsInvalidOperationException()
    {
        var seat = new Seat(1, 1);
        var entity = ShowtimeSeatEntity.Create(seat, Guid.NewGuid());

        // First reservation sets ReservationTime to DateTime.UtcNow
        entity.SetReserved();

        // Second call is immediately after, well within the 10-minute cooldown
        var ex = Assert.Throws<InvalidOperationException>(() => entity.SetReserved());
        Assert.Contains("10", ex.Message);
    }
}

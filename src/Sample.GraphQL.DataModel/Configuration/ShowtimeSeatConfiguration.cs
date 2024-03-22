using Sample.GraphQL.Domain;

namespace Sample.GraphQL.Persistence.Configuration;

internal class ShowtimeSeatConfiguration : IEntityTypeConfiguration<ShowtimeSeatEntity>
{

    public void Configure(EntityTypeBuilder<ShowtimeSeatEntity> builder)
    {
        builder.HasKey(t => t.Id);
        builder.OwnsOne(a => a.Seat, seatBuilder =>
        {
            seatBuilder.Property(s => s.RowNumber);
            seatBuilder.Property(s => s.SeatNumber);
        });
    }
}


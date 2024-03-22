using Sample.GraphQL.Domain;

namespace Sample.GraphQL.Persistence.Configuration;

internal class ShowtimeConfiguration : IEntityTypeConfiguration<ShowtimeEntity>
{
    public void Configure(EntityTypeBuilder<ShowtimeEntity> builder)
    {
        builder.HasKey(t => t.Id);

        // FK to ShowtimeSeat 1-N
        builder.HasMany(s => s.Seats)
            .WithOne()
            .HasForeignKey(s => s.ShowtimeId);

        //Add entity db constraints
        // FK to ShowtimeSeat 1-N
        builder.HasOne(s => s.Movie)
               .WithMany()
               .HasForeignKey(x => x.MovieId);
    }
}


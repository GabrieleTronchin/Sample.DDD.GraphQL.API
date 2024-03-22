using Sample.GraphQL.Domain;

namespace Sample.GraphQL.Persistence.Context
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<ShowtimeEntity> Showtimes { get; set; }
        public DbSet<ShowtimeSeatEntity> ShowtimesSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly);
        }

    }
}

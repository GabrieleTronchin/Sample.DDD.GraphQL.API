using Sample.GraphQL.Domain;
using Sample.GraphQL.Domain.Repository;
using Sample.GraphQL.Persistence.Context;
using System.Linq.Expressions;

namespace Sample.GraphQL.Persistence.Repository;

internal class ShowtimesRepository : IShowtimesRepository
{
    private readonly CinemaDbContext _context;

    public ShowtimesRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public DbSet<ShowtimeEntity> Entity { get => _context.Showtimes; }

    public async Task<ShowtimeEntity?> GetAsync(Guid id, CancellationToken cancel)
    {
        return await _context.Showtimes
            .Include(x => x.Movie)
            .Include(x => x.Seats)
            .SingleOrDefaultAsync(x => x.Id == id, cancel) ??
             throw new InvalidOperationException($"System could not find any {nameof(ShowtimeEntity.Id)} with value {id}");
    }

    public async Task<IEnumerable<ShowtimeEntity>> GetAsync(CancellationToken cancel)
    {
            return await _context.Showtimes
            .Include(x => x.Movie)
            .Include(x => x.Seats)
            .ToListAsync(cancel);

    }

    public async Task<IEnumerable<ShowtimeEntity>> GetAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel)
    {

        return await _context.Showtimes
            .Include(x => x.Movie)
            .Include(x => x.Seats)
            .Where(filter)
            .ToListAsync(cancel);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(ShowtimeEntity entity)
    {
        await _context.AddAsync(entity);
    }
}

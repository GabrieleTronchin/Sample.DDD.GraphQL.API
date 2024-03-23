using System.Linq.Expressions;

namespace Sample.GraphQL.Domain.Repository;

public interface IShowtimesRepository : IRepository<ShowtimeEntity>
{

    Task<ShowtimeEntity?> GetAsync(Guid id, CancellationToken cancel);
    Task<IEnumerable<ShowtimeEntity>> GetAsync(CancellationToken cancel);
    Task<IEnumerable<ShowtimeEntity>> GetAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel);
}
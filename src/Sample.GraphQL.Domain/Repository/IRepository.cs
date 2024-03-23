
namespace Sample.GraphQL.Domain.Repository;

public interface IRepository<in T> where T : class
{
    Task SaveChangesAsync();

    Task AddAsync(T entity);
    Task<ShowtimeEntity?> GetAsync(Guid id, CancellationToken cancel);
}

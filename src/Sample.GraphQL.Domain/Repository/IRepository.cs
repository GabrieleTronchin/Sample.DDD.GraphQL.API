namespace Sample.GraphQL.Domain.Repository;

public interface IRepository<in T> where T : class
{
    Task SaveChangesAsync();

    Task AddAsync(T entity);
}

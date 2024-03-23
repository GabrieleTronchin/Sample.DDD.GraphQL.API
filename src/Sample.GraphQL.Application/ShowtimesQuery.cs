using HotChocolate.Data;
using Sample.GraphQL.Domain;
using Sample.GraphQL.Domain.Repository;

namespace Sample.GraphQL.Application
{
    public class ShowtimesQuery(IShowtimesRepository showtimesRepository)
    {
        /// <summary>
        /// Plane Get with GraphQL without filtering
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<ShowtimeEntity>> GetAll()
        {
            var result = await showtimesRepository.GetAsync(default);
            return result.AsQueryable();
        }

        /// <summary>
        /// Get with filters and paging
        /// </summary>
        /// <returns></returns>
        [UsePaging(IncludeTotalCount =true, DefaultPageSize =50)]
        [UseFiltering]
        public async Task<IQueryable<ShowtimeEntity>> GetShowTimes()
        {
            var result = await showtimesRepository.GetAsync(default);
            return result.AsQueryable();
        }
    }
}

using Sample.GraphQL.Domain;
using Sample.GraphQL.Domain.Repository;

namespace Sample.GraphQL.Application
{
    public class ShowtimesQuery(IShowtimesRepository showtimesRepository)
    {
        public async Task<IQueryable<ShowtimeEntity>> GetAll()
        {

            //TODO just a sample
            var result = await showtimesRepository.GetAllAsync(null, default);
            return result.AsQueryable();

        }


    }
}

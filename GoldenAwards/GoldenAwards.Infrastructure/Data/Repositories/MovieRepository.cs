using GoldenAwards.Domain.Interfaces.Repositories;
using GoldenAwards.Domain.Models.Movies;
using GoldenAwards.Infrastructure.Data.Context;

namespace GoldenAwards.Infrastructure.Data.Repositories
{
    public class MovieRepository : Repository<Movie, Guid, ContextDb>, IMovieRepository
    {
        public MovieRepository(ContextDb context) : base(context)
        {
        }
    }
}

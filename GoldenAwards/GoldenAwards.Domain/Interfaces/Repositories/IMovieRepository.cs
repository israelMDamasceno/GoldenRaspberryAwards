using GoldenAwards.Domain.Models.Movies;

namespace GoldenAwards.Domain.Interfaces.Repositories
{
    public interface IMovieRepository : IRepository<Movie, Guid>
    {
    }
}

using GoldenAwards.Domain.Models.Movies;

namespace GoldenAwards.Domain.Interfaces.Services
{
    public interface IMoviesService
    {
        Task<bool> SaveMoviesAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken);
        Task<bool> SaveMoviesAsync(string pathToCsv, CancellationToken cancellationToken);
    }
}

using GoldenAwards.Domain.Interfaces.Services;
using MediatR;

namespace GoldenAwards.Application.Commands.Movie.SaveMovies
{
    public class SaveMovieCommandHandle : IRequestHandler<SaveMovieCommand, bool>
    {
        private readonly IMoviesService moviesService;

        public SaveMovieCommandHandle(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<bool> Handle(SaveMovieCommand request, CancellationToken cancellationToken)
        {
            return await moviesService.SaveMoviesAsync(request.PathToCsvFile, cancellationToken);
        }
    }
}

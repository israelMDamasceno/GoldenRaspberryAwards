using MediatR;

namespace GoldenAwards.Application.Commands.Movie.SaveMovies
{
    public class SaveMovieCommand : IRequest<bool>
    {
        public string PathToCsvFile { get; set; } = string.Empty;
    }
}

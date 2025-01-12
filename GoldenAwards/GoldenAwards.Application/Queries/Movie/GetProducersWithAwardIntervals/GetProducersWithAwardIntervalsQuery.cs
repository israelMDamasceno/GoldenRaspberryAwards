using GoldenAwards.Domain.Dtos.Movie;
using MediatR;

namespace GoldenAwards.Application.Queries.Movie.GetProducersWithAwardIntervals
{
    public class GetProducersWithAwardIntervalsQuery : IRequest<AwardIntervalWinDto>
    {
    }
}

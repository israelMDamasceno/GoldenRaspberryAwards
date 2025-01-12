using GoldenAwards.Domain.Dtos.Movie;
using GoldenAwards.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoldenAwards.Application.Queries.Movie.GetProducersWithAwardIntervals
{
    public class GetProducersWithAwardIntervalsQueryHandle : IRequestHandler<GetProducersWithAwardIntervalsQuery, AwardIntervalWinDto>
    {
        private readonly ContextDb context;
        public GetProducersWithAwardIntervalsQueryHandle(ContextDb context)
        {
            this.context = context;
        }

        private static AwardWinDto GetWinIntervalForProducer(string producer, IEnumerable<int> years, bool isMinInterval)
        {
            var orderedYears = years.OrderBy(x => x).ToList();
            var bestInterval = isMinInterval ? int.MaxValue : int.MinValue;
            var previousWin = 0;
            var followingWin = 0;

            for (int i = 0; i < orderedYears.Count - 1; i++)
            {
                var interval = orderedYears[i + 1] - orderedYears[i];
                var isBetterInterval = isMinInterval ? interval < bestInterval : interval > bestInterval;

                if (isBetterInterval)
                {
                    bestInterval = interval;
                    previousWin = orderedYears[i];
                    followingWin = orderedYears[i + 1];
                }
            }

            return new AwardWinDto(producer, bestInterval, previousWin, followingWin);
        }

        public async Task<AwardIntervalWinDto> Handle(GetProducersWithAwardIntervalsQuery request, CancellationToken cancellationToken)
        {
            var groupedWis = await context.Movies
                                .Where(movie => movie.IsWinner.HasValue && movie.IsWinner.Value)
                                .GroupBy(movie => movie.Producers)
                                .Where(movie => movie.Select(x => x.Year).Count() > 1)
                                .Select(g => new
                                {
                                    MinWin = GetWinIntervalForProducer(g.Key, g.Select(x => x.Year), true),
                                    MaxWin = GetWinIntervalForProducer(g.Key, g.Select(x => x.Year), false)
                                })
                                .ToListAsync(cancellationToken);



            var minInterval = groupedWis.Min(x => x.MinWin.Interval);
            var maxInterval = groupedWis.Max(x => x.MaxWin.Interval);

            var minWinners = groupedWis.Where(x => x.MinWin.Interval == minInterval).Select(x => x.MinWin).ToList();
            var maxWinners = groupedWis.Where(x => x.MaxWin.Interval == maxInterval).Select(x => x.MaxWin).ToList();

            return new AwardIntervalWinDto(minWinners, maxWinners);
        }
    }

}

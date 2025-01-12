using GoldenAwards.Application.Queries.Movie.GetProducersWithAwardIntervals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoldenAwards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardIntervalController : ControllerBase
    {
        private readonly ISender sender;

        public AwardIntervalController(ISender sender)
        {
            this.sender = sender;
        }

        /// <summary>
        /// Obtém o produtor com o maior intervalo entre dois prêmios consecutivos 
        /// e o que obteve dois prêmios mais rapidamente.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("award-intervals")]
        public async Task<IActionResult> GetProducersWithAwardIntervals(CancellationToken cancellationToken)
        {
            var minMaxWinners = await sender.Send(new GetProducersWithAwardIntervalsQuery(), cancellationToken);

            if (minMaxWinners == null || !minMaxWinners.Min.Any() || !minMaxWinners.Max.Any())
                return NotFound("No producers with award intervals found.");

            return Ok(minMaxWinners);
        }
    }
}

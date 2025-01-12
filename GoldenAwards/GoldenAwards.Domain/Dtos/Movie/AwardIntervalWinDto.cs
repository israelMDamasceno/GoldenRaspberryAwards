namespace GoldenAwards.Domain.Dtos.Movie
{
    public class AwardIntervalWinDto
    {
        public AwardIntervalWinDto(IEnumerable<AwardWinDto> min, IEnumerable<AwardWinDto> max)
        {
            Min = min ?? throw new ArgumentNullException(nameof(min));
            Max = max ?? throw new ArgumentNullException(nameof(max));
        }
        public IEnumerable<AwardWinDto> Min { get; }
        public IEnumerable<AwardWinDto> Max { get; }
    }
}

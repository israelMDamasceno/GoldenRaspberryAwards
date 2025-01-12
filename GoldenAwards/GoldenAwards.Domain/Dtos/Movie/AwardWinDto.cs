namespace GoldenAwards.Domain.Dtos.Movie
{
    public class AwardWinDto
    {
        public AwardWinDto(string producer, int interval, int previous, int following)
        {
            Producer = producer;
            Interval = interval;
            Previous = previous;
            Following = following;
        }
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int Previous { get; set; }
        public int Following { get; set; }
    }
}

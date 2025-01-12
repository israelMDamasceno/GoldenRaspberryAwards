namespace GoldenAwards.Domain.Dtos.Movie
{
    public class MovieDto
    {
        public MovieDto(int year, string title, string studios, string producers, bool winner)
        {
            Year = year;
            Title = title;
            Studios = studios;
            Producers = producers;
            Winner = winner;
        }
        public int Year { get; }
        public string Title { get; }
        public string Studios { get; }
        public string Producers { get; }
        public bool Winner { get; }
    }
}

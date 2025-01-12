using GoldenAwards.Domain.Dtos.Movie;
using System.Net;
using System.Net.Http.Json;

namespace GoldenAwards.Api.TestIntegration.Tests
{
    public class AwardIntervalTests
    {
        [Theory]
        [InlineData("../../../CsvFile/movielist.csv", 1, 1, 1, 13)]
        public async Task GetWinners_WhenCsfFileIsNotEmpty_ReturnsWinners(string pathToCsv, int minTotalCount, int maxTotalCount, int minInterval, int maxInterval)
        {
            // Arrange

            var factory = new ConfigureWebApplicationFactory(pathToCsv);
            var client = factory.CreateClient();

            //// Act
            var response = await client.GetAsync("api/AwardInterval/award-intervals");

            //// Assert
            response.EnsureSuccessStatusCode();

            var minMaxWinnersDto = await response.Content.ReadFromJsonAsync<AwardIntervalWinDto>();

            Assert.NotNull(minMaxWinnersDto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(minTotalCount, minMaxWinnersDto.Min.Count());
            Assert.Equal(minInterval, minMaxWinnersDto.Min.First().Interval);
            Assert.Equal(maxTotalCount, minMaxWinnersDto.Max.Count());
            Assert.Equal(maxInterval, minMaxWinnersDto.Max.First().Interval);

        }


    }
}

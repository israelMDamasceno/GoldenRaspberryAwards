using AutoMapper;
using GoldenAwards.Domain.Dtos.Movie;
using GoldenAwards.Domain.Models.Movies;

namespace GoldenAwards.Application.MapperProfiles
{
    internal class MovieProfile : Profile
    {   
        public MovieProfile()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
        }
    }
}

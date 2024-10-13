using AutoMapper;
using Movies.API.Dtos;
using Movies.DAL.Entity;

namespace Movies.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenreDto, Genre>();
            CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.genre.Name))
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreID));
            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}

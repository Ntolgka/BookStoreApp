using AutoMapper;
using BookStoreApp.Model;
using BookStoreApp.Schema.Book;
using BookStoreApp.Schema.Genre;

namespace BookStoreApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<Book, GetBookDetailDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("yyyy-MM-dd")));
            CreateMap<Book, GetBooksDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("yyyy-MM-dd")));
            CreateMap<UpdateBookDto, Book>();
        
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<Genre, GetGenreDetailDto>();
            CreateMap<Genre, GetGenresDto>();
            CreateMap<UpdateGenreDto, Genre>();
        }
    }
}

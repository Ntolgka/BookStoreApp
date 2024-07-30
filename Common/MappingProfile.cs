using AutoMapper;
using BookStoreApp.Model;
using static BookStoreApp.BooksOperations.CreateBookCommand;
using static BookStoreApp.BooksOperations.GetBookDetailQuery;
using static BookStoreApp.BooksOperations.GetBooksQuery;


namespace BookStoreApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>().ReverseMap();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy"))).ReverseMap();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy"))).ReverseMap();
        }
    }
}

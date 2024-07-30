using AutoMapper;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.BooksOperations;

public class GetBooksQuery
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBooksQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetBooksDto>> Handle()
    {
        var books = await _unitOfWork.BookRepository.GetAll("Genre"); 
        return _mapper.Map<List<GetBooksDto>>(books);
    }
}
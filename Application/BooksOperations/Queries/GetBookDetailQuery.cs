using AutoMapper;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.BooksOperations.Queries;

public class GetBookDetailQuery
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public int BookId { get; set; }

    public GetBookDetailQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetBookDetailDto> HandleAsync()
    {
        var book = await _unitOfWork.BookRepository.GetById(BookId, "Genre");

        if (book is null)
        {
            throw new InvalidOperationException("Book not found.");
        }

        return _mapper.Map<GetBookDetailDto>(book);
    }
}

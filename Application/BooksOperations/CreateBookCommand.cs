using AutoMapper;
using BookStoreApp.Model;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.BooksOperations;

public class CreateBookCommand
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateBookDto Model { get; set; }

    public CreateBookCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task HandleAsync()
    {
        var existingBook = await _unitOfWork.BookRepository.FirstOrDefault(x => x.Title == Model.Title);

        if (existingBook is not null)
        {
            throw new InvalidOperationException("This book already exists...");
        }

        var book = _mapper.Map<Book>(Model);

        await _unitOfWork.BookRepository.Insert(book);
        await _unitOfWork.Complete();
    }
}

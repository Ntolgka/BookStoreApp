using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.BooksOperations;

public class DeleteBookCommand
{
    public int BookId { get; set; }

    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {   
        var book = await _unitOfWork.BookRepository.GetById(BookId);

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.Complete();
    }
}

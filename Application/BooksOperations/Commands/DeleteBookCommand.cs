using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.BooksOperations.Commands;

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
            throw new InvalidOperationException("Book does not exist.");
        }

        _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.Complete();
    }
}

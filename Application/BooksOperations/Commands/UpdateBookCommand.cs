using AutoMapper;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.BooksOperations.Commands;

public class UpdateBookCommand
{
    public int BookId { get; set; }
    public UpdateBookDto Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommand(IUnitOfWork unitOfWork)
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

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        _unitOfWork.BookRepository.Update(book);
        await _unitOfWork.Complete();
    }
}
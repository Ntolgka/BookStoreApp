using BookStoreApp.Application.BooksOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Book;

public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}

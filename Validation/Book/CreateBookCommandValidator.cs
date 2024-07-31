using BookStoreApp.Application.BooksOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Book;

public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(4).WithMessage("Title must be at least 4 characters long.");

        RuleFor(command => command.Model.PageCount)
            .GreaterThan(0).WithMessage("PageCount must be greater than 0.");

        RuleFor(command => command.Model.PublishDate)
            .NotEmpty().WithMessage("PublishDate is required.")
            .LessThan(DateTime.Now).WithMessage("PublishDate cannot be in the future.");

        RuleFor(command => command.Model.GenreId)
            .GreaterThan(0).WithMessage("GenreId must be greater than 0.");

        RuleFor(command => command.Model.AuthorId)
            .GreaterThan(0).WithMessage("AuthorId must be greater than 0.");
    }
}

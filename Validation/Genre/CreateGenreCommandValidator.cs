using BookStoreApp.Application.BooksOperations;
using BookStoreApp.Application.GenreOperations;
using FluentValidation;

namespace BookStoreApp.Validation.Genre;

public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Id).GreaterThan(0);
        RuleFor(command => command.Model.IsActive).NotEmpty();
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
    }
}

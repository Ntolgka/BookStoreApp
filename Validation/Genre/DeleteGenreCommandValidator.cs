using BookStoreApp.Application.GenreOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Genre;

public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(command => command.genreId).GreaterThan(0);
    }
}

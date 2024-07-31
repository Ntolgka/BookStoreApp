using BookStoreApp.Application.GenreOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Genre;

public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(command => command.genreId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
    }
}

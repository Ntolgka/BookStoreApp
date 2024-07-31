using BookStoreApp.Application.GenreOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Genre;

public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(4).WithMessage("Name must be at least 4 characters long.");

        RuleFor(command => command.Model.IsActive)
            .NotNull().WithMessage("IsActive must be specified.");
    }
}

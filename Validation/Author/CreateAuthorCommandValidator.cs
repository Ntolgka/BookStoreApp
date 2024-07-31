using BookStoreApp.Application.AuthorOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Author;

public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .LessThan(DateTime.Now).WithMessage("BirthDate cannot be in the future.");

        RuleFor(command => command.Model.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(4).WithMessage("Name must be at least 4 characters long.");

        RuleFor(command => command.Model.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MinimumLength(4).WithMessage("LastName must be at least 4 characters long.");
    }
}

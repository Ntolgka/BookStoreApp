using BookStoreApp.Application.AuthorOperations;
using FluentValidation;

namespace BookStoreApp.Validation.Author;

public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Id).GreaterThan(0);
        RuleFor(command => command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
        RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(4);
    }
}

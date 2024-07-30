using BookStoreApp.Application.AuthorOperations;
using FluentValidation;

namespace BookStoreApp.Validation.Author;

public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId).GreaterThan(0);
    }
}

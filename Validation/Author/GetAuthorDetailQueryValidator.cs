using BookStoreApp.Application.AuthorOperations.Queries;
using FluentValidation;

namespace BookStoreApp.Validation.Author;

public class GetAuthorDetailQueryValidator:AbstractValidator<GetAuthorDetailQuery>
{
    public GetAuthorDetailQueryValidator()
    {
        RuleFor(query => query.AuthorId).GreaterThan(0);
    }
}

using BookStoreApp.Application.GenreOperations;
using FluentValidation;

namespace BookStoreApp.Validation.Genre;

public class GetGenreDetailQueryValidator:AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(query => query.genreId).GreaterThan(0);
    }
}

﻿using BookStoreApp.Application.BooksOperations.Queries;
using FluentValidation;

namespace BookStoreApp.Validation.Book;

public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}

﻿using BookStoreApp.Application.BooksOperations.Commands;
using FluentValidation;

namespace BookStoreApp.Validation.Book;

public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
    }
}

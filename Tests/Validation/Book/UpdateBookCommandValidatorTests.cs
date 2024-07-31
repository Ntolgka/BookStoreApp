using BookStoreApp.Application.BooksOperations.Commands;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Book;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Book;

public class UpdateBookCommandValidatorTests
{
    private readonly UpdateBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public UpdateBookCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _validator = new UpdateBookCommandValidator();
    }

    [Fact]
    public void Validate_WhenBookIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object)
        {
            BookId = 0,
            Model = new UpdateBookDto()
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenTitleIsTooShort_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object)
        {
            BookId = 1,
            Model = new UpdateBookDto { Title = "abc", GenreId = 1 }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Model.Title);
    }

    [Fact]
    public void Validate_WhenAllPropertiesAreValid_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object)
        {
            BookId = 1,
            Model = new UpdateBookDto { Title = "Valid Title", GenreId = 1 }
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
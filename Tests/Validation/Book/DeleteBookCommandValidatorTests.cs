using BookStoreApp.Application.BooksOperations.Commands;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Book;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Book;

public class DeleteBookCommandValidatorTests
{
    private readonly DeleteBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public DeleteBookCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _validator = new DeleteBookCommandValidator();
    }

    [Fact]
    public void Validate_WhenBookIdIsZero_ShouldHaveValidationError()
    {
        var command = new DeleteBookCommand(_mockUnitOfWork.Object) { BookId = 0 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenBookIdIsNegative_ShouldHaveValidationError()
    {
        var command = new DeleteBookCommand(_mockUnitOfWork.Object) { BookId = -1 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenBookIdIsValid_ShouldNotHaveValidationError()
    {
        var command = new DeleteBookCommand(_mockUnitOfWork.Object) { BookId = 1 };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.BookId);
    }
}
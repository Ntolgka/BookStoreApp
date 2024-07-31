using BookStoreApp.Application.GenreOperations.Commands;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Genre;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Book;

public class DeleteGenreCommandValidatorTests
{
    private readonly DeleteGenreCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public DeleteGenreCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _validator = new DeleteGenreCommandValidator();
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        var command = new DeleteGenreCommand(_mockUnitOfWork.Object) { genreId = 0 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.genreId);
    }

    [Fact]
    public void Validate_WhenGenreIdIsNegative_ShouldHaveValidationError()
    {
        var command = new DeleteGenreCommand(_mockUnitOfWork.Object) { genreId = -1 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.genreId);
    }

    [Fact]
    public void Validate_WhenGenreIdIsValid_ShouldNotHaveValidationError()
    {
        var command = new DeleteGenreCommand(_mockUnitOfWork.Object) { genreId = 1 };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.genreId);
    }
}
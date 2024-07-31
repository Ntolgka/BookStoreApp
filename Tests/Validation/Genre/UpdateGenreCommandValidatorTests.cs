using BookStoreApp.Application.GenreOperations.Commands;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Genre;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Book;

public class UpdateGenreCommandValidatorTests
{
    private readonly UpdateGenreCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public UpdateGenreCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _validator = new UpdateGenreCommandValidator();
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = 0,
            Model = new UpdateGenreDto()
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.genreId);
    }

    [Fact]
    public void Validate_WhenNameIsTooShort_ShouldHaveValidationError()
    {
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = 1,
            Model = new UpdateGenreDto { Name = "abc" }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Model.Name);
    }

    [Fact]
    public void Validate_WhenAllPropertiesAreValid_ShouldNotHaveValidationError()
    {
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = 1,
            Model = new UpdateGenreDto { Name = "ValidName" }
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
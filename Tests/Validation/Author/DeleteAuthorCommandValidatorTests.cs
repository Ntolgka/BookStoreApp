using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Author;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Author;

public class DeleteAuthorCommandValidatorTests
{
    private readonly DeleteAuthorCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public DeleteAuthorCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _validator = new DeleteAuthorCommandValidator();
    }

    [Fact]
    public void Validate_WhenAuthorIdIsZero_ShouldHaveValidationError()
    {
        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 0 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenAuthorIdIsNegative_ShouldHaveValidationError()
    {
        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object) { AuthorId = -1 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenAuthorIdIsValid_ShouldNotHaveValidationError()
    {
        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 1 };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.AuthorId);
    }
}
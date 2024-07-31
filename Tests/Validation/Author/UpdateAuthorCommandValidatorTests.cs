using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Author;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Author;

public class UpdateAuthorCommandValidatorTests
{
    private readonly UpdateAuthorCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    public UpdateAuthorCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _validator = new UpdateAuthorCommandValidator();
    }

    [Fact]
    public void Validate_WhenAuthorIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            AuthorId = 0,
            Model = new UpdateAuthorDto()
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenNameIsTooShort_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            AuthorId = 1,
            Model = new UpdateAuthorDto { Name = "abc", LastName = "ValidLastName", BirthDate = DateTime.Now.AddYears(-1) }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Model.Name);
    }

    [Fact]
    public void Validate_WhenBirthDateIsInTheFuture_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            AuthorId = 1,
            Model = new UpdateAuthorDto { Name = "ValidName", LastName = "ValidLastName", BirthDate = DateTime.Now.AddDays(1) }
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Model.BirthDate.Date);
    }

    [Fact]
    public void Validate_WhenAllPropertiesAreValid_ShouldNotHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            AuthorId = 1,
            Model = new UpdateAuthorDto { Name = "ValidName", LastName = "ValidLastName", BirthDate = DateTime.Now.AddYears(-1) }
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
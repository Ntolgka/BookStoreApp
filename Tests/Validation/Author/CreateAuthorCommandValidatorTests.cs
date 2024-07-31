using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Author;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Author;

public class CreateAuthorCommandValidatorTests
{
    private readonly CreateAuthorCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    public CreateAuthorCommandValidatorTests()
    {
        _validator = new CreateAuthorCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
    {
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateAuthorDto
            {
                Name = "John",
                LastName = "Smith",
                BirthDate = DateTime.Now.AddYears(-30)
            }
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.Name);
        result.ShouldNotHaveValidationErrorFor(c => c.Model.LastName);
        result.ShouldNotHaveValidationErrorFor(c => c.Model.BirthDate);
    }
}
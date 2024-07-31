using AutoMapper;
using BookStoreApp.Application.BooksOperations.Commands;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Book;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Book;

public class CreateBookCommandValidatorTests
{
    private readonly CreateBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    public CreateBookCommandValidatorTests()
    {
        _validator = new CreateBookCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public void Validate_WhenAllPropertiesAreValid_ShouldNotHaveValidationError()
    {
        var command = new CreateBookCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateBookDto
            {
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddDays(-1),
                Title = "Valid Title",
                AuthorId = 1
            }
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenTitleIsTooShort_ShouldHaveValidationError()
    {
        var command = new CreateBookCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateBookDto
            {
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddDays(-1),
                Title = "abc",
                AuthorId = 1
            }
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.Title)
            .WithErrorMessage("Title must be at least 4 characters long.");
    }
}
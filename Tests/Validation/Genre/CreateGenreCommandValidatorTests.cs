using AutoMapper;
using BookStoreApp.Application.GenreOperations.Commands;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Genre;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Validation.Book;

public class CreateGenreCommandValidatorTests
{
    private readonly CreateGenreCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    public CreateGenreCommandValidatorTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _validator = new CreateGenreCommandValidator();
    }

    [Fact]
    public void WhenInvalidNameIsGiven_ShouldReturnError()
    {
        var command = new CreateGenreCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateGenreDto { Name = "" } 
        };
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(c => c.Model.Name);
    }

    [Fact]
    public void WhenValidNameIsGiven_ShouldNotReturnErrors()
    {
        var command = new CreateGenreCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateGenreDto { Name = "Fiction" } 
        };
        
        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(c => c.Model.Name);
    }
}
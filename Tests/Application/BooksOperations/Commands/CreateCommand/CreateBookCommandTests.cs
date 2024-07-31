using AutoMapper;
using BookStoreApp.Application.BooksOperations.Commands;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;
using FluentAssertions;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.BooksOperations.Commands.CreateCommand;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        var command = new CreateBookCommand(_mockUnitOfWork.Object, _mapper);
        command.Model = new CreateBookDto() { Title = "Lean Startup" };
        
        FluentActions.Invoking(() => command.Handle()).Should().ThrowAsync<InvalidOperationException>().WithMessage("Book does not exist.");
    }
}
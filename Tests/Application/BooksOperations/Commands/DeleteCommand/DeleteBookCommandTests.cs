using AutoMapper;
using BookStoreApp.Application.BooksOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using FluentAssertions;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.BooksOperations.Commands.CreateCommand;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenBookDoesNotExist_InvalidOperationException_ShouldBeReturned()
    {
        var command = new DeleteBookCommand(_mockUnitOfWork.Object);
        
        _mockUnitOfWork
            .Setup(repo => repo.BookRepository.GetById(It.IsAny<int>()))
            .ReturnsAsync((Book)null);

        await FluentActions.Invoking(() => command.Handle())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Book does not exist.");
    }

    [Fact]
    public async Task WhenValidBookIsGiven_Book_ShouldBeDeleted()
    {
        var existingBook = new Book { Id = 1, Title = "Title to Delete" };
        var command = new DeleteBookCommand(_mockUnitOfWork.Object) { BookId = 1 };

        _mockUnitOfWork
            .Setup(repo => repo.BookRepository.GetById(command.BookId))
            .ReturnsAsync(existingBook);

        _mockUnitOfWork
            .Setup(repo => repo.BookRepository.Delete(It.IsAny<Book>()));

        await command.Handle();

        _mockUnitOfWork.Verify(repo => repo.BookRepository.Delete(It.Is<Book>(b => b.Id == command.BookId)), Times.Once);
    }
}
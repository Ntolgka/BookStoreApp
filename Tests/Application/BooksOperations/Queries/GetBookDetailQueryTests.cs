using AutoMapper;
using BookStoreApp.Application.BooksOperations.Queries;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.BooksOperations.Queries;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenValidBookIdIsGiven_Book_ShouldBeReturned()
    {
        var bookId = 1;
        var book = new Book { Id = bookId, Title = "Book Title", GenreId = 1 };

        _mockUnitOfWork
            .Setup(repo => repo.BookRepository.GetById(bookId, "Genre"))
            .ReturnsAsync(book);

        var query = new GetBookDetailQuery(_mockUnitOfWork.Object, _mapper) { BookId = bookId };
        var result = await query.HandleAsync();

        Assert.NotNull(result);
        Assert.Equal(bookId, result.Id);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.GenreId, result.GenreId);
    }

    [Fact]
    public async Task WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        var bookId = 1;

        _mockUnitOfWork
            .Setup(repo => repo.BookRepository.GetById(bookId, "Genre"))
            .ReturnsAsync((Book)null);

        var query = new GetBookDetailQuery(_mockUnitOfWork.Object, _mapper) { BookId = bookId };

        await Assert.ThrowsAsync<InvalidOperationException>(() => query.HandleAsync());
    }
}
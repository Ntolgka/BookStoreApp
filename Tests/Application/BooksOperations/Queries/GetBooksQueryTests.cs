using AutoMapper;
using BookStoreApp.Application.BooksOperations.Queries;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.BooksOperations.Queries;

public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public GetBooksQueryTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenBooksExist_Books_ShouldBeReturned()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Genre = new Genre { Name = "Fiction" } },
            new Book { Id = 2, Title = "Book 2", Genre = new Genre { Name = "Non-Fiction" } }
        };

        _mockUnitOfWork
            .Setup(repo => repo.BookRepository.GetAll("Genre"))
            .ReturnsAsync(books);

        var query = new GetBooksQuery(_mockUnitOfWork.Object, _mapper);
        var result = await query.Handle();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Book 1", result[0].Title);
        Assert.Equal("Book 2", result[1].Title);
    }
}
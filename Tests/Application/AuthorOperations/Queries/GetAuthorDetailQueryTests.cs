using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Queries;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.AuthorOperations.Queries;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenValidAuthorIdIsGiven_Author_ShouldBeReturned()
    {
        var authorId = 1;
        var author = new Author { Id = authorId, Name = "John", LastName = "Doe" };

        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetById(authorId))
            .ReturnsAsync(author);

        var query = new GetAuthorDetailQuery(_mockUnitOfWork.Object, _mapper) { AuthorId = authorId };
        var result = await query.Handle();

        Assert.NotNull(result);
        Assert.Equal(authorId, result.Id);
        Assert.Equal(author.Name, result.Name);
        Assert.Equal(author.LastName, result.LastName);
    }

    [Fact]
    public async Task WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        var authorId = 1;

        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetById(authorId))
            .ReturnsAsync((Author)null);

        var query = new GetAuthorDetailQuery(_mockUnitOfWork.Object, _mapper) { AuthorId = authorId };

        await Assert.ThrowsAsync<InvalidOperationException>(() => query.Handle());
    }
}
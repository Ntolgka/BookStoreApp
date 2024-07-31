using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Queries;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.AuthorOperations.Queries;

public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorsQueryTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenAuthorsExist_Authors_ShouldBeReturned()
    {
        var authors = new List<Author>
        {
            new Author { Id = 1, Name = "John", LastName = "Doe" },
            new Author { Id = 2, Name = "Jane", LastName = "Smith" }
        };

        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetAll())
            .ReturnsAsync(authors);

        var query = new GetAuthorsQuery(_mockUnitOfWork.Object, _mapper);
        var result = await query.Handle();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal("Jane", result[1].Name);
    }
}
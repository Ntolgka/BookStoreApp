using AutoMapper;
using BookStoreApp.Application.GenreOperations.Queries;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.GenreOperations.Queries;

public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public GetGenresQueryTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenGenresExist_Genres_ShouldBeReturned()
    {
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Science Fiction" },
            new Genre { Id = 2, Name = "Mystery" }
        };

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetAll())
            .ReturnsAsync(genres);

        var query = new GetGenresQuery(_mockUnitOfWork.Object, _mapper);
        var result = await query.Handle();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Science Fiction", result[0].Name);
        Assert.Equal("Mystery", result[1].Name);
    }
}
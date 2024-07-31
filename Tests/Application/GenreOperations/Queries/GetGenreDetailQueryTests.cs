using AutoMapper;
using BookStoreApp.Application.GenreOperations.Queries;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.GenreOperations.Queries;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenValidGenreIdIsGiven_Genre_ShouldBeReturned()
    {
        var currentGenreId = 1;
        var genre = new Genre { Id = currentGenreId, Name = "Fantasy" };

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetById(currentGenreId))
            .ReturnsAsync(genre);

        var query = new GetGenreDetailQuery(_mockUnitOfWork.Object, _mapper) { genreId = currentGenreId };
        var result = await query.Handle();

        Assert.NotNull(result);
        Assert.Equal(currentGenreId, result.Id);
        Assert.Equal(genre.Name, result.Name);
    }

    [Fact]
    public async Task WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        var genreId = 1;

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetById(genreId))
            .ReturnsAsync((Genre)null);

        var query = new GetGenreDetailQuery(_mockUnitOfWork.Object, _mapper) { genreId = genreId };

        await Assert.ThrowsAsync<InvalidOperationException>(() => query.Handle());
    }
}
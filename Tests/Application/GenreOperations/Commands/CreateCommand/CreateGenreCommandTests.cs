using System.Linq.Expressions;
using AutoMapper;
using BookStoreApp.Application.GenreOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.GenreOperations.Commands.CreateCommand;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenAlreadyExistsGenreName_InvalidOperationException_ShouldBeReturned()
    {
        var existingGenre = new Genre { Name = "Fantasy" };
        var newGenreDto = new CreateGenreDto { Name = "Fantasy" };

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>()))
            .ReturnsAsync(existingGenre);

        var command = new CreateGenreCommand(_mockUnitOfWork.Object, _mapper)
        {
            Model = newGenreDto
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
    }

    [Fact]
    public async Task WhenValidGenreIsGiven_Genre_ShouldBeCreated()
    {
        var newGenreDto = new CreateGenreDto { Name = "Science Fiction" };
        Genre createdGenre = null;

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>()))!
            .ReturnsAsync((Genre)null);

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.Insert(It.IsAny<Genre>()))
            .Callback<Genre>(genre => createdGenre = genre);

        var command = new CreateGenreCommand(_mockUnitOfWork.Object, _mapper)
        {
            Model = newGenreDto
        };

        await command.Handle();

        Assert.NotNull(createdGenre);
        Assert.Equal(newGenreDto.Name, createdGenre.Name);

        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }
}
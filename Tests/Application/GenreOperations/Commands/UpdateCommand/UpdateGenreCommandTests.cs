using AutoMapper;
using BookStoreApp.Application.GenreOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.GenreOperations.Commands.UpdateCommand;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenValidGenreIsGiven_Genre_ShouldBeUpdated()
    {
        var genreId = 1;
        var existingGenre = new Genre { Id = genreId, Name = "Drama" };
        var updatedGenreDto = new UpdateGenreDto { Name = "Updated Drama" };

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetById(genreId))
            .ReturnsAsync(existingGenre);

        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = genreId,
            Model = updatedGenreDto
        };

        await command.Handle();

        _mockUnitOfWork.Verify(repo => repo.GenreRepository.Update(It.Is<Genre>(g =>
            g.Id == genreId &&
            g.Name == updatedGenreDto.Name)), Times.Once);

        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }

    [Fact]
    public async Task WhenGenreDoesNotExist_InvalidOperationException_ShouldBeReturned()
    {
        var genreId = 1;
        var updatedGenreDto = new UpdateGenreDto { Name = "Updated Drama" };

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetById(genreId))
            .ReturnsAsync((Genre)null);

        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = genreId,
            Model = updatedGenreDto
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
    }
}
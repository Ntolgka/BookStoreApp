using BookStoreApp.Application.GenreOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.GenreOperations.Commands.DeleteCommand;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
    }

    [Fact]
    public async Task WhenGenreDoesNotExist_InvalidOperationException_ShouldBeReturned()
    {
        var genreId = 1;

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetById(genreId))
            .ReturnsAsync((Genre)null);

        var command = new DeleteGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = genreId
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
    }

    [Fact]
    public async Task WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
    {
        var genre = new Genre { Id = 1, Name = "Horror" };

        _mockUnitOfWork
            .Setup(repo => repo.GenreRepository.GetById(genre.Id))
            .ReturnsAsync(genre);

        var command = new DeleteGenreCommand(_mockUnitOfWork.Object)
        {
            genreId = genre.Id
        };

        await command.Handle();

        _mockUnitOfWork.Verify(repo => repo.GenreRepository.Delete(It.Is<Genre>(g => g.Id == genre.Id)), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }
}
using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.AuthorOperations.Commands.DeleteCommand;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenAuthorExists_Author_ShouldBeDeleted()
    {
        var authorId = 1;
        var existingAuthor = new Author { Id = authorId, Name = "John", LastName = "Doe" };

        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetById(authorId))
            .ReturnsAsync(existingAuthor);

        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object)
        {
            AuthorId = authorId
        };
        
        await command.Handle();
        
        _mockUnitOfWork.Verify(repo => repo.AuthorRepository.Delete(existingAuthor), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }

    [Fact]
    public async Task WhenAuthorDoesNotExist_InvalidOperationException_ShouldBeReturned()
    {
        var authorId = 1;
        
        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetById(authorId))
            .ReturnsAsync((Author)null);

        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object)
        {
            AuthorId = authorId
        };
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
    }
}
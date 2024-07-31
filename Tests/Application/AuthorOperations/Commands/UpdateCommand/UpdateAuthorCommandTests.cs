using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.AuthorOperations.Commands.UpdateCommand;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _mockUnitOfWork = testFixture.MockUnitOfWork;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public async Task WhenValidAuthorIsGiven_Author_ShouldBeUpdated()
    {
        var authorId = 1;
        var existingAuthor = new Author 
        { 
            Id = authorId, 
            Name = "John", 
            LastName = "Doe", 
            BirthDate = new DateTime(1970, 1, 1) 
        };
        
        var updatedAuthorDto = new UpdateAuthorDto 
        { 
            Id = authorId, 
            Name = "John Updated", 
            LastName = "Doe Updated", 
            BirthDate = new DateTime(1980, 1, 1) 
        };

        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetById(authorId))
            .ReturnsAsync(existingAuthor);
        
        var configuration = new MapperConfiguration(cfg => {
            cfg.CreateMap<UpdateAuthorDto, Author>();
        });
        var mapper = configuration.CreateMapper();

        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object, mapper)
        {
            AuthorId = authorId,
            Model = updatedAuthorDto
        };

        await command.Handle();

        _mockUnitOfWork.Verify(repo => repo.AuthorRepository.Update(It.Is<Author>(a =>
            a.Id == authorId &&
            a.Name == updatedAuthorDto.Name &&
            a.LastName == updatedAuthorDto.LastName &&
            a.BirthDate == updatedAuthorDto.BirthDate)), Times.Once);

        _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }

    [Fact]
    public async Task WhenAuthorDoesNotExist_InvalidOperationException_ShouldBeReturned()
    {
        var authorId = 1;
        var updatedAuthorDto = new UpdateAuthorDto 
        { 
            Id = authorId, 
            Name = "John Updated", 
            LastName = "Doe Updated", 
            BirthDate = new DateTime(1980, 1, 1) 
        };

        _mockUnitOfWork
            .Setup(repo => repo.AuthorRepository.GetById(authorId))
            .ReturnsAsync((Author)null);

        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object, _mapper)
        {
            AuthorId = authorId,
            Model = updatedAuthorDto
        };
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
    }
}
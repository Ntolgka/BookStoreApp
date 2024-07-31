using System.Linq.Expressions;
using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.AuthorOperations.Commands.CreateCommand;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _mockUnitOfWork = testFixture.MockUnitOfWork;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public async Task WhenValidAuthorIsGiven_Author_ShouldBeCreated()
        {
            var newAuthorDto = new CreateAuthorDto { Name = "John", LastName = "Doe", BirthDate = DateTime.Now };
            
            _mockUnitOfWork
                .Setup(repo => repo.AuthorRepository.FirstOrDefault(It.IsAny<Expression<Func<Author, bool>>>()))!
                .ReturnsAsync((Author)null);

            var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mapper) { Model = newAuthorDto };
            
            await command.Handle();
            
            _mockUnitOfWork.Verify(repo => repo.AuthorRepository.Insert(It.Is<Author>(a =>
                a.Name == newAuthorDto.Name &&
                a.LastName == newAuthorDto.LastName &&
                a.BirthDate == newAuthorDto.BirthDate)), Times.Once);

            _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
        }

        [Fact]
        public async Task WhenAlreadyExistsAuthorName_InvalidOperationException_ShouldBeReturned()
        {
            var existingAuthor = new Author { Name = "John", LastName = "Doe" };
            var newAuthorDto = new CreateAuthorDto { Name = "John", LastName = "Doe", BirthDate = DateTime.Now };

            _mockUnitOfWork
                .Setup(repo => repo.AuthorRepository.FirstOrDefault(It.Is<Expression<Func<Author, bool>>>(expr =>
                    expr.Compile().Invoke(existingAuthor)))) 
                .ReturnsAsync(existingAuthor);

            var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mapper) { Model = newAuthorDto };

            await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
        }
    }
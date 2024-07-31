using AutoMapper;
using BookStoreApp.Application.BooksOperations.Commands;
using BookStoreApp.Model;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;
using Moq;
using Tests.TestSetup;

namespace Tests.Application.BooksOperations.Commands.CreateCommand;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _mockUnitOfWork = testFixture.MockUnitOfWork;
        }

        [Fact]
        public async Task WhenValidBookIsGiven_Book_ShouldBeUpdated()
        {
            var bookId = 1;
            var existingBook = new Book { Id = bookId, Title = "Original Title", GenreId = 1 };
            var updatedBookDto = new UpdateBookDto { GenreId = 2, Title = "Updated Title" };
            
            _mockUnitOfWork
                .Setup(repo => repo.BookRepository.GetById(bookId))
                .ReturnsAsync(existingBook);

            var command = new UpdateBookCommand(_mockUnitOfWork.Object)
            {
                BookId = bookId,
                Model = updatedBookDto
            };
            
            await command.Handle();
            
            _mockUnitOfWork.Verify(repo => repo.BookRepository.Update(It.Is<Book>(b =>
                b.Id == bookId &&
                b.Title == "Updated Title" &&
                b.GenreId == 2)), Times.Once);

            _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
        }

        [Fact]
        public async Task WhenBookDoesNotExist_InvalidOperationException_ShouldBeReturned()
        {
            // Arrange
            var bookId = 1;
            var updatedBookDto = new UpdateBookDto { Title = "Updated Title" };

            // Setup the mock to return null (book does not exist)
            _mockUnitOfWork
                .Setup(repo => repo.BookRepository.GetById(bookId))
                .ReturnsAsync((Book)null);

            var command = new UpdateBookCommand(_mockUnitOfWork.Object)
            {
                BookId = bookId,
                Model = updatedBookDto
            };
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => command.Handle());
        }
    }
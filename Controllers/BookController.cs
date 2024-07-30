using AutoMapper;
using BookStoreApp.Application.BooksOperations;
using BookStoreApp.Schema.Book;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Book;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetBooksQuery(_unitOfWork, _mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetBookDetailQuery(_unitOfWork, _mapper) { BookId = id };

            var validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = await query.HandleAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto newBook)
        {
            var command = new CreateBookCommand(_unitOfWork, _mapper) { Model = newBook };

            var validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok($"{newBook.Title} has saved successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updatedBook)
        {
            var command = new UpdateBookCommand(_unitOfWork) { BookId = id, Model = updatedBook };

            var validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok($"{updatedBook.Title} updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var command = new DeleteBookCommand(_unitOfWork) { BookId = id };

            var validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok("Book deleted successfully.");
        }
    }
}

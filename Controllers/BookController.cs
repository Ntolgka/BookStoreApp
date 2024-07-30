using AutoMapper;
using BookStoreApp.BooksOperations;
using BookStoreApp.DBOperations;
using BookStoreApp.GenericRepository;
using BookStoreApp.Model;
using BookStoreApp.Validation.Book;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static BookStoreApp.BooksOperations.CreateBookCommand;
using static BookStoreApp.BooksOperations.GetBookDetailQuery;
using static BookStoreApp.BooksOperations.UpdateBookCommand;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookModel newBook)
        {
            var genericRepository = new GenericRepository<Book>(_context);
            CreateBookCommand command = new CreateBookCommand(genericRepository, _mapper);

            command.Model = newBook;

            CreateBookCommandValidator validator = new();
            ValidationResult result = validator.Validate(command);

            validator.ValidateAndThrow(command);
            await command.HandleAsync();

            return Ok($"{newBook.Title} has saved successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok($"{updatedBook.Title} updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok("Book deleted successfully.");
        }
    }
}

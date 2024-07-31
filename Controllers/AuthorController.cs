using AutoMapper;
using BookStoreApp.Application.AuthorOperations.Commands;
using BookStoreApp.Application.AuthorOperations.Queries;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;
using BookStoreApp.Validation.Author;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAuthorsQuery(_unitOfWork, _mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetAuthorDetailQuery(_unitOfWork, _mapper);

            var validator = new GetAuthorDetailQueryValidator();
            await validator.ValidateAndThrowAsync(query);

            var result = await query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto newAuthor)
        {
            var command = new CreateAuthorCommand(_unitOfWork, _mapper) { Model = newAuthor };

            var validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok($"{newAuthor.Name} has saved successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updatedAuthor)
        {
            var command = new UpdateAuthorCommand(_unitOfWork, _mapper) { AuthorId = id, Model = updatedAuthor };

            var validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok($"{updatedAuthor.Name} updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var command = new DeleteAuthorCommand(_unitOfWork) { AuthorId = id };

            var validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok("Author deleted successfully.");
        }
    }
}


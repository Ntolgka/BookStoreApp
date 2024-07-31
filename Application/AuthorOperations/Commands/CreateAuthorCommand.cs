using AutoMapper;
using BookStoreApp.Model;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.AuthorOperations.Commands;

public class CreateAuthorCommand
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateAuthorDto Model { get; set; }

    public CreateAuthorCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingAuthor = await _unitOfWork.AuthorRepository.FirstOrDefault(x => x.Name == Model.Name && x.LastName == Model.LastName);

        if (existingAuthor is not null)
        {
            throw new InvalidOperationException("This book already exists...");
        }

        var author = _mapper.Map<Author>(Model);

        await _unitOfWork.AuthorRepository.Insert(author);
        await _unitOfWork.Complete();
    }
}

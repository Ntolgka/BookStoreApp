using AutoMapper;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.AuthorOperations;

public class UpdateAuthorCommand
{
    public int AuthorId { get; set; }
    public UpdateAuthorDto Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuthorCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var author = await _unitOfWork.AuthorRepository.GetById(AuthorId);

        if (author == null)
        {
            throw new InvalidOperationException("Author not found.");
        }

        _mapper.Map(Model, author);

        _unitOfWork.AuthorRepository.Update(author);
        await _unitOfWork.Complete();
    }
}
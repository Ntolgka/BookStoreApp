using AutoMapper;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.AuthorOperations;

public class GetAuthorDetailQuery
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public int AuthorId { get; set; }

    public GetAuthorDetailQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetAuthorDetailDto> Handle()
    {
        var author = await _unitOfWork.AuthorRepository.GetById(AuthorId);

        if (author is null)
        {
            throw new InvalidOperationException("Author not found.");
        }

        return _mapper.Map<GetAuthorDetailDto>(author);
    }
}

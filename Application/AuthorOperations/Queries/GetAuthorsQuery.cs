using AutoMapper;
using BookStoreApp.Schema.Author;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.AuthorOperations.Queries;

public class GetAuthorsQuery
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetAuthorsDto>> Handle()
    {
        var authors = await _unitOfWork.AuthorRepository.GetAll();
        var mapped = _mapper.Map<List<GetAuthorsDto>>(authors);
        return mapped;
    }
}
using AutoMapper;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.GenreOperations;

public class GetGenresQuery
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGenresQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetGenresDto>> Handle()
    {
        var genres = await _unitOfWork.GenreRepository.GetAll(); 
        return _mapper.Map<List<GetGenresDto>>(genres);
    }
}
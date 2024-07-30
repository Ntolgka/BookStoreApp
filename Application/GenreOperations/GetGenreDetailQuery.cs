using AutoMapper;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.GenreOperations;

public class GetGenreDetailQuery
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public int genreId { get; set; }

    public GetGenreDetailQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetGenreDetailDto> Handle()
    {
        var book = await _unitOfWork.GenreRepository.GetById(genreId);

        if (book is null)
        {
            throw new InvalidOperationException("Genre not found.");
        }

        return _mapper.Map<GetGenreDetailDto>(book);
    }
}

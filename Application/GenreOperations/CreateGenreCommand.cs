using AutoMapper;
using BookStoreApp.Model;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.GenreOperations;

public class CreateGenreCommand
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateGenreDto Model { get; set; }

    public CreateGenreCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingGenre = await _unitOfWork.GenreRepository.FirstOrDefault(x => x.Name == Model.Name);

        if (existingGenre is not null)
        {
            throw new InvalidOperationException("This genre already exists...");
        }

        var genre = _mapper.Map<Genre>(Model);

        await _unitOfWork.GenreRepository.Insert(genre);
        await _unitOfWork.Complete();
    }
}

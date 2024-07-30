using AutoMapper;
using BookStoreApp.Schema.Genre;
using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.GenreOperations;

public class UpdateGenreCommand
{
    public int genreId { get; set; }
    public UpdateGenreDto Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;

    public UpdateGenreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }   

    public async Task Handle()
    {   
        var genre = await _unitOfWork.GenreRepository.GetById(genreId);

        if (genre is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        genre.Name = Model.Name != default ? Model.Name : genre.Name;

        _unitOfWork.GenreRepository.Update(genre);
        await _unitOfWork.Complete();
    }
}
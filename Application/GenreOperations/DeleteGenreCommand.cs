using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.GenreOperations;

public class DeleteGenreCommand
{
    public int genreId { get; set; }

    private readonly IUnitOfWork _unitOfWork;

    public DeleteGenreCommand(IUnitOfWork unitOfWork)
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

        _unitOfWork.GenreRepository.Delete(genre);
        await _unitOfWork.Complete();
    }
}

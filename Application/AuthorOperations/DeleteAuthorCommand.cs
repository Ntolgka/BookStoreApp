using BookStoreApp.UnitOfWork;

namespace BookStoreApp.Application.AuthorOperations;

public class DeleteAuthorCommand
{
    public int AuthorId { get; set; }

    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {   
        var author = await _unitOfWork.AuthorRepository.GetById(AuthorId);

        if (author is null)
        {
            throw new InvalidOperationException("There is no author with this book id.");
        }

        _unitOfWork.AuthorRepository.Delete(author);
        await _unitOfWork.Complete();
    }
}

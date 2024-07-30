using BookStoreApp.DBOperations;

namespace BookStoreApp.BooksOperations;

public class DeleteBookCommand
{
    public int BookId { get; set; }

    private readonly AppDbContext _dbContext;
    public DeleteBookCommand(AppDbContext dBContext)
    {
        _dbContext = dBContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == BookId);

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}

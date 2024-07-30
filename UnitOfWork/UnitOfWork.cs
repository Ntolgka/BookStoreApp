using BookStoreApp.DBOperations;
using BookStoreApp.GenericRepository;
using BookStoreApp.Model;

namespace BookStoreApp.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;

    public IGenericRepository<Book> BookRepository { get; }
    public IGenericRepository<Genre> GenreRepository { get; }
    public IGenericRepository<Author> AuthorRepository { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        BookRepository = new GenericRepository<Book>(_dbContext);
        GenreRepository = new GenericRepository<Genre>(_dbContext);
        AuthorRepository = new GenericRepository<Author>(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task Complete()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task CompleteWithTransaction()
    {
        using (var dbTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
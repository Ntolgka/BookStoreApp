using BookStoreApp.GenericRepository;
using BookStoreApp.Model;

namespace BookStoreApp.UnitOfWork;

public interface IUnitOfWork
{
    Task Complete(); 
    Task CompleteWithTransaction();
    IGenericRepository<Book> BookRepository { get; }
    IGenericRepository<Genre> GenreRepository { get; }
    IGenericRepository<Author> AuthorRepository { get; }    

}
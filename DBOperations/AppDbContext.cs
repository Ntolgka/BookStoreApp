using BookStoreApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.DBOperations;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }

}

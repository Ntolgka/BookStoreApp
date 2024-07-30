using BookStoreApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.DBOperations;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.GenreId);
        
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.AuthorId);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }

}

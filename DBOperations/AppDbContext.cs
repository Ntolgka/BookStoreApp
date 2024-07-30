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
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }

}

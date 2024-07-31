using BookStoreApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.DBOperations;

public class DataGenerator
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            context.Database.EnsureCreated();

            if (context.Genres.Any())
            {
                return; // Database has been seeded
            }

            context.Genres.AddRange(
                new Genre { Name = "Fiction", InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)},
                new Genre { Name = "Non-Fiction", InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)}
            );

            context.SaveChanges();
            
            context.Authors.AddRange(
                new Author { Name = "William", LastName = "Shakespeare", BirthDate = DateTime.SpecifyKind(new DateTime(1564, 04, 23), DateTimeKind.Utc), InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)},
                new Author { Name = "George", LastName = "Orwell", BirthDate = DateTime.SpecifyKind(new DateTime(1903, 06, 25), DateTimeKind.Utc), InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc) },
                new Author { Name = "Fyodor", LastName = "Dostoevsky", BirthDate = DateTime.SpecifyKind(new DateTime(1821, 11, 11), DateTimeKind.Utc), InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc) }
                );
            
            context.SaveChanges();

            context.Books.AddRange(
                new Book { Title = "Lean Startup", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc), InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)},
                new Book { Title = "Herland", GenreId = 2, AuthorId = 1, PageCount = 250, PublishDate = DateTime.SpecifyKind(new DateTime(2010, 06, 12), DateTimeKind.Utc), InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc) },
                new Book { Title = "Dune2", GenreId = 2, AuthorId = 1, PageCount = 540, PublishDate = DateTime.SpecifyKind(new DateTime(2001, 12, 21), DateTimeKind.Utc), InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc) }
            );

            context.SaveChanges();
        }
    }


}
using BookStoreApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.DBOperations;

public class DataGenerator
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {

            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book { Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc) },
                new Book { Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = DateTime.SpecifyKind(new DateTime(2010, 06, 12), DateTimeKind.Utc) },
                new Book { Title = "Dune2", GenreId = 2, PageCount = 540, PublishDate = DateTime.SpecifyKind(new DateTime(2001, 12, 21), DateTimeKind.Utc) }
            );

            context.SaveChanges();

        }
    }


}
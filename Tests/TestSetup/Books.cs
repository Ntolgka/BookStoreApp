using BookStoreApp.Model;

namespace Tests.TestSetup;

public static class Books
{
    public static List<Book> GetBooks()
    {
        return new List<Book>
        {
            new Book
            {
                Title = "Lean Startup",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 200,
                PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc),
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            },
            new Book
            {
                Title = "Herland",
                GenreId = 1,
                AuthorId = 2,
                PageCount = 250,
                PublishDate = DateTime.SpecifyKind(new DateTime(2010, 06, 12), DateTimeKind.Utc),
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            },
            new Book
            {
                Title = "Dune2",
                GenreId = 2,
                AuthorId = 3,
                PageCount = 540,
                PublishDate = DateTime.SpecifyKind(new DateTime(2001, 12, 21), DateTimeKind.Utc),
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            }
        };
    }
}
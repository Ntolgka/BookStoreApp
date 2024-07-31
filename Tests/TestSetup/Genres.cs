using BookStoreApp.Model;

namespace Tests.TestSetup;

public static class Genres
{
    public static List<Genre> GetGenres()
    {
        return new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "Fiction",
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            },
            new Genre
            {
                Id = 2,
                Name = "Non-Fiction",
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            }
        };
    }
}
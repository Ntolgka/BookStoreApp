using BookStoreApp.DBOperations;
using BookStoreApp.Model;

namespace Tests.TestSetup;

public static class Authors
{
    public static List<Author> GetAuthors()
    {
        return new List<Author>
        {
            new Author
            {
                Name = "William", 
                LastName = "Shakespeare", 
                BirthDate = DateTime.SpecifyKind(new DateTime(1564, 04, 23), DateTimeKind.Utc), 
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            },
            new Author
            {
                Name = "George", 
                LastName = "Orwell", 
                BirthDate = DateTime.SpecifyKind(new DateTime(1903, 06, 25), DateTimeKind.Utc), 
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            },
            new Author
            {
                Name = "Fyodor", 
                LastName = "Dostoevsky", 
                BirthDate = DateTime.SpecifyKind(new DateTime(1821, 11, 11), DateTimeKind.Utc), 
                InsertDate = DateTime.SpecifyKind(new DateTime(2024, 06, 12), DateTimeKind.Utc)
            }
        };
    }
}
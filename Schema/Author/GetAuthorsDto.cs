namespace BookStoreApp.Schema.Author;

public class GetAuthorsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
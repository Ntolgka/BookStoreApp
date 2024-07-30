namespace BookStoreApp.Schema.Author;

public class GetAuthorDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
namespace BookStoreApp.Schema.Author;

public class CreateAuthorDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
namespace BookStoreApp.Schema.Genre;

public class CreateGenreDto
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
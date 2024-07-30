namespace BookStoreApp.Schema.Genre;

public class GetGenreDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
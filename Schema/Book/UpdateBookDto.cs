namespace BookStoreApp.Schema.Book;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }
}
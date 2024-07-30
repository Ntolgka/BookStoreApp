namespace BookStoreApp.Schema.Book;

public class GetBooksDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
}
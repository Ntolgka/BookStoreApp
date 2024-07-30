using BookStoreApp.Base;

namespace BookStoreApp.Schema.Book;

public class CreateBookDto : BaseEntity
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public int GenreId { get; set; }
}
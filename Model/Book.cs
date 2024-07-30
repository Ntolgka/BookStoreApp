using System.ComponentModel.DataAnnotations.Schema;
using BookStoreApp.Base;

namespace BookStoreApp.Model;

public class Book : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public Genre Genre { get; set; }
    public int GenreId { get; set; }
    public Author Author { get; set; }
    public int AuthorId { get; set; }
    
}

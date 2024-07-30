using System.ComponentModel.DataAnnotations.Schema;
using BookStoreApp.Base;

namespace BookStoreApp.Model;

public class Genre : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
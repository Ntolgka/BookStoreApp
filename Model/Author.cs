using System.ComponentModel.DataAnnotations.Schema;
using BookStoreApp.Base;

namespace BookStoreApp.Model;

public class Author : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}   
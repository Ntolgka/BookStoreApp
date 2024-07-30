using BookStoreApp.Base;

namespace BookStoreApp.Schema.Author;

public class CreateAuthorDto : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
using BookStoreApp.Base;

namespace BookStoreApp.Schema.Genre;

public class CreateGenreDto : BaseEntity
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
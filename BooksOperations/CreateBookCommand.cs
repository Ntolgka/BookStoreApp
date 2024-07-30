using AutoMapper;
using BookStoreApp.GenericRepository;
using BookStoreApp.Model;

namespace BookStoreApp.BooksOperations;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly IGenericRepository<Book> _genericRepository;
    private readonly IMapper _mapper;

    public CreateBookCommand(IGenericRepository<Book> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task HandleAsync()
    {
        var existingBook = await _genericRepository.FirstOrDefault(x => x.Title == Model.Title);

        if (existingBook is not null)
        {
            throw new InvalidOperationException("This book already exists...");
        }
            
        var book = _mapper.Map<Book>(Model);

        await _genericRepository.Insert(book);
        await _genericRepository.Save();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}

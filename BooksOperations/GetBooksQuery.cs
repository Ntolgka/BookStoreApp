using AutoMapper;
using BookStoreApp.DBOperations;

namespace BookStoreApp.BooksOperations;

public class GetBooksQuery

{
    private readonly AppDbContext _dbContext;
    private readonly IMapper mapper;

    public GetBooksQuery(AppDbContext dBContext, IMapper mapper)
    {
        _dbContext = dBContext;
        this.mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();

        List<BooksViewModel> books = mapper.Map<List<BooksViewModel>>(bookList);

        return books;
    }

    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }

}
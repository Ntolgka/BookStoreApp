using AutoMapper;
using BookStoreApp.DBOperations;

namespace BookStoreApp.BooksOperations;

public class GetBookDetailQuery
{
    public int BookId { get; set; }

    private readonly AppDbContext _dbContext;
    private readonly IMapper mapper;

    public GetBookDetailQuery(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        this.mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        BookDetailViewModel viewModel = mapper.Map<BookDetailViewModel>(book);

        return viewModel;

    }

    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }

}

using System.Linq.Expressions;
using AutoMapper;
using BookStoreApp.Common;
using BookStoreApp.DBOperations;
using BookStoreApp.GenericRepository;
using BookStoreApp.Model;
using BookStoreApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.TestSetup;

public class CommonTestFixture
{
    public IMapper Mapper { get; set; }
    public Mock<IUnitOfWork> MockUnitOfWork { get; set; }

    public CommonTestFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("PostgresSqlTestConnection");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;
            
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = mapperConfig.CreateMapper();

        MockUnitOfWork = new Mock<IUnitOfWork>();
        SetupMockRepositories();
    }

    private void SetupMockRepositories()
    {
        var mockBookRepository = new Mock<IGenericRepository<Book>>();
        mockBookRepository.Setup(repo => repo.FirstOrDefault(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<string[]>()))
            .ReturnsAsync((Expression<Func<Book, bool>> predicate, string[] includes) =>
            {
                return Books.GetBooks().AsQueryable().FirstOrDefault(predicate);
            });

        mockBookRepository.Setup(repo => repo.Insert(It.IsAny<Book>()))
            .ReturnsAsync((Book book) =>
            {
                Books.GetBooks().Add(book);
                return book;
            });

        mockBookRepository.Setup(repo => repo.GetAll(It.IsAny<string[]>()))
            .ReturnsAsync((string[] includes) => Books.GetBooks());

        var mockAuthorRepository = new Mock<IGenericRepository<Author>>();
        var mockGenreRepository = new Mock<IGenericRepository<Genre>>();

        MockUnitOfWork.Setup(u => u.BookRepository).Returns(mockBookRepository.Object);
        MockUnitOfWork.Setup(u => u.AuthorRepository).Returns(mockAuthorRepository.Object);
        MockUnitOfWork.Setup(u => u.GenreRepository).Returns(mockGenreRepository.Object);
        MockUnitOfWork.Setup(u => u.Complete()).Verifiable();
    }
}

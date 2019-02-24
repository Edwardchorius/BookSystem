using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.UnitTests.ServiceTests.UserServiceTests
{
    [TestClass]
    public class PagedUserBooks_Should
    {
        private DbContextOptions<BookSystemDbContext> contextOptions;
        private Mock<IUserService> userServiceMock;
        private User user;
        private Book book;
        private UsersBooks usersBooks;
        private string userId = "randomId";
        private string firstName = "randomFirstName";
        private string lastName = "randomLastName";
        private string title = "randomTitle";
        private string genre = "randomGenre";
        private string username = "randomUserName";
        private DateTime addedOn = DateTime.Now;
        private string sortOrder = "date_desc";
        private string currentFilter = "random";
        private string searchString = "Genre";

        [TestMethod]
        public async Task ReturnQueryableOfBooks_When_PassedCorrectParams()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
            .UseInMemoryDatabase(databaseName: "ReturnQueryableOfBooks_When_PassedCorrectParams")
                .Options;
            

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                UsersBooks = new List<UsersBooks>(),
                CreatedOn = addedOn
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

            var bookList = new List<Book>();
            bookList.Add(book);

            userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(usm => usm.GetUserBooks(user)).Returns(bookList.AsQueryable());

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.UsersBooks.AddAsync(usersBooks);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new UserService(assertContext);
                var pageList = sut.PagedUserBooks(user, sortOrder, currentFilter, searchString, 1);

                Assert.IsInstanceOfType(pageList, typeof(IQueryable<Book>));
            }
        }

        [TestMethod]
        public async Task ReturnQueryableOfBooks_WhenPassed_ConcreteGenreParams_InSearchString()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
            .UseInMemoryDatabase(databaseName: "ReturnQueryableOfBooks_WhenPassed_ConcreteGenreParams_InSearchString")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                UsersBooks = new List<UsersBooks>(),
                CreatedOn = addedOn
            };

            Book book2 = new Book
            {
                Title = "2" + title,
                Genre = "2" + genre,
                UsersBooks = new List<UsersBooks>(),
                CreatedOn = addedOn
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

            var usersBooks2 = new UsersBooks
            {
                User = user,
                Book = book2
            };

            var bookList = new List<Book>();
            bookList.Add(book);
            bookList.Add(book2);

            userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(usm => usm.GetUserBooks(user)).Returns(bookList.AsQueryable());

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.UsersBooks.AddAsync(usersBooks);
                await actContext.UsersBooks.AddAsync(usersBooks2);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new UserService(assertContext);
                var pagedList = sut.PagedUserBooks(user, sortOrder, currentFilter, "2"+ genre, 1);

                Assert.IsInstanceOfType(pagedList, typeof(IQueryable<Book>));
                Assert.IsTrue(pagedList.Count() == 1);
            }
        }

        [TestMethod]
        public async Task ReturnQueryableOfBooks_WhenPassed_ConcreteSortParams_DateDesc()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
            .UseInMemoryDatabase(databaseName: "ReturnQueryableOfBooks_WhenPassed_ConcreteSortParams_DateDesc")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                UsersBooks = new List<UsersBooks>(),
                CreatedOn = addedOn
            };

            Book book2 = new Book
            {
                Title = "2" + title,
                Genre = "2" + genre,
                UsersBooks = new List<UsersBooks>(),
                CreatedOn = addedOn.AddHours(5)
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

            var usersBooks2 = new UsersBooks
            {
                User = user,
                Book = book2
            };

            var bookList = new List<Book>();
            bookList.Add(book);
            bookList.Add(book2);

            userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(usm => usm.GetUserBooks(user)).Returns(bookList.AsQueryable());

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.UsersBooks.AddAsync(usersBooks);
                await actContext.UsersBooks.AddAsync(usersBooks2);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new UserService(assertContext);
                var pagedList = sut.PagedUserBooks(user, sortOrder, currentFilter, searchString, 1);

                Assert.IsInstanceOfType(pagedList, typeof(IQueryable<Book>));
                Assert.IsTrue(pagedList.Select(b => b.CreatedOn).FirstOrDefault() == book2.CreatedOn);
                Assert.IsTrue(pagedList.Count() == 2);
            }
        }
    }
}

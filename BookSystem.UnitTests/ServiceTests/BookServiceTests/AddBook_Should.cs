using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.Exceptions;
using BookSystem.ServiceLayer.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.UnitTests.ServiceTests.BookServiceTests
{
    [TestClass]
    public class AddBook_Should
    {
        private DbContextOptions<BookSystemDbContext> contextOptions;
        private UsersBooks usersBooks;
        private User user;
        private Book book;
        private string userId = "randomId";
        private string firstName = "randomFirstName";
        private string lastName = "randomLastName";
        private string title = "randomTitle";
        private string genre = "randomGenre";
        private string username = "randomUserName";

        [TestMethod]
        public async Task ReturnAddedBook_WhenPassedValidParams()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnAddedBook_WhenPassedValidParams")
                .Options;

           user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>()
            };


            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new BookService(assertContext);
                Book bookResult = await sut.AddBook(user, title, genre);
                Assert.IsInstanceOfType(bookResult, typeof(Book));
                Assert.IsTrue(bookResult.Title == title);
                Assert.IsTrue(bookResult.Genre == genre);
                Assert.IsTrue(bookResult.UsersBooks.Select(ub => ub.User).FirstOrDefault(u => u.UserName == username) == user);
            }
        }

        [TestMethod]
        public async Task ThrowException_When_BookAlreadyAdded()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException_When_BookAlreadyAdded")
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
                UsersBooks = new List<UsersBooks>()
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

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
                var bookService = new BookService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(async () => await bookService.AddBook(user, title, genre));
            }
        }

        [TestMethod]
        public async Task ThrowException_When_FailToRetrieveUser()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException_When_FailToRetrieveUser")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>(),
                UsersBooksLikes = new List<UsersBooksLikes>()
            };

            var userForException = new User
            {
                Id = userId + "2",
                UserName = username + "2",
                FirstName = firstName + "2",
                LastName = lastName + "2",
                UsersBooks = new List<UsersBooks>(),
                UsersBooksLikes = new List<UsersBooksLikes>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                UsersBooks = new List<UsersBooks>()
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

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
                var bookService = new BookService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await bookService.AddBook(userForException, title, genre));
            }
        }

        [TestMethod]
        public async Task CheckIf_ReturnedBookIs_AddedInTheBase()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "CheckIf_ReturnedBookIs_AddedInTheBase")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>()
            };


            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var bookService = new BookService(assertContext);
                Book bookResult = await bookService.AddBook(user, title, genre);
                var bookInBase = await assertContext.UsersBooks.Where(ub => ub.Book == bookResult).Select(b => b.Book).FirstOrDefaultAsync();

                Assert.AreSame(bookResult, bookInBase);
            }
        }
    }
}

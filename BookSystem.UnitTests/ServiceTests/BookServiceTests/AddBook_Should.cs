using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
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
        private Mock<IUserService> userServiceMock;
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

            book = new Book
            {
                Title = "randomTitle2",
                Genre = "randomGenre2",
                UsersBooks = new List<UsersBooks>()
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

            var usersBooksList = new List<UsersBooks>();
            usersBooksList.Add(usersBooks);

            userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(usm => usm.GetUserBooks(username)).ReturnsAsync(usersBooksList);

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
                var sut = new BookService(assertContext, userServiceMock.Object);
                Book bookResult = await sut.AddBook(user, title, genre);
                Assert.IsInstanceOfType(bookResult, typeof(Book));
                Assert.IsTrue(bookResult.Title == title);
                Assert.IsTrue(bookResult.Genre == genre);
                Assert.IsTrue(bookResult.UsersBooks.Select(ub => ub.User).FirstOrDefault(u => u.UserName == username) == user);
            }
        }


    }
}

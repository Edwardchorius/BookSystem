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
        private string sortOrder = "genre_desc";
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

            var userBooksList = new List<UsersBooks>();
            var bookList = new List<Book>();
            bookList.Add(book);
            userBooksList.Add(usersBooks);

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
    }
}

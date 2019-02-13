using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.UnitTests.ServiceTests.UserServiceTests
{
    [TestClass]
    public class GetUserBooks_Should
    {
        private DbContextOptions<BookSystemDbContext> contextOptions;
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

        [TestMethod]
        public async Task ReturnQueryOfUserBooks_When_PassedValidUser()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnQueryOfUserBooks_When_PassedValidUser")
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
                var userService = new UserService(assertContext);
                var booksResult = userService.GetUserBooks(user);
                Assert.IsInstanceOfType(booksResult, typeof(IQueryable<Book>));
                Assert.IsTrue(booksResult.Select(br => br.Id).FirstOrDefault() == book.Id);
                Assert.IsTrue(booksResult.Select(br => br.Genre).FirstOrDefault() == book.Genre);
                Assert.IsTrue(booksResult.Select(br => br.Title).FirstOrDefault() == book.Title);
            }
        }
    }
}
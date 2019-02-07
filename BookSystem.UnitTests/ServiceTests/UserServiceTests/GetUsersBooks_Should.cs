using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Exceptions;
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
    public class GetUsersBooks_Should
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
        public async Task ReturnUsersBooks_WhenPassedValidUser()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnUsersBooks_WhenPassedValidUser")
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
            userBooksList.Add(usersBooks);


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
                var sut = await userService.GetUsersBooks(user);

                Assert.IsInstanceOfType(sut, typeof(IEnumerable<UsersBooks>));
                Assert.IsTrue(sut.Count() == 1);
            }
        }           
    }
}

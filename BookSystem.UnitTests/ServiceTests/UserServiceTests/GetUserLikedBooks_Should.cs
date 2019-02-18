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
    public class GetUserLikedBooks_Should
    {
        private DbContextOptions<BookSystemDbContext> contextOptions;
        private User user;
        private Book book;
        private UsersBooksLikes userLikedBooks;
        private string userId = "randomId";
        private string firstName = "randomFirstName";
        private string lastName = "randomLastName";
        private string title = "randomTitle";
        private string genre = "randomGenre";
        private string username = "randomUserName";
        private DateTime addedOn = DateTime.Now;

        [TestMethod]
        public async Task Return_CollectionOfUserLikedBooks__CountOfOne_WhenPassedValidParams()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "Return_CollectionOfUserLikedBooks__CountOfOne_WhenPassedValidParams")
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

            userLikedBooks = new UsersBooksLikes
            {
                Book = book,
                User = user
            };

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.UsersBooksLikes.AddAsync(userLikedBooks);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new UserService(assertContext);
                var userLikedBooksCollection = await sut.GetUserLikedBooks(user);
                Assert.IsInstanceOfType(userLikedBooksCollection, typeof(IEnumerable<UsersBooksLikes>));
                Assert.IsTrue(userLikedBooksCollection.Count() == 1);
            }
        }
    }
}

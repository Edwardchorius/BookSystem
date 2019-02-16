using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Exceptions;
using BookSystem.ServiceLayer.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.UnitTests.ServiceTests.BookServiceTests
{
    [TestClass]
    public class LikeBook_Should
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
        public async Task ReturnLikedBook_WhenPassed_ValidParams()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnLikedBook_WhenPassed_ValidParams")
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
            using ( var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.UsersBooks.AddAsync(usersBooks);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new BookService(assertContext);
                var likedBook = await sut.LikeBook(book.Id, user);
                Assert.IsInstanceOfType(likedBook, typeof(UsersBooksLikes));
                Assert.IsTrue(likedBook.UserId == user.Id);
                Assert.IsTrue(likedBook.BookId == book.Id);
            }
        }

        [TestMethod]
        public async Task Throw_EntityNotFoundException_WhenNoBookIsFound()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "Throw_EntityNotFoundException_WhenNoBookIsFound")
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
                var sut = new BookService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.LikeBook(0, user));
            }
        }                     
    }
}

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

namespace BookSystem.UnitTests.ServiceTests.ReviewServiceTests
{
    [TestClass]
    public class MakeReview_Should
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
        private string reviewText = "Random text for a random review test";
        private int bookIdForFail = 999;

        [TestMethod]
        public async Task ReturnAddedReview_When_PassedValidParams()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnAddedReview_When_PassedValidParams")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>(),
                Reviews = new List<Review>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                UsersBooks = new List<UsersBooks>(),
                Reviews = new List<Review>()
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
                assertContext.Attach(user);
                assertContext.Attach(usersBooks);
                var sut = new ReviewService(assertContext);
                Review reviewResult = await sut.MakeReview(user, book.Id, reviewText);
                Assert.IsInstanceOfType(reviewResult, typeof(Review));
                Assert.IsTrue(reviewResult.ReviewText == reviewText);
                Assert.IsTrue(reviewResult.Book == book);
                Assert.IsTrue(reviewResult.Author == user);
            }
        }

        [TestMethod]
        public async Task Throw_EntityNotFoundException_When_NoUserExists()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "Throw_EntityNotFoundException_When_NoUserExists")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>(),
                Reviews = new List<Review>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                UsersBooks = new List<UsersBooks>(),
                Reviews = new List<Review>()
            };

            usersBooks = new UsersBooks
            {
                User = user,
                Book = book
            };

            user.IsDeleted = true;

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                assertContext.Attach(user);
                assertContext.Attach(usersBooks);
                var sut = new ReviewService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.MakeReview(user, book.Id, reviewText));
            }
        }

        [TestMethod]
        public async Task Throw_EntityNotFoundException_When_NoBookToReviewExists()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "Throw_EntityNotFoundException_When_NoBookToReviewExists")
                .Options;

            user = new User
            {
                Id = userId,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                UsersBooks = new List<UsersBooks>(),
                Reviews = new List<Review>()
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
                assertContext.Attach(user);
                //assertContext.Attach(usersBooks);
                var sut = new ReviewService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.MakeReview(user, bookIdForFail, reviewText));
            }
        }
    }
}

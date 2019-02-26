using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.DTO;
using BookSystem.ServiceLayer.Data.Exceptions;
using BookSystem.ServiceLayer.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.UnitTests.ServiceTests.ReviewServiceTests
{
    [TestClass]
    public class GetBookReviews_Should
    {
        private DbContextOptions<BookSystemDbContext> contextOptions;
        private Book book;
        private User user;
        private Review review;
        private string firstName = "randomFirstName";
        private string lastName = "randomLastName";
        private string title = "randomTitle";
        private string genre = "randomGenre";
        private string reviewText = "Random text for a random review test";

        [TestMethod]
        public async Task ReturnCollectionOfReviews_When_PassedValidParams()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCollectionOfReviews_When_PassedValidParams")
                .Options;

            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Reviews = new List<Review>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                Reviews = new List<Review>()
            };

            review = new Review
            {
                AuthorId = user.Id,
                BookId = book.Id,
                ReviewText = reviewText,
                Comments = new List<Comment>(),
                Ratings = new Dictionary<string, int>(),
                CreatedOn = DateTime.Now
            };

            user.Reviews.Add(review);
            book.Reviews.Add(review);

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.Books.AddAsync(book);
                await actContext.Reviews.AddAsync(review);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new ReviewService(assertContext);
                var listOfReviews = await sut.GetBookReviews(book.Id);
                Assert.IsInstanceOfType(listOfReviews, typeof(IEnumerable<ReviewDTO>));
                Assert.IsTrue(listOfReviews.Count() == 1);
                Assert.IsTrue(listOfReviews.Select(r => r.Book.Title).First() == book.Title);
            }
        }

        [TestMethod]
        public async Task Throw_EntityNotFoundException_When_NoBookReviewsAreFound()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "Throw_EntityNotFoundException_When_NoBookReviewsAreFound")
                .Options;

            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Reviews = new List<Review>()
            };

            book = new Book
            {
                Title = title,
                Genre = genre,
                Reviews = new List<Review>()
            };

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.Books.AddAsync(book);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new ReviewService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await sut.GetBookReviews(book.Id));
            }
        }
    }
}

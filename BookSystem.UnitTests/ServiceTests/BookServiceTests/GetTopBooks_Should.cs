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

namespace BookSystem.UnitTests.ServiceTests.BookServiceTests
{
    [TestClass]
    public class GetTopBooks_Should
    {
        private DbContextOptions<BookSystemDbContext> contextOptions;
        private User user;
        private Book book;
        private Review bookReview;
        private string userId = "randomId";
        private string firstName = "randomFirstName";
        private string lastName = "randomLastName";
        private string title = "randomTitle";
        private string genre = "randomGenre";
        private string username = "randomUserName";
        private string reviewText = "Random text for a random review test";
        private string sortOrder = "title_desc";

        [TestMethod]
        public async Task ReturnCollectionOFBookDTO_When_PassedValidArguments()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCollectionOFBookDTO_When_PassedValidArguments")
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

            bookReview = new Review
            {
                Book = book,
                Author = user,
                ReviewText = reviewText,
                Comments = new List<Comment>(),
                Ratings = new Dictionary<string, int>()
            };

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.Reviews.AddAsync(bookReview);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new BookService(assertContext);
                var bookDtoList = await sut.GetTopBooks(sortOrder);
                Assert.IsInstanceOfType(bookDtoList, typeof(IEnumerable<BookDTO>));
                Assert.IsTrue(bookDtoList.Count() == 1);
            }
        }


        [TestMethod]
        public async Task ReturnEmptyCollectionOfBookDTO_When_NoBooksWereRetrieved()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnEmptyCollectionOfBookDTO_When_NoBooksWereRetrieved")
                .Options;

            //Act & Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new BookService(assertContext);
                var emptyCollection = await sut.GetTopBooks(sortOrder);
                Assert.IsTrue(emptyCollection.Count() == 0);
                Assert.IsInstanceOfType(emptyCollection, typeof(IEnumerable<BookDTO>));
            }
        }


        [TestMethod]
        public async Task ReturnBookDTOCollection_Sorted_WhenPassedSortingCriteria()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnBookDTOCollection_Sorted_WhenPassedSortingCriteria")
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

            bookReview = new Review
            {
                Book = book,
                Author = user,
                ReviewText = reviewText,
                Comments = new List<Comment>(),
                Ratings = new Dictionary<string, int>()
            };

            Review bookReviewTwo = new Review
            {
                Book = new Book
                {
                    Title = "DemoTitle To Check the Sorting Criteria",
                    Genre = "DemoGenre"
                },
                Author = user,
                ReviewText = "This is a demo reviewText to check the sorting if is done correctly or not",
                Comments = new List<Comment>(),
                Ratings = new Dictionary<string, int>()
            };

            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.Reviews.AddAsync(bookReview);
                await actContext.Reviews.AddAsync(bookReviewTwo);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new BookService(assertContext);
                var bookDTOCollection = await sut.GetTopBooks(sortOrder);
                Assert.IsInstanceOfType(bookDTOCollection, typeof(IEnumerable<BookDTO>));
                Assert.IsTrue(bookDTOCollection.Count() == 2);
                Assert.IsTrue(bookDTOCollection.First().Title == bookReviewTwo.Book.Title);
            }
        }


        [TestMethod]
        public async Task ReturnSingleBookDTO_When_SearchedByKeyWord()
        {
            //Arrange
            contextOptions = new DbContextOptionsBuilder<BookSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnSingleBookDTO_When_SearchedByKeyWord")
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

            bookReview = new Review
            {
                Book = book,
                Author = user,
                ReviewText = reviewText,
                Comments = new List<Comment>(),
                Ratings = new Dictionary<string, int>()
            };

            Review bookReviewTwo = new Review
            {
                Book = new Book
                {
                    Title = "DemoTitle To Check the Sorting Criteria",
                    Genre = "DemoGenre"
                },
                Author = user,
                ReviewText = "This is a demo reviewText to check the sorting if is done correctly or not",
                Comments = new List<Comment>(),
                Ratings = new Dictionary<string, int>()
            };


            //Act
            using (var actContext = new BookSystemDbContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.Reviews.AddAsync(bookReview);
                await actContext.Reviews.AddAsync(bookReviewTwo);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new BookSystemDbContext(contextOptions))
            {
                var sut = new BookService(assertContext);
                var bookDTOResult = await sut.GetTopBooks(sortOrder, title);
                Assert.IsInstanceOfType(bookDTOResult, typeof(IEnumerable<BookDTO>));
                Assert.IsTrue(bookDTOResult.Count() == 1);
                Assert.IsTrue(bookDTOResult.First().Title == title);
            }
        }
    }
}

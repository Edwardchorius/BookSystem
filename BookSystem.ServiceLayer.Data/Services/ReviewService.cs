using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.DTO;
using BookSystem.ServiceLayer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BookSystemDbContext _context;

        public ReviewService(BookSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Review> MakeReview(User author, int bookId, string reviewText)
        {
            try
            {
                if (author.IsDeleted == true)
                {
                    throw new EntityNotFoundException("User not found");
                }

                if (await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId) == null)
                {
                    throw new EntityNotFoundException("Book to review not found");
                }

                Review reviewToMake = new Review
                {
                    Author = author,
                    BookId = bookId,
                    ReviewText = reviewText,
                    Comments = new List<Comment>(),
                    Ratings = new Dictionary<string, int>(),
                    CreatedOn = DateTime.Now
                };

                reviewToMake.Ratings.Add("Copyright", 1);

                await _context.Reviews.AddAsync(reviewToMake);
                await _context.SaveChangesAsync();

                return reviewToMake;
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException("Book/User not found", ex);
            }
        }

        public async Task<Review> GetReview(int reviewId)
        {
            var review = await _context.Reviews
                .Where(r => r.Id == reviewId)
                .FirstOrDefaultAsync();

            return review;
        }

        public async Task<IEnumerable<ReviewDTO>> GetBookReviews(int bookId)
        {
            try
            {
                var bookReviews = await _context
                .Reviews
                .Include(r => r.Book)
                .Include(r => r.Comments)
                .ThenInclude(c => c.Author)
                .Where(r => r.BookId == bookId)
                .ToListAsync();



                var result = bookReviews
                    .Select(review => new ReviewDTO()
                    {
                        Author = review.Author,
                        AuthorId = review.AuthorId,
                        ReviewId = review.Id,
                        Book = review.Book,
                        Title = review.Book.Title,
                        ReviewText = review.ReviewText,
                        Comments = review.Comments
                    });

                if (!result.Any())
                {
                    throw new EntityNotFoundException("Could not find any existing book reviews");
                }

                return result;
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException("Could not retrieve book reviews", ex);
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("Could not load null data", ex);
            }
        }
    }
}

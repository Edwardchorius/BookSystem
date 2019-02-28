using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Contracts
{
    public interface IReviewService
    {
        Task<Review> MakeReview(User author, int bookId, string reviewText);

        Task<Review> GetReview(int reviewId);

        Task<IEnumerable<ReviewDTO>> GetBookReviews(int bookId);
    }
}

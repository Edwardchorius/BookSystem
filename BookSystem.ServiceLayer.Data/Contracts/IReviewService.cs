using BookSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Contracts
{
    public interface IReviewService
    {
        Task<Review> MakeReview(User author, int bookId, string reviewText);
    }
}

using BookSystem.ServiceLayer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Models.ReviewViewModels
{
    public class BookReviewsViewModel
    {
        public BookReviewsViewModel()
        {

        }

        public BookReviewsViewModel(IEnumerable<ReviewDTO> bookReviews)
        {
            this.BookReviews = bookReviews;
            this.Title = bookReviews.Select(r => r.Book.Title).FirstOrDefault();
        }

        public string Title { get; set; }

        public IEnumerable<ReviewDTO> BookReviews { get; set; }
    }
}

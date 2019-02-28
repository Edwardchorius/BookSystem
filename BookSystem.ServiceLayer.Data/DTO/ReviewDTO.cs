using BookSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookSystem.ServiceLayer.Data.DTO
{
    public class ReviewDTO
    {
        public ReviewDTO()
        {

        }

        public ReviewDTO(Review review)
        {
            this.Author = review.Author;
            this.AuthorId = review.AuthorId;
            this.Book = review.Book;
            this.Title = review.Book.Title;
            this.ReviewText = review.ReviewText;
            this.ReviewId = review.Id;
            this.Comments = review.Comments;
        }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public Book Book { get; set; }

        public string Title { get; set; }

        public int ReviewId { get; set; }

        public string ReviewText { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}

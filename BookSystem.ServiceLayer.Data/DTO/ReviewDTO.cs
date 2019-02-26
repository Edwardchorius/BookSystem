using BookSystem.Data.Models;
using System;
using System.Collections.Generic;
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
        }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public Book Book { get; set; }

        public string Title { get; set; }

        public string ReviewText { get; set; }
    }
}

using BookSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Models.ReviewViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {

        }

        public CommentViewModel(Comment comment)
        {
            this.Id = comment.Id;
            this.Content = comment.Content;
            this.Author =  comment.Author;
            this.AuthorId = comment.AuthorId;
            this.ReviewId = comment.ReviewId;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public int ReviewId { get; set; }
    }
}

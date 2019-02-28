using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Services
{
    public class CommentService : ICommentService
    {
        private readonly BookSystemDbContext _context;

        public CommentService(BookSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> PostComment(int reviewId, string content, User author)
        {
            var comment = new Comment
            {
                Content = content,
                ReviewId = reviewId,
                Author = author,
                AuthorId = author.Id,
                CreatedOn = DateTime.Now
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();


            return comment;
        }
    }
}

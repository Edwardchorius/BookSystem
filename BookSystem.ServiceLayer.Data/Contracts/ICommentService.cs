using BookSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Contracts
{
    public interface ICommentService
    {
        Task<Comment> PostComment(int reviewId, string content, User author);
    }
}

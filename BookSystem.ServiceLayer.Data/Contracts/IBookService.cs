using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Contracts
{
    public interface IBookService
    {
        Task<Book> AddBook(User user, string title, string genre);

        Task<Book> GetById(User user, int bookId);

        Task<UsersBooksLikes> LikeBook(int bookId, User user);

        Task<UsersBooksLikes> DislikeBook(int bookId, User user);

        Task<IEnumerable<BookDTO>> GetTopBooks(string sortOrder, string searchByTitle = null);
    }
}

using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Services
{
    public class UserService : IUserService
    {
        private readonly BookSystemDbContext _context;

        public UserService(BookSystemDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<UsersBooksLikes>> GetUserLikedBooks(User user)
        {
            try
            {
                var userId = user.Id;
                var userLikedBooks = await _context.UsersBooksLikes
                    .Include(ubl => ubl.Book)
                    .Where(ubl => ubl.User == user)
                    .ToListAsync();

                return userLikedBooks;
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException("Current user was not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong...", ex);
            }
        }


        public IQueryable<Book> GetUserBooks(User user)
        {
            var userBooks = from b in _context.UsersBooks
                           .Where(u => u.User == user)
                            select b.Book;

            return userBooks;
        }

        public IQueryable<Book> PagedUserBooks(User user,
            string sortOrder, string currentFilter, string searchString, int? page = 1)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var userBooks = GetUserBooks(user);

            if (!string.IsNullOrEmpty(searchString))
            {

                userBooks = userBooks.Where(b => b.Genre.Contains(searchString)
                || b.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "genre_desc":
                    userBooks = userBooks.OrderByDescending(b => b.Genre);
                    break;
                case "Date":
                    userBooks = userBooks.OrderBy(b => b.CreatedOn);
                    break;
                case "date_desc":
                    userBooks = userBooks.OrderByDescending(b => b.CreatedOn);
                    break;
                default:
                    userBooks = userBooks.OrderBy(b => b.Title);
                    break;
            }

            return userBooks;
        }
    }
}

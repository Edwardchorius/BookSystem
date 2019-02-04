using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<UsersBooks>> GetUserBooks(string username)
        {
            try
            {
                var user = await _context.Users.FindAsync(username);

                if (user == null)
                {
                    throw new UserNotFoundException("Current user was not found");
                }

                var userBooks = await _context.UsersBooks.Include(b => b.Book).Where(u => u.User.UserName == username).ToListAsync();

                return userBooks;
            }
            catch (Exception ex)
            {
                throw new CouldNotRetrieveUserBooksException("Could not retrieve the current user's books", ex);
            }
        }
    }
}

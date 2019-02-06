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

        public async Task<IEnumerable<UsersBooks>> GetUserBooks(User user)
        {
            try
            {
                var userBooks = await _context.UsersBooks.Include(b => b.Book).Where(u => u.User == user).ToListAsync();

                return userBooks;
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException("Could not retrieve the current user's books", ex);
            }
        }
    }
}

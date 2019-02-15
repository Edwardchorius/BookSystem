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
    public class BookService : IBookService
    {
        private readonly BookSystemDbContext _context;

        public BookService(BookSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetById(User author, int id)
        {
            var book = await _context.UsersBooks
                .Where(b => b.BookId == id && b.User == author)
                .Select(b => b.Book)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new EntityNotFoundException("Could not retrieve book with current ID");
            }

            return book;
        }

        public async Task<Book> AddBook(User user, string title, string genre)
        {
            try
            {
                var userId = user.Id;

                //var userBooks = await _userService.GetUsersBooks(user);

                var userBooks = await _context.UsersBooks.Include(b => b.Book).Where(u => u.User == user).ToListAsync();

                if (!_context.Users.Contains(user))
                {
                    throw new EntityNotFoundException("User does not exist/Could not retrieve the current user's books");
                }

                if (userBooks.Select(b => b.Book).Any(b => b.Title == title))
                {
                    throw new EntityAlreadyExistsException("Book already added by the current user");
                }

                Book bookToAdd = new Book
                {
                    Title = title,
                    Genre = genre,
                    Reviews = new List<Review>(),
                    UsersBooks = new List<UsersBooks>(),
                    UsersBooksLikes = new List<UsersBooksLikes>(),
                    CreatedOn = DateTime.Now
                };

                var usersBooks = new UsersBooks { Book = bookToAdd, User = user };

                _context.Books.Add(bookToAdd);
                _context.UsersBooks.Update(usersBooks);

                await _context.SaveChangesAsync();

                return bookToAdd;
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException("Could not retrieve the current user's books", ex);
            }
            catch (EntityAlreadyExistsException ex)
            {
                throw new EntityAlreadyExistsException("Book already added by the current user", ex);
            }
        }
    }
}

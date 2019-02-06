using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.Exceptions;
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
        private readonly IUserService _userService;

        public BookService(BookSystemDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Book> AddBook(User user, string title, string genre, int likes = 0)
        {
            var userId = user.Id;

            var userBooks = await _userService.GetUserBooks(user);

            if (userBooks.Select(b => b.Book).Any(b => b.Title == title))
            {
                throw new EntityAlreadyExistsException("Book already added by the current user");
            }

            Book bookToAdd = new Book
            {
                Title = title,
                Genre = genre,
                Likes = likes,
                Reviews = new List<Review>(),
                UsersBooks = new List<UsersBooks>()
            };

            var usersBooks = new UsersBooks { Book = bookToAdd, User = user };

            _context.Books.Add(bookToAdd);
            _context.UsersBooks.Update(usersBooks);

            await _context.SaveChangesAsync();

            return bookToAdd;
        }
    }
}

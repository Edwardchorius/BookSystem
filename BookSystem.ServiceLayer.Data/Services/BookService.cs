using BookSystem.Data;
using BookSystem.Data.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.ServiceLayer.Data.DTO;
using BookSystem.ServiceLayer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<IEnumerable<BookDTO>> GetTopBooks(string sortOrder, string searchByTitle = null)
        {
            try
            {
                var books = _context
                .Reviews
                .Include(r => r.Book)
                .Include(r => r.Ratings)
                .OrderBy(r => r.Ratings.Values.Sum())
                .Select(r => r.Book)
                .Distinct()
                .Include(b => b.Reviews)
                .Take(10)
                .AsQueryable();

                if (books.Count() == 0)
                {
                    return new List<BookDTO>();
                }

                if (!string.IsNullOrWhiteSpace(searchByTitle))
                {
                    books = books.Where(b => b.Title.Contains(searchByTitle));
                }


                if (sortOrder != null && !sortOrder.Contains("_desc"))
                {
                    books = books.OrderBy(book => book);
                }
                else
                {
                    books = books.OrderByDescending(book => book);
                }


                var result = await books
                    .Select(book => new BookDTO()
                    {
                        Title = book.Title,
                        Genre = book.Genre,
                        TotalRating = book.Reviews.Select
                        (r => r.Ratings.Values.Sum()) == null ? 0 : book.Reviews.Select(r => r.Ratings.Values.Sum()).Sum()
                    })
                    .ToListAsync();

                return result;
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException("Could not load top books!",ex);
            }         
        }

        public async Task<Book> GetById(User user, int id)
        {
            var book = await _context.UsersBooks
                .Where(b => b.BookId == id && b.User.Id == user.Id)
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

        public async Task<UsersBooksLikes> LikeBook(int bookId, User user)
        {
            var userId = user.Id;
            var userBook = await _context.UsersBooks
                .Include(ub => ub.Book)
                .Include(ub => ub.User)
                .Where(ub => ub.BookId == bookId && ub.User == user).FirstOrDefaultAsync();

            if (bookId == 0 || user == null || userBook == null)
            {
                throw new EntityNotFoundException("Could not retrieve current book/user");
            }

            var doesExist = await _context.UsersBooksLikes
                .Where(ubl => ubl.Book == userBook.Book && ubl.User == user)
                .FirstOrDefaultAsync();

            if (doesExist != null && doesExist.IsDeleted == false)
            {
                throw new EntityAlreadyExistsException("User has already liked this book");
            }


            if (doesExist == null)
            {           
                var likedBook = new UsersBooksLikes
                {
                    BookId = bookId,
                    UserId = userId
                };

                await _context.UsersBooksLikes.AddAsync(likedBook);
                await _context.SaveChangesAsync();

                return likedBook;
            }

            else 
            {
                doesExist.IsDeleted = false;
                _context.UsersBooksLikes.Update(doesExist);
                await _context.SaveChangesAsync();

                return doesExist;
            }
        }

        public async Task<UsersBooksLikes> DislikeBook(int bookId, User user)
        {
            var userId = user.Id;
            var userBook = await _context.UsersBooks
                .Include(ub => ub.Book)
                .Include(ub => ub.User)
                .Where(ub => ub.BookId == bookId && ub.User == user).FirstOrDefaultAsync();

            if (bookId == 0 || user == null || userBook == null)
            {
                throw new EntityNotFoundException("Could not retrieve current book/user");
            }

            var doesExist = await _context.UsersBooksLikes
                .Where(ubl => ubl.Book == userBook.Book && ubl.User == user)
                .FirstOrDefaultAsync();

            if (doesExist == null)
            {
                throw new EntityNotFoundException("Current book to dislike not found");
            }

            doesExist.IsDeleted = true;
            doesExist.DeletedOn = DateTime.Now;

            _context.UsersBooksLikes.Update(doesExist);
            await _context.SaveChangesAsync();

            return doesExist;
        }
    }
}

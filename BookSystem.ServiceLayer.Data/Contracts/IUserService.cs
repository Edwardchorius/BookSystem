using BookSystem.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Contracts
{
    public interface IUserService
    {
        IQueryable<Book> GetUserBooks(User user);

        IQueryable<Book> PagedUserBooks(User user, string sortOrder, string currentFilter, string searchString, int? page = 1);
    }
}

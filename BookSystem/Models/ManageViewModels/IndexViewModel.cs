using BookSystem.Data.Models;
using BookSystem.Extensions;
using BookSystem.Models.BookViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            
        }

        public IndexViewModel(PaginatedList<Book> paginatedBooks, IEnumerable<UsersBooksLikes> userLikedBooks)
        {
            this.UserLikedBooks = userLikedBooks;
            this.PaginatedBooks = paginatedBooks;
        }

        public IEnumerable<UsersBooksLikes> UserLikedBooks { get; set; }

        public PaginatedList<Book> PaginatedBooks { get; set; }
    }
}

using BookSystem.ServiceLayer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Models.HomeViewModels
{
    public class TopBooksViewModel
    {
        public TopBooksViewModel()
        {

        }

        public TopBooksViewModel(IEnumerable<BookDTO> topBooks)
        {
            this.TopBooks = topBooks;
        }

        public IEnumerable<BookDTO> TopBooks { get; set; }
    }
}

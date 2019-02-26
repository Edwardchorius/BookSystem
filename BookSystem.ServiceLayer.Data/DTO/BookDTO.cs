using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.ServiceLayer.Data.DTO
{
    public class BookDTO
    {
        public BookDTO()
        {

        }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int TotalRating { get; set; }
    }
}

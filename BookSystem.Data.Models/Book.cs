using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }
    }
}

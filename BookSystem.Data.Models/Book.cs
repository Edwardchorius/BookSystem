using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSystem.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int Likes { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }
    }
}

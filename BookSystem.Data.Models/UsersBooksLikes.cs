

using BookSystem.Data.Models.Abstract;
using System;

namespace BookSystem.Data.Models
{
    public class UsersBooksLikes : IDeletable
    {
        public string UserId { get; set; }

        public int BookId { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}

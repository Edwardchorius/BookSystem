

namespace BookSystem.Data.Models
{
    public class UsersBooks
    {
        public string UserId { get; set; }

        public int BookId { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}

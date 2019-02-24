using BookSystem.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BookSystem.Models.BookViewModels
{
    public class MakeReviewViewModel
    {
        public MakeReviewViewModel()
        {

        }

        public MakeReviewViewModel(User author, Book book)
        {
            this.Book = book;
            this.Author = author;
        }

        public Book Book { get; set; }

        public User Author { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(20)]
        public string Content { get; set; }
    }
}

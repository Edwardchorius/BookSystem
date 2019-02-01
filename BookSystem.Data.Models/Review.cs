using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSystem.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public User Author { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public Book Book { get; set; }

        public string ReviewText { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }
    }
}

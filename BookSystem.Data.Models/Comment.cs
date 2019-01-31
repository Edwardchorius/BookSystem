using BookSystem.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSystem.Data.Models
{
    public class Comment : IAuditable, IDeletable
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string Content { get; set; }

        public int Likes { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public User Author { get; set; }
        
        public int BookId { get; set; }

        [Required]
        public Book Book { get; set; }
    }
}

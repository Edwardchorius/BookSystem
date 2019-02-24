using BookSystem.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSystem.Data.Models
{
    public class Book : IAuditable, IDeletable
    {
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }

        public ICollection<UsersBooksLikes> UsersBooksLikes { get; set; }
    }
}

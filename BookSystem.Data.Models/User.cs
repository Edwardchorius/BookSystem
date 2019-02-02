using BookSystem.Data.Models.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSystem.Data.Models
{
    public class User : IdentityUser, IAuditable, IDeletable
    {
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "FirstName cannot be more than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "LastName cannot be more than 50 characters")]
        public string LastName { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }
    }
}

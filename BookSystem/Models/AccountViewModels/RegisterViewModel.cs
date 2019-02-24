using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Firstname cannot be more than 50 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Firstname must countain only letters!")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Lastname cannot be more than 50 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Lastname must countain only letters!")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,20}$", ErrorMessage = "Password should contain at least 1 uppercase letter, 1 special symbol and between 8 & 20 symbols in total")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,20}$", ErrorMessage = "Password should contain at least 1 uppercase letter, 1 special symbol and between 8 & 20 symbols in total")]
        [Compare("Password", ErrorMessage = "Must match with password.")]
        public string ConfirmPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Models.BookViewModels
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {

        }

        [Required]
        [MinLength(5), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [MinLength(5), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Genre { get; set; }
    }
}

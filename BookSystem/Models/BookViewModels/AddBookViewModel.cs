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
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}

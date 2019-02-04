using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.ServiceLayer.Data.Exceptions
{
    public class BookAlreadyAddedException : Exception
    {
        public BookAlreadyAddedException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}

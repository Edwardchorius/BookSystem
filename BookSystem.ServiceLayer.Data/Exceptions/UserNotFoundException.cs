using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.ServiceLayer.Data.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}

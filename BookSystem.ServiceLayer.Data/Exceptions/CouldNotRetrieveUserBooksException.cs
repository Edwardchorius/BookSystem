using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.ServiceLayer.Data.Exceptions
{
    public class CouldNotRetrieveUserBooksException : Exception
    {
        public CouldNotRetrieveUserBooksException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}

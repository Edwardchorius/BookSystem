using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.ServiceLayer.Data.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}

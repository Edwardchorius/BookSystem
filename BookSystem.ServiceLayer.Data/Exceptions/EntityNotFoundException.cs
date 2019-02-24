using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.ServiceLayer.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}

using BookSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem.ServiceLayer.Data.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UsersBooks>> GetUserBooks(string username);
    }
}

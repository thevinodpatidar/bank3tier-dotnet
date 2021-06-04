using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Models;

namespace Bank3Tier.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> Register(User user);
        Task<User> UpdateUser(User userToBeUpdated);
        Task DeleteUser(User user);
    }
}

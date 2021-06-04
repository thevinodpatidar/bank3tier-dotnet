using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Models;

namespace Bank3Tier.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> UpdateUser(User user);
    }
}

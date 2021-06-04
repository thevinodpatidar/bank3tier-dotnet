using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Models;
using Bank3Tier.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank3Tier.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Bank3TierDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await Bank3TierDbContext.Users.ToListAsync();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return Bank3TierDbContext.Users.SingleOrDefaultAsync(a => a.Id == id);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return Bank3TierDbContext.Users.SingleOrDefaultAsync(a => a.Username ==  username);
        }

        public Task<User> UpdateUser(User user)
        {
            return await Bank3TierDbContext.Users.Update(user);
        }

        private Bank3TierDbContext Bank3TierDbContext
        {
            get { return Context as Bank3TierDbContext; }
        }
    }
}

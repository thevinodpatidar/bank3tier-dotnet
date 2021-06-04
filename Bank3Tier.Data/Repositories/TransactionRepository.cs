using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Models;
using Bank3Tier.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank3Tier.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(Bank3TierDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Transaction>> GetAllTransactionAsync()
        {
            return await Bank3TierDbContext.Transactions.ToListAsync();
        }

        public Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return Bank3TierDbContext.Transactions.SingleOrDefaultAsync(a => a.Id == id);
        }

        private Bank3TierDbContext Bank3TierDbContext
        {
            get { return Context as Bank3TierDbContext; }
        }
    }
}

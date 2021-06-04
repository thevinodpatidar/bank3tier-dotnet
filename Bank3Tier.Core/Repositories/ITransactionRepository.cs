using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Models;

namespace Bank3Tier.Core.Repositories
{
     public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllTransactionAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
    }
}

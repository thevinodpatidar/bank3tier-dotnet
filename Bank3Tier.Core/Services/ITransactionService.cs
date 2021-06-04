using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Models;

namespace Bank3Tier.Core.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransaction();
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> CreateTransaction(int userId,Transaction transaction);
    }
}

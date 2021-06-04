using System;
using System.Threading.Tasks;
using Bank3Tier.Core.Repositories;
using Bank3Tier.Data.Repositories;

namespace Bank3Tier.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Bank3TierDbContext _context;
        private UserRepository _userRepository;
        private TransactionRepository _transactionRepository;

        public UnitOfWork(Bank3TierDbContext context)
        {
            this._context = context;
        }

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

        public ITransactionRepository Transactions => _transactionRepository = _transactionRepository ?? new TransactionRepository(_context);

 
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

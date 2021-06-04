using System;
using System.Threading.Tasks;

namespace Bank3Tier.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITransactionRepository Transactions { get; }
        Task<int> CommitAsync();
    }
}

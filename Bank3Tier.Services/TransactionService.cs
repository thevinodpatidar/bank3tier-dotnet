using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Helpers;
using Bank3Tier.Core.Models;
using Bank3Tier.Core.Repositories;
using Bank3Tier.Core.Services;

namespace Bank3Tier.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Transaction> CreateTransaction(int userId, Transaction transaction)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (transaction.Mode == "DEPOSIT")
            {
                user.Balance = user.Balance + transaction.Amount;
                _unitOfWork.Users.UpdateUser(user);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                if (user.Balance < transaction.Amount)
                {
                    throw new AppException("Insufficient Balance");
                }
                user.Balance = user.Balance - transaction.Amount;
                _unitOfWork.Users.UpdateUser(user);
                await _unitOfWork.CommitAsync();
            }

            Transaction addTranasction = new Transaction
            {
                UserId = userId,
                Amount = transaction.Amount,
                Mode = transaction.Mode
            };
            await _unitOfWork.Transactions
                .AddAsync(addTranasction);
            await _unitOfWork.CommitAsync();

            return addTranasction;
        }

        public async Task DeleteUser(User artist)
        {
            _unitOfWork.Users.Remove(artist);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransaction()
        {
            return await _unitOfWork.Transactions.GetAllAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _unitOfWork.Transactions.GetTransactionByIdAsync(id);
        }
    }
}

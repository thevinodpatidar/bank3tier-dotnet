using System.Collections.Generic;
using System.Threading.Tasks;
using Bank3Tier.Core.Helpers;
using Bank3Tier.Core.Models;
using Bank3Tier.Core.Repositories;
using Bank3Tier.Core.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Bank3Tier.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<User> Register(User user)
        {
            if (_unitOfWork.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username '" + user.Username + "' is already taken");

            await _unitOfWork.Users
                .AddAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task DeleteUser(User artist)
        {
            _unitOfWork.Users.Remove(artist);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            if (user.Username != user.Username && _unitOfWork.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username '" + user.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(user.Password))
                user.Password = BCryptNet.HashPassword(user.Password);

            await _unitOfWork.Users.UpdateUser(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _unitOfWork.Users.GetUserByUsernameAsync(username);
        }
    }
}

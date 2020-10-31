using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WalletPay.Data.Context;
using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.UserRepositories
{
    public class UserDbRepository : IUserRepository
    {
        private readonly WalletPayDbContext _dbContext;

        public UserDbRepository()
        {
            _dbContext = new WalletPayDbContext();
        }

        public User GetUser(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public Task<User> GetUserAsync(int userId)
        {
            return _dbContext.Users.FindAsync(userId).AsTask();
        }
    }
}

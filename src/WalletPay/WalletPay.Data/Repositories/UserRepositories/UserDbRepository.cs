using System.Threading.Tasks;

using WalletPay.Data.Context;
using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.UserRepositories
{
    public class UserDbRepository : RepositoryDbContextBase<User>, IUserRepository
    {
        public UserDbRepository(WalletPayDbContext dbContext) 
            : base(dbContext)
        {
        }

        public Task<User> GetUserAsync(int userId) => GetFirstWhereAsync(u => u.Id == userId);
    }
}

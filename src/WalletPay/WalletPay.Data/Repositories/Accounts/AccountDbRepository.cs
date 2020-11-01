
using WalletPay.Data.Context;
using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.Accounts
{
    public class AccountDbRepository : RepositoryDbContextBase<Account>, IAccountRepository
    {
        public AccountDbRepository(WalletPayDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}

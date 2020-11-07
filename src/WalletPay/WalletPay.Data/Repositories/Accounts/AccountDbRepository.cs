
using WalletPay.Data.Context;
using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Transactions;

namespace WalletPay.Data.Repositories.Accounts
{
    public class AccountDbRepository : RepositoryDbContextBase<Account>, IAccountRepository
    {
        public AccountDbRepository(WalletPayDbContext dbContext)
            : base(dbContext)
        {
        }

        public IDatabaseTransaction BeginTransaction()
        {
            var transaction = Context.Database.BeginTransaction();
            return new WalletPayTransaction(transaction);
        }
    }
}

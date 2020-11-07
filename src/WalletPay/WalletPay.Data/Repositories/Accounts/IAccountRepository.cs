
using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Transactions;

namespace WalletPay.Data.Repositories.Accounts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IDatabaseTransaction BeginTransaction();
    }
}

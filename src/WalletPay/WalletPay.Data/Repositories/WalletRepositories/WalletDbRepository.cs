
using WalletPay.Data.Context;
using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public class WalletDbRepository : RepositoryDbContextBase<Wallet>, IWalletRepository
    {
        public WalletDbRepository(WalletPayDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}

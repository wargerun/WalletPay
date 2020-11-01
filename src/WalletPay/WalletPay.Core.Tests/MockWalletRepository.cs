using System.Collections.Generic;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.WalletRepositories;

namespace WalletPay.Core.Tests
{
    public class MockWalletRepository : CollectionRepositoryBase<Wallet>, IWalletRepository
    {
        public MockWalletRepository(IEnumerable<Wallet> wallets)
            : base(wallets)
        {
        }
    }
}

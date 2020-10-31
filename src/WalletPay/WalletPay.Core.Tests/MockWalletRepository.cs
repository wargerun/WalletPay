
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.WalletRepositories;

namespace WalletPay.Core.Tests
{
    public class MockWalletRepository : IWalletRepository
    {
        private readonly IEnumerable<Wallet> _wallets;

        public MockWalletRepository(IEnumerable<Wallet> wallets)
        {
            _wallets = wallets;
        }

        public Task<Wallet> GetWalletByUserIdAsync(int userId, CancellationToken token = default) => Task.FromResult(_wallets.SingleOrDefault(w => w.UserId == userId));

        public Task<Wallet> GetWalletByIdAsync(int walletId, CancellationToken token = default) => Task.FromResult(_wallets.SingleOrDefault(w => w.Id == walletId));

        public Task<Account> AddAccountAsync(Account account, CancellationToken token = default)
        {
            return Task.FromResult(account);
        }

        public Task<Account> UpdateAccountAsync(int accountId, decimal newAmmount, CancellationToken token = default)
        {
            Account account = _wallets.Where(a => a.Accounts != null).SelectMany(w => w.Accounts).SingleOrDefault(a => a.Id == accountId);
            account.Amount = newAmmount;
            return Task.FromResult(account);
        }
    }
}

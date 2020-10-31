using Microsoft.EntityFrameworkCore;

using System;
using System.Threading;
using System.Threading.Tasks;

using WalletPay.Data.Context;
using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public class WalletDbRepository : IWalletRepository
    {
        private readonly WalletPayDbContext _dbContext;

        public WalletDbRepository()
        {
            _dbContext = new WalletPayDbContext();
        }

        public async Task<Account> AddAccountAsync(Account account, CancellationToken token = default)
        {
            Wallet wallet = await GetWalletByIdAsync(account.WalletId, token);

            if (wallet is null)
            {
                throw new InvalidOperationException($"Кошелек с id={account.WalletId} не найден.");
            }

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync(token);
            return account;
        }

        public Task<Wallet> GetWalletByIdAsync(int walletId, CancellationToken token = default) => _dbContext.Wallets.Include(w => w.Accounts).SingleOrDefaultAsync(w => w.Id == walletId, token);

        public Task<Wallet> GetWalletByUserIdAsync(int userId, CancellationToken token = default) => _dbContext.Wallets.Include(w => w.Accounts).SingleOrDefaultAsync(w => w.UserId == userId, token);

        public async Task<Account> UpdateAccountAsync(int accountId, decimal newAmmount, CancellationToken token = default)
        {
            Account account = await _dbContext.Accounts.SingleOrDefaultAsync(a => a.Id == accountId, token);

            if (account is null)
            {
                throw new InvalidOperationException($"Счет {accountId} пользователя не найден.");
            }

            account.Amount = newAmmount;

            await _dbContext.SaveChangesAsync(token);

            return account;
        }
    }
}

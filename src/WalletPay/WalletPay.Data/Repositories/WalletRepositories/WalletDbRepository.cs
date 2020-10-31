using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Account> AddAccountAsync(int walletId, string codeCurrency, decimal amount)
        {
            Wallet wallet = await GetWalletByIdAsync(walletId);

            if (wallet is null)
            {
                throw new InvalidOperationException($"Кошелек с id={walletId} не найден.");
            }

            Account account = new()
            {
                Amount = amount,
                Currency = codeCurrency,
                WalletId = walletId,
            };

            _dbContext.Accounts.Add(account);

            await _dbContext.SaveChangesAsync();

            return account;
        }

        public Task<Wallet> GetWalletByIdAsync(int walletId)
        {
            return _dbContext.Wallets.FindAsync(walletId).AsTask();
        }

        public Task<Wallet> GetWalletByUserIdAsync(int userId)
        {
            return _dbContext.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Account> UpdateAccountAsync(int accountId, decimal newAmmount)
        {
            Account account = await _dbContext.Accounts.FindAsync(accountId);

            if (account is null)
            {
                throw new InvalidOperationException($"Счет {accountId} пользователя не найден.");
            }

            account = await UpdateAccountAsync(account);

            return account;
        }

        public Task<Account> UpdateAccountAsync(Account accountForUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

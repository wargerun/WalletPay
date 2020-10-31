
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Wallet> GetWalletByUserIdAsync(int userId) => Task.FromResult(_wallets.SingleOrDefault(w => w.UserId == userId));

        public Task<Wallet> GetWalletByIdAsync(int walletId) => Task.FromResult(_wallets.SingleOrDefault(w => w.Id == walletId));

        public async Task<Account> AddAccountAsync(int walletId, string codeCurrency, decimal amount)
        {
            Wallet wallet = await GetWalletByIdAsync(walletId);
            Account account = wallet.Accounts?.SingleOrDefault(a => a.WalletId == walletId && a.Currency == codeCurrency);

            if (account != null)
            {
                throw new InvalidOperationException($"Код валюты должен быть уникален в одном кошельке");
            }

            return new Account()
            {
                Amount = amount,
                Currency = codeCurrency,
                WalletId = walletId,
                Wallet = wallet,
            };
        }

        public async Task<Account> UpdateAccountAsync(int walletId, int accountId, decimal newAmmount)
        {
            Wallet wallet = await GetWalletByIdAsync(walletId);
            Account account = wallet.Accounts.Single(a => a.AccountId == accountId);
            account.Amount = newAmmount;
            return account;
        }
    }
}

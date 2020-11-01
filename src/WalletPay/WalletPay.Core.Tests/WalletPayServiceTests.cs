using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WalletPay.Data.Entities;

using Xunit;

namespace WalletPay.Core.Tests
{
    public class WalletPayServiceTests
    {
        private readonly List<Account> _accounts = new List<Account>()
        {
            new()
            {
                Id = 123,
                Currency = "RUB",
                Amount = 5,
            },
            new()
            {
                Id = 123444,
                Currency = "EUR",
                Amount = 500,
            }
        };

        private readonly List<Wallet> _wallets = new List<Wallet>()
        {
            new()
            {
                Id = 1,
                Status = WalletStatus.Active
            },
            new()
            {
                Id = 2,
                Status = WalletStatus.Active,
            },
            new()
            {
                Id = 3,
                Status = WalletStatus.Active,
            }
        };

        public WalletPayServiceTests()
        {
            SetAccountInWalletById(1, 123);
            SetAccountInWalletById(2, 123444);
        }

        private void SetAccountInWalletById(int walletId, params int[] accountIds)
        {
            Wallet wallet = _wallets.Single(w => w.Id == walletId);
            wallet.Accounts = _accounts.Where(a => accountIds.Contains(a.Id)).ToList();
        }

        [Theory]
        [InlineData(1, "RUB", 666.66, 666.66)]
        [InlineData(2, "EUR", 300.123, 300.123)]
        public async Task DepositAsyncTest(int walletId, string codeCurrency, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository);

            // then
            Account account = await walletPay.DepositAsync(new()
            {
                WalletId = walletId,
                Currency = codeCurrency,
                Amount = amount
            });

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }

        [Theory]
        [InlineData(1, 123, 666.66, 671.66)]
        [InlineData(3, 123444, 300.123, 800.123)]
        public async Task DepositAsyncByAccountIdTest(int walletId, int accountId, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository);

            // then
            Account account = await walletPay.DepositAsync(walletId, accountId, amount);

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }

        [Theory]
        [InlineData(1, 123, 4, 1)]
        [InlineData(3, 123444, 450, 50)]
        public async Task WithdrawFromAccountAsyncTest(int walletId, int accountId, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository);

            Wallet wallet = await walletRepository.GetFirstWhereAsync(w => w.Id == walletId);

            // then
            await walletPay.WithdrawFromAccountAsync(wallet.Id, accountId, amount);
            Account account = wallet.Accounts.Single(a => a.Id == accountId);

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }
    }
}

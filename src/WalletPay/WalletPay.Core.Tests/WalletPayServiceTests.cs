using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WalletPay.Core.CurrencyConversions;
using WalletPay.Core.Tests.Repositories;
using WalletPay.Data.Entities;

using Xunit;

namespace WalletPay.Core.Tests
{
    public class WalletPayServiceTests
    {
        private readonly ICurrencyConversion _mockCurrency = new MockCurrencyConversion();
        private readonly List<Account> _accounts = new List<Account>()
        {
            // WALLET ID = 1 
            new()
            {
                Id = 123,
                Currency = "RUB",
                Amount = 5,
            },
            // WALLET ID = 2 
            new()
            {
                Id = 123444,
                Currency = "EUR",
                Amount = 500,
            },
            // WALLET ID = 3 
            new()
            {
                Id = 1,
                Currency = "RUB",
                Amount = 500,
            },
            new()
            {
                Id = 2,
                Currency = "RUB",
                Amount = 500,
            },
            new()
            {
                Id = 3,
                Currency = "EUR",
                Amount = 100,
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
            setAccountInWalletById(1, 123);
            setAccountInWalletById(2, 123444);
            setAccountInWalletById(3, 1, 2, 3);
        }

        private void setAccountInWalletById(int walletId, params int[] accountIds)
        {
            Wallet wallet = _wallets.Single(w => w.Id == walletId);
            wallet.Accounts = _accounts.Where(a => accountIds.Contains(a.Id)).ToList();

            wallet.Accounts.ForEach(a => a.WalletId = walletId);
        }

        [Theory]
        [InlineData(1, "RUB", 666.66, 666.66)]
        [InlineData(2, "EUR", 300.123, 300.123)]
        public async Task DepositAsyncTest(int walletId, string codeCurrency, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository, _mockCurrency);

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
        [InlineData(2, 123444, 300.123, 800.123)]
        public async Task DepositAsyncByAccountIdTest(int walletId, int accountId, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository, _mockCurrency);

            // then
            Account account = await walletPay.DepositAsync(walletId, accountId, amount);

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }

        [Theory]
        [InlineData(1, 123, 4, 1)]
        [InlineData(2, 123444, 450, 50)]
        public async Task WithdrawFromAccountAsyncTest(int walletId, int accountId, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository, _mockCurrency);

            Wallet wallet = await walletRepository.GetFirstWhereAsync(w => w.Id == walletId);

            // then
            await walletPay.WithdrawFromAccountAsync(wallet.Id, accountId, amount);
            Account account = wallet.Accounts.Single(a => a.Id == accountId);

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }

        [Theory]      
        [InlineData(3, 1, 2, 500, 0, 1000)]       
        [InlineData(3, 1, 2, 240, 260, 740)]
        [InlineData(3, 1, 3, 1, 499, 192.7646)]
        [InlineData(3, 3, 1, 90, 10, 500.972369)]
        public async Task TransferBetweenAccountsAsyncTests(
            int walletId,
            int fromAccountId, 
            int toAccountId,
            decimal amount, 
            decimal expectedAmountFromAccount,
            decimal expectedAmountToAccount)
        {
            // given
            MockWalletRepository walletRepository = new(_wallets);
            MockAccountRepository accountRepository = new(_accounts);
            WalletPayService walletPay = new(walletRepository, accountRepository, _mockCurrency);

            // then
            await walletPay.TransferBetweenAccountsAsync(walletId, fromAccountId, toAccountId, amount);

            Account fromAccountTransfer = await accountRepository.GetFirstWhereAsync(a => a.Id == fromAccountId);
            Account newAccountTransfer = await accountRepository.GetFirstWhereAsync(a => a.Id == toAccountId);

            // when
            Assert.Equal(expectedAmountFromAccount, fromAccountTransfer.Amount);
            Assert.Equal(expectedAmountToAccount, newAccountTransfer.Amount);
        }
    }
}

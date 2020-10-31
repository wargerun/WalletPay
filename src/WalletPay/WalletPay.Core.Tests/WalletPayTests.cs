using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WalletPay.Data.Entities;

using Xunit;

namespace WalletPay.Core.Tests
{
    public class WalletPayTests
    {
        private readonly List<Wallet> wallets = new List<Wallet>()
        {
            new Wallet()
            {
                Id = 1,
                Status = WalletStatus.Active,
                Accounts = new List<Account>()
                {
                    new Account()
                    {
                        Id = 123,
                        Currency = "RUB",
                        Amount = 5,
                    }
                }
            },
            new Wallet()
            {
                Id = 2,
                Status = WalletStatus.Active,
            },
            new Wallet()
            {
                Id = 3,
                Status = WalletStatus.Active,
                Accounts = new List<Account>()
                {
                    new Account()
                    {
                        Id = 123444,
                        Currency = "EUR",
                        Amount = 500,
                    }
                }
            }
        };

        [Theory]
        [InlineData(1, "RUB", 666.66, 666.66)]
        [InlineData(2, "EUR", 300.123, 300.123)]
        public async Task DepositAsyncTest(int walletId, string codeCurrency, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new MockWalletRepository(wallets);
            WalletPay walletPay = new WalletPay(walletRepository);

            // then
            Account account = await walletPay.DepositAsync(walletId, codeCurrency, amount);

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }

        [Theory]
        [InlineData(1, 123,     666.66,     671.66)]
        [InlineData(3, 123444,  300.123,    800.123)]
        public async Task DepositAsyncByAccountIdTest(int walletId, int accountId, decimal amount, decimal expectedAmount)
        {
            // given
            MockWalletRepository walletRepository = new MockWalletRepository(wallets);
            WalletPay walletPay = new WalletPay(walletRepository);

            // then
            Account account = await walletPay.DepositAsync(walletId, accountId, amount);

            // when
            Assert.Equal(expectedAmount, account.Amount);
        }
    }
}

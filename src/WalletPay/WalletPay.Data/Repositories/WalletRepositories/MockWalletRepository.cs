
using System.Threading.Tasks;

using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public class MockWalletRepository : IWalletRepository
    {
        public Wallet GetUserWallet(int userId)
        {
            return new Wallet
            {
                UserId = userId,
                Status = WalletStatus.Active,
            };
        }

        public Task<Wallet> GetUserWalletAsync(int userId) => Task.FromResult(GetUserWallet(userId));


        public Currency Transfer(int userId, string fromCodeCurrency, string toCodeCurrency, decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public Task<Currency> TransferAsync(int userId, string fromCodeCurrency, string toCodeCurrency, decimal amount)
        {
            throw new System.NotImplementedException();
        }


        public Currency Deposit(int userId, string codeCurrency, decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public Task<Currency> DepositAsync(int userId, string codeCurrency, decimal amount)
        {
            Currency currency = new Currency()
            {
                Amount = amount,
                Code = codeCurrency,
                Wallet = new Wallet()
                {
                    UserId = userId,
                }
            };

            return Task.FromResult(currency);
        }
    }
}

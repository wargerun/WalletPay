using System;
using System.Linq;
using System.Threading.Tasks;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.WalletRepositories;

namespace WalletPay.Core
{
    public class WalletPay
    {
        private IWalletRepository _walletRepository;

        public WalletPay(IWalletRepository walletRepository)
        {
            this._walletRepository = walletRepository;
        }

        /// <summary>
        /// Асинхронная версия метода, пополнения кошелька
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="codeCurrency">код валюты</param>
        /// <param name="amount">сумма пополнения</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        public async Task<Account> DepositAsync(int walletId, string codeCurrency, decimal amount)
        {
            Wallet wallet = await _walletRepository.GetWalletByIdAsync(walletId);
            AssertWalletExist(wallet, walletId);

            Account account = await _walletRepository.AddAccountAsync(walletId, codeCurrency, amount);
            return account;
        }

        /// <summary>
        /// Асинхронная версия метода, пополнения кошелька
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="accountId">ID счета</param>
        /// <param name="amount">сумма пополнения</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        public async Task<Account> DepositAsync(int walletId, int accountId, decimal amount)
        {
            Wallet wallet = await _walletRepository.GetWalletByIdAsync(walletId);
            AssertWalletExist(wallet, walletId);

            Account account = wallet.Accounts?.SingleOrDefault(a => a.Id == accountId);

            if (account is null)
            {
                throw new InvalidOperationException($"Счет не найден");
            }

            decimal newAmmount = account.Amount + amount;
            account = await _walletRepository.UpdateAccountAsync(accountId, newAmmount);

            return account;
        }

        private static void AssertWalletExist(Wallet wallet, int walletId)
        {
            if (wallet is null)
            {
                throw new ArgumentNullException(nameof(wallet), $"Кошелек с id={walletId} не найден.");
            }
        }

        ///// <summary>
        ///// Метод, перевода из одной валюты в другую
        ///// </summary>
        ///// <param name="userId">ID пользователя</param>
        ///// <param name="fromCodeCurrency">код валюты из которой необходимо выполнить перевод</param>
        ///// <param name="toCodeCurrency">код валюты в которую необходимо выполнить перевод</param>
        ///// <param name="amount">сумма для перевода</param>
        ///// <returns>актуальна валюта после успешной транзакции</returns>
        //Account Transfer(int userId, string fromCodeCurrency, string toCodeCurrency, decimal amount);
        ///// <summary>
        ///// Асинхронная версия метода, перевода из одной валюты в другую
        ///// </summary>
        ///// <param name="userId">ID пользователя</param>
        ///// <param name="fromCodeCurrency">код валюты из которой необходимо выполнить перевод</param>
        ///// <param name="toCodeCurrency">код валюты в которую необходимо выполнить перевод</param>
        ///// <param name="amount">сумма для перевода</param>
        ///// <returns>актуальна валюта после успешной транзакции</returns>
        //Task<Account> TransferAsync(int userId, string fromCodeCurrency, string toCodeCurrency, decimal amount);

        ///// <summary>
        ///// Метод, пополнения кошелька пользователя
        ///// </summary>
        ///// <param name="userId">ID пользователя</param>
        ///// <param name="codeCurrency">код валюты</param>
        ///// <param name="amount">сумма пополнения</param>
        ///// <returns>актуальна валюта после успешной транзакции</returns>
        //Account Deposit(int userId, string codeCurrency, decimal amount);
        ///// <summary>
        ///// Асинхронная версия метода, пополнения кошелька пользователя
        ///// </summary>
        ///// <param name="userId">ID пользователя</param>
        ///// <param name="codeCurrency">код валюты</param>
        ///// <param name="amount">сумма пополнения</param>
        ///// <returns>актуальна валюта после успешной транзакции</returns>
        //Task<Account> DepositAsync(int userId, string codeCurrency, decimal amount);
    }
}

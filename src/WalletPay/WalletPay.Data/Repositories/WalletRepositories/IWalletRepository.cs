using System.Threading.Tasks;

using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public interface IWalletRepository
    {
        /// <summary>
        /// Метод, получения кошелька пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Кошелек пользователя</returns>
        Wallet GetUserWallet(int userId);
        /// <summary>
        /// Асинхронная версия метода, получения кошелька пользователя
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <returns>Кошелек пользователя</returns>
        Task<Wallet> GetUserWalletAsync(int userId);

        /// <summary>
        /// Метод, перевода из одной валюты в другую
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="fromCodeCurrency">код валюты из которой необходимо выполнить перевод</param>
        /// <param name="toCodeCurrency">код валюты в которую необходимо выполнить перевод</param>
        /// <param name="amount">сумма для перевода</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        Currency Transfer(int userId, string fromCodeCurrency, string toCodeCurrency, decimal amount);
        /// <summary>
        /// Асинхронная версия метода, перевода из одной валюты в другую
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="fromCodeCurrency">код валюты из которой необходимо выполнить перевод</param>
        /// <param name="toCodeCurrency">код валюты в которую необходимо выполнить перевод</param>
        /// <param name="amount">сумма для перевода</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        Task<Currency> TransferAsync(int userId, string fromCodeCurrency, string toCodeCurrency, decimal amount);

        /// <summary>
        /// Метод, пополнения кошелька пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="codeCurrency">код валюты</param>
        /// <param name="amount">сумма пополнения</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        Currency Deposit(int userId, string codeCurrency, decimal amount);
        /// <summary>
        /// Асинхронная версия метода, пополнения кошелька пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="codeCurrency">код валюты</param>
        /// <param name="amount">сумма пополнения</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        Task<Currency> DepositAsync(int userId, string codeCurrency, decimal amount);
    }
}

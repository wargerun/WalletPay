using System.Threading.Tasks;

using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public interface IWalletRepository
    {
        /// <summary>
        /// Асинхронная версия метода, получения кошелька пользователя
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <returns>Кошелек пользователя</returns>
        Task<Wallet> GetWalletByUserIdAsync(int userId);

        /// <summary>
        /// Асинхронная версия метода, получения кошелька
        /// </summary>
        /// <param name="walletId">ID кошелька</param>
        /// <returns>Кошелек пользователя</returns>
        Task<Wallet> GetWalletByIdAsync(int walletId);

        Task<Account> AddAccountAsync(int walletId, string codeCurrency, decimal amount);

        Task<Account> UpdateAccountAsync(int accountId, decimal newAmmount);
    }
}

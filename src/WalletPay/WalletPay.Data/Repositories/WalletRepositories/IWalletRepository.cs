using System.Threading;
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
        Task<Wallet> GetWalletByUserIdAsync(int userId, CancellationToken token = default);

        /// <summary>
        /// Асинхронная версия метода, получения кошелька
        /// </summary>
        /// <param name="walletId">ID кошелька</param>
        /// <returns>Кошелек пользователя</returns>
        Task<Wallet> GetWalletByIdAsync(int walletId, CancellationToken token = default);

        /// <summary>
        /// Асинхронная версия метода, создание нового счета в кошельке
        /// </summary>
        /// <param name="account"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Account> AddAccountAsync(Account account, CancellationToken token = default);

        /// <summary>
        /// Асинхронная версия метода, обновления суммы счета
        /// </summary>
        /// <param name="accountId">ID счет</param>
        /// <param name="newAmmount">Новая сумма счета</param>
        /// <param name="token">токен отмены</param>
        /// <returns></returns>
        Task<Account> UpdateAccountAsync(int accountId, decimal newAmmount, CancellationToken token = default);
    }
}

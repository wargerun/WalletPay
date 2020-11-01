using System;
using System.Linq;
using System.Threading.Tasks;

using WalletPay.Core.CurrencyConversions;
using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Accounts;
using WalletPay.Data.Repositories.WalletRepositories;

namespace WalletPay.Core
{
    public class WalletPayService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyConversion _currencyConversion;

        public WalletPayService(
            IWalletRepository walletRepository, 
            IAccountRepository accountRepository)
        {
            _walletRepository = walletRepository;
            _accountRepository = accountRepository;
            _currencyConversion = new XECurrencyConversion();
        }

        /// <summary>
        /// Асинхронная версия метода, создания счета в кошельке
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="codeCurrency">код валюты</param>
        /// <param name="amount">сумма пополнения</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        public async Task<Account> DepositAsync(Account inputAccount)
        {
            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.Id == inputAccount.WalletId);
            AssertWalletExist(wallet, wallet.Id);
            await _accountRepository.InsertAsync(inputAccount);
            return inputAccount;
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
            Account account = await _accountRepository.GetFirstWhereAsync(a => a.WalletId == walletId && a.Id == accountId);
            AssertAccountExist(account, accountId);
            account.Amount += amount;

            account = await _accountRepository.UpdateAsync(account);
            return account;
        }

        /// <summary>
        /// Асинхронная версия метода, списания средств с счета
        /// </summary>
        /// <param name="wallet">кошелек</param>
        /// <param name="accountId">ид счета</param>
        /// <param name="amount">сумма снятий</param>
        /// <returns></returns>
        public async Task WithdrawFromAccountAsync(Wallet wallet, int accountId, decimal amount)
        {
            // TODO WALLET ??!1
            Account account = await _accountRepository.GetFirstWhereAsync(a => a.WalletId == wallet.Id && a.Id == accountId);
            AssertAccountExist(account, accountId);
            account.Amount -= amount;

            if (account.Amount < 0)
            {
                throw new InvalidOperationException($"Insufficient funds.");
            }

            await _accountRepository.UpdateAsync(account);
        }

        /// <summary>
        /// Асинхронная версия метода, перевод средств между своими счетами 
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task TransferBetweenAccounts(Wallet wallet, int fromAccountId, int toAccountId, decimal amount)
        {
            Account fromTransferAccount = wallet.Accounts.SingleOrDefault(a => a.Id == fromAccountId);

            if (fromTransferAccount is null)
            {
                throw new InvalidOperationException("The transfer account in wallet is was not found");
            }

            Account toTransferAccount = wallet.Accounts.SingleOrDefault(a => a.Id == toAccountId);

            if (toTransferAccount is null)
            {
                throw new InvalidOperationException("The receipt account was not found in the wallet");
            }

            // TODO продолжить от сюла, транзакция =) ? 
            var oldAmount = fromTransferAccount.Amount - amount;

            if (fromTransferAccount.Currency == toTransferAccount.Currency)
            {
                //await DepositAsync(wallet.Id, )
            }
        }

        private static void AssertWalletExist(Wallet wallet, int walletId)
        {
            if (wallet is null)
            {
                throw new ArgumentNullException(nameof(wallet), $"Wallet with id {walletId} is not found.");
            }
        }

        private static void AssertAccountExist(Account account, int accountId)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), $"Account with id {accountId} is not found.");
            }
        }
    }
}

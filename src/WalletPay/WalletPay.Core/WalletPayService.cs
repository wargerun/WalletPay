using System;
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
            IAccountRepository accountRepository,
            ICurrencyConversion currencyConversion)
        {
            _walletRepository = walletRepository;
            _accountRepository = accountRepository;
            _currencyConversion = currencyConversion;
        }

        /// <summary>
        /// Асинхронная версия метода, создания счета в кошельке
        /// </summary>
        /// <param name="inputAccount">счета</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        public async Task<Account> CreateAccountInWalletAsync(Account inputAccount)
        {
            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.Id == inputAccount.WalletId);
            assertWalletExist(wallet, wallet.Id);
            await _accountRepository.InsertAsync(inputAccount);
            return inputAccount;
        }

        /// <summary>
        /// Асинхронная версия метода, пополнения кошелька
        /// </summary>
        /// <param name="walletId">ID</param>
        /// <param name="accountId">ID счета</param>
        /// <param name="amount">сумма пополнения</param>
        /// <returns>актуальна валюта после успешной транзакции</returns>
        public async Task<Account> DepositAsync(int walletId, int accountId, decimal amount)
        {
            Account account = await _accountRepository.GetFirstWhereAsync(a => a.WalletId == walletId && a.Id == accountId);
            assertAccountExist(account, accountId);
            account.Amount += amount;

            account = await _accountRepository.UpdateAsync(account);
            return account;
        }

        /// <summary>
        /// Асинхронная версия метода, списания средств с счета
        /// </summary>
        /// <param name="walletId">ид кошелек</param>
        /// <param name="accountId">ид счета</param>
        /// <param name="amount">сумма снятий</param>
        /// <returns></returns>
        public async Task WithdrawFromAccountAsync(int walletId, int accountId, decimal amount)
        {
            Account account = await _accountRepository.GetFirstWhereAsync(a => a.WalletId == walletId && a.Id == accountId);
            assertAccountExist(account, accountId);
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
        /// <param name="walletId">ид кошелек</param>
        /// <param name="fromAccountId">номер счета из которого надо произвести перевод</param>
        /// <param name="toAccountId">номер счета на который надо произвести перевод</param>
        /// <param name="amount">сумма перевода</param>
        /// <returns></returns>
        public async Task TransferBetweenAccountsAsync(int walletId, int fromAccountId, int toAccountId, decimal amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Account fromTransferAccount = await _accountRepository.GetFirstWhereAsync(a => a.WalletId == walletId && a.Id == fromAccountId);
            assertAccountExist(fromTransferAccount, fromAccountId);

            Account toTransferAccount = await _accountRepository.GetFirstWhereAsync(a => a.WalletId == walletId && a.Id == toAccountId);
            assertAccountExist(toTransferAccount, toAccountId);

            // Снятие средств
            fromTransferAccount.Amount -= amount;
            await _accountRepository.UpdateAsync(fromTransferAccount);

            // Пополнение
            if (fromTransferAccount.Currency == toTransferAccount.Currency)
            {
                toTransferAccount.Amount += amount;
            }
            else
            {
                toTransferAccount.Amount += _currencyConversion.CurrencyConvert(fromTransferAccount.Currency, toTransferAccount.Currency, amount);
            }

            await _accountRepository.UpdateAsync(toTransferAccount);
        }

        private static void assertWalletExist(Wallet wallet, int walletId)
        {
            if (wallet is null)
            {
                throw new ArgumentNullException(nameof(wallet), $"Wallet with id {walletId} is not found.");
            }
        }

        private static void assertAccountExist(Account account, int accountId)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), $"Account with id {accountId} is not found.");
            }
        }
    }
}

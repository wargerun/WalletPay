using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using WalletPay.Core;
using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Accounts;
using WalletPay.Data.Repositories.UserRepositories;
using WalletPay.Data.Repositories.WalletRepositories;
using WalletPay.WebService.Models;
using WalletPay.WebService.Models.Dto;
using WalletPay.WebService.Models.Requests;
using WalletPay.WebService.Models.Response;

namespace WalletPay.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletPayController : ControllerBase
    {
        private const string ERROR_MESSAGE_USER_NOT_FOUND = "User is not found.";
        private const string ERROR_MESSAGE_WALLET_NOT_FOUND = "Wallet of User is not found.";
        private readonly WalletPayService _walletPay;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IAccountRepository _accountRepository;

        public WalletPayController(
            IUserRepository userRepository,
            IWalletRepository walletRepository,
            IAccountRepository accountRepository,
            WalletPayService walletPayService)
        {
            _walletPay = walletPayService;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("GetWalletByUserId")]
        public async Task<ActionResult<GetUserWalletResponse>> GetWalletByUserId(int userId)
        {
            User user = await _userRepository.GetUserAsync(userId);

            if (user is null)
            {
                return NotFound(ERROR_MESSAGE_USER_NOT_FOUND);
            }

            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == user.Id);
            List<Account> accounts = null;

            if (wallet != null)
            {
                accounts = await FindAllByWhereOrderedAscendingAsync(wallet.Id);
            }

            return Ok(new GetUserWalletResponse(user, wallet, accounts));
        }

        [HttpGet("GetWallet")]
        public async Task<ActionResult<GetUserWalletResponse>> GetWallet(int walletId)
        {
            User user = await _userRepository.GetFirstWhereAsync(u => u.Wallet.Id == walletId);

            if (user is null)
            {
                return NotFound(ERROR_MESSAGE_USER_NOT_FOUND);
            }

            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.Id == walletId);
            List<Account> accounts = null;

            if (wallet != null)
            {
                accounts = await FindAllByWhereOrderedAscendingAsync(wallet.Id);
            }

            return Ok(new GetUserWalletResponse(user, wallet, accounts));
        }

        private Task<List<Account>> FindAllByWhereOrderedAscendingAsync(int walletId)
        {
            return _accountRepository.FindAllByWhereOrderedAscendingAsync(a => a.WalletId == walletId, a => a.Id);
        }

        [HttpPost("createAccountInWallet")]
        public async Task<ActionResult> CreateAccountInWallet(PostCreateAccountInWalletRequest accountInWalletRequest)
        {
            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.Id == accountInWalletRequest.WalletId);

            if (wallet is null)
            {
                return NotFound(ERROR_MESSAGE_WALLET_NOT_FOUND);
            }

            Account account = await _walletPay.CreateAccountInWalletAsync(new()
            {
                WalletId = wallet.Id,
                Amount = accountInWalletRequest.Amount,
                Name = accountInWalletRequest.AccountName,
                Currency = accountInWalletRequest.CodeCurrency,
            });

            return Created(nameof(GetWallet), new AccountDto(account));
        }

        [HttpPut("deposit")]
        public async Task<ActionResult> Deposit(PutDepositRequest depositRequest)
        {
            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == depositRequest.UserId);

            if (wallet is null)
            {
                return NotFound(ERROR_MESSAGE_WALLET_NOT_FOUND);
            }
            
            await _walletPay.DepositAsync(wallet.Id, depositRequest.AccountId, depositRequest.Amount);

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw(PostWithdrawRequest withdrawRequest)
        {
            int userId = withdrawRequest.UserId;

            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == userId);

            if (wallet is null)
            {
                return NotFound(ERROR_MESSAGE_WALLET_NOT_FOUND);
            }

            if (!await _accountRepository.AnyAsync(a => a.Id == withdrawRequest.AccountId))
            {
                return NotFound($"Account is not found.");
            }

            await _walletPay.WithdrawFromAccountAsync(wallet.Id, withdrawRequest.AccountId, withdrawRequest.Amount);

            return Ok();
        }

        [HttpPost("transferBetweenAccounts")]
        public async Task<ActionResult> TransferBetweenAccounts(PostTransferBetweenAccountsRequest betweenAccountsRequest)
        {
            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == betweenAccountsRequest.UserId);

            if (wallet is null)
            {
                return NotFound(ERROR_MESSAGE_WALLET_NOT_FOUND);
            }

            await _walletPay.TransferBetweenAccountsAsync(
                walletId: wallet.Id,
                fromAccountId: betweenAccountsRequest.TransferFromAccountId,
                toAccountId: betweenAccountsRequest.TransferToAccountId,
                amount: betweenAccountsRequest.Amount);

            return Ok();
        }
    }
}

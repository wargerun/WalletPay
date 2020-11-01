using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("GetWallet")]
        public async Task<ActionResult<GetUserWalletResponse>> GetWallet(int userId)
        {
            if (userId == 0)
            {
                return BadRequest(Errors.InvalidUserId);
            }

            User user = await _userRepository.GetUserAsync(userId);

            if (user is null)
            {
                return NotFound("User is not found.");
            }

            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == user.Id);
            List<Account> accounts = null;

            if (wallet != null)
            {
                accounts = await _accountRepository.FindAllByWhereOrderedAscendingAsync(a => a.WalletId == wallet.Id, a => a.Id);
            }

            return Ok(new GetUserWalletResponse(user, wallet, accounts));
        }

        [HttpPut("deposit")]
        public async Task<ActionResult> Deposit(PutDepositRequest depositRequest)
        {
            int userId = depositRequest.UserId;

            if (userId == 0)
            {
                return BadRequest(Errors.InvalidUserId);
            }

            if (depositRequest.Amount < 0)
            {
                return BadRequest(Errors.InvalidAmount);
            }

            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == userId);

            Account account = new()
            {
                WalletId = wallet.Id,
                Amount = depositRequest.Amount,
                Name = depositRequest.AccountName,
                Currency = depositRequest.CodeCurrency,
            };

            if (depositRequest.AccountId.HasValue)
            {
                account.Id = depositRequest.AccountId.Value;
                await _walletPay.DepositAsync(account.WalletId, account.Id, account.Amount);
            }
            else
            {
                await _walletPay.DepositAsync(account);
            }

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw(PostWithdrawRequest withdrawRequest)
        {
            int userId = withdrawRequest.UserId;

            if (userId == 0)
            {
                return BadRequest(Errors.InvalidUserId);
            }

            if (withdrawRequest.Amount < 0)
            {
                return BadRequest(Errors.InvalidAmount);
            }

            if (withdrawRequest.AccountId <= 0)
            {
                return BadRequest(Errors.InvalidAccountId);
            }

            Wallet wallet = await _walletRepository.GetFirstWhereAsync(w => w.UserId == userId);

            if (!wallet.Accounts.Any(a => a.Id == withdrawRequest.AccountId))
            {
                return NotFound($"AccountId is not found.");
            }

            await _walletPay.WithdrawFromAccountAsync(wallet, withdrawRequest.AccountId, withdrawRequest.Amount);

            return Ok();
        }
    }
}

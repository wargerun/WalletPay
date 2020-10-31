using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

using WalletPay.Core;
using WalletPay.Data.Entities;
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
        private readonly Core.WalletPay _walletPay;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletPayController(
            IUserRepository userRepository,
            IWalletRepository walletRepository)
        {
            _walletPay = new Core.WalletPay(walletRepository);
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        [HttpGet("GetWallet")]
        public async Task<ActionResult<GetUserWalletResponse>> GetWallet(int userId)
        {
            if (userId == 0)
            {
                return BadRequest(Errors.InvalidUserId);
            }

            User user = _userRepository.GetUser(userId);

            if (user is null)
            {
                return NotFound("User is not found");
            }

            Wallet wallet = await _walletRepository.GetWalletByUserIdAsync(user.Id);

            return Ok(new GetUserWalletResponse(user, wallet));
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit(PostDepositRequest depositRequest)
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

            Wallet wallet = await _walletRepository.GetWalletByUserIdAsync(userId);

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
    }
}

using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

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
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletPayController(
            IUserRepository userRepository,
            IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        [HttpGet("GetWallet")]
        public ActionResult<GetUserWalletResponse> GetWallet(GetUserWalletRequest getUserWalletRequest)
        {
            int userId = getUserWalletRequest.UserId;

            if (userId == 0)
            {
                return BadRequest(Errors.InvalidUserId);
            }

            User user = _userRepository.GetUser(userId);
            Wallet wallet = _walletRepository.GetUserWallet(user.Id);

            return Ok(new GetUserWalletResponse
            {
                Wallet = new WalletDto(wallet),
                User = new UserDto(user),
            });
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

            await _walletRepository.DepositAsync(userId, depositRequest.CodeCurrency, depositRequest.Amount);

            return Ok();
        }
    }
}

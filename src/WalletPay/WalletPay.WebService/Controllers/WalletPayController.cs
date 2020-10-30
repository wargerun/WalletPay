using Microsoft.AspNetCore.Mvc;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.UserRepositories;
using WalletPay.Data.Repositories.WalletRepositories;
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
            User user = _userRepository.GetUser(getUserWalletRequest.UserId);
            Wallet wallet = _walletRepository.GetUserWallet(user.Id);

            return Ok(new GetUserWalletResponse
            {
                Wallet = new WalletDto(wallet),
                User = new UserDto(user),
            });
        }
    }
}

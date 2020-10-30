
using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public class MockWalletRepository : IWalletRepository
    {
        public Wallet GetUserWallet(int userId)
        {
            return new Wallet
            {
                UserId = userId,
                Status = WalletStatus.Active,
            };
        }
    }
}

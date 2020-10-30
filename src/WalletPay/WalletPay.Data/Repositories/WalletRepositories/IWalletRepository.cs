using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.WalletRepositories
{
    public interface IWalletRepository
    {
        Wallet GetUserWallet(int userId);
    }
}

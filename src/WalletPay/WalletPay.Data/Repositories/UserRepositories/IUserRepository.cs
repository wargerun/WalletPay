using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        User GetUser(int userId);
    }
}

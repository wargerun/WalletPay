using System.Threading.Tasks;

using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int userId);
        User GetUser(int userId);
    }
}

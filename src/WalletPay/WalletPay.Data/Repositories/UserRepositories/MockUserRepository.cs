
using System.Threading.Tasks;

using WalletPay.Data.Entities;

namespace WalletPay.Data.Repositories.UserRepositories
{
    public class MockUserRepository : IUserRepository
    {
        public User GetUser(int userId)
        {
            return new User()
            {
                Id = userId,
                Name = $"Пользователь с id={userId}"
            };
        }

        public Task<User> GetUserAsync(int userId)
        {
            return Task.FromResult(GetUser(userId));
        }
    }
}


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
    }
}

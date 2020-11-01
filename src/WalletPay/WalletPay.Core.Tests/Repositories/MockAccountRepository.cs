using System.Collections.Generic;
using System.Threading.Tasks;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Accounts;

namespace WalletPay.Core.Tests.Repositories
{
    public class MockAccountRepository : CollectionRepositoryBase<Account>, IAccountRepository
    {
        public MockAccountRepository(IEnumerable<Account> _accounts)
            : base(_accounts)
        {
        }

        public override async Task<Account> UpdateAsync(Account entityToUpdate)
        {
            Account account = await GetFirstWhereAsync(a => a.Id == entityToUpdate.Id);

            if (account != null)
            {
                account.Amount = entityToUpdate.Amount;
            }

            return account;
        }
    }
}

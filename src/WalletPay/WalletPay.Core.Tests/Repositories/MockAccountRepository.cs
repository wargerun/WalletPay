using System.Collections.Generic;
using System.Threading.Tasks;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Accounts;
using WalletPay.Data.Repositories.Transactions;

namespace WalletPay.Core.Tests.Repositories
{
    public class MockAccountRepository : CollectionRepositoryBase<Account>, IAccountRepository
    {
        public MockAccountRepository(IEnumerable<Account> accounts)
            : base(accounts)
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

        public IDatabaseTransaction BeginTransaction() => new MockTransaction();
    }
}

using System.Collections.Generic;

using WalletPay.Data.Entities;
using WalletPay.Data.Repositories.Accounts;

namespace WalletPay.Core.Tests
{
    public class MockAccountRepository : CollectionRepositoryBase<Account>, IAccountRepository
    {
        public MockAccountRepository(IEnumerable<Account> _accounts)
            : base(_accounts)
        {
        }
    }
}

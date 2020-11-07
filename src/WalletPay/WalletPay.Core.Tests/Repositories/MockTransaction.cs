using System.Threading.Tasks;

using WalletPay.Data.Repositories.Transactions;

namespace WalletPay.Core.Tests.Repositories
{
    internal class MockTransaction : IDatabaseTransaction
    {
        public void Dispose() { }

        public Task CommitAsync() => Task.CompletedTask;

        public Task RollbackAsync() => Task.CompletedTask;
    }
}

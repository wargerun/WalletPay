using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace WalletPay.Data.Repositories.Transactions
{
    public class WalletPayTransaction : IDatabaseTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public WalletPayTransaction(IDbContextTransaction transaction) => _transaction = transaction;

        public Task CommitAsync() => _transaction.CommitAsync();

        public Task RollbackAsync() => _transaction.RollbackAsync();

        public void Dispose() => _transaction.Dispose();
    }
}

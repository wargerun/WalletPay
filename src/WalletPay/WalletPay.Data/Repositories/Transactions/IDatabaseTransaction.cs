using System;
using System.Threading.Tasks;

namespace WalletPay.Data.Repositories.Transactions
{
    public interface IDatabaseTransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}

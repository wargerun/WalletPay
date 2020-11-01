using Microsoft.EntityFrameworkCore;

using WalletPay.Data.Entities;

namespace WalletPay.Data.Context
{
    public class WalletPayDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public WalletPayDbContext()
        { 
            // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=../../../db/WalletPayDb.sqlite");
    }
}

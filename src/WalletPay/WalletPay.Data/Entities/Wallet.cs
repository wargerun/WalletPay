using System;
using System.Collections.Generic;

namespace WalletPay.Data.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime CurrenciesUpdated { get; set; }

        public WalletStatus Status { get; set; }

        public virtual List<Currency> Currencies { get; set; }
        public virtual User User { get; set; }
    }
}
                        
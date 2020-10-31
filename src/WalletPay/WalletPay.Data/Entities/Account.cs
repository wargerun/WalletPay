﻿namespace WalletPay.Data.Entities
{
    public class Account
    {
        public int WalletId { get; set; }

        public int AccountId { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }  

        public virtual Wallet Wallet { get; set; }
    }
}
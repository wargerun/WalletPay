using System;
using System.Collections.Generic;

namespace WalletPay.Data.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// TODO SQlite does not have a specific datetime type.<seealso cref="https://stackoverflow.com/questions/17227110/how-do-datetime-values-work-in-sqlite"/>>
        /// НО, EF мапит как боженька в TEXT и обратно
        /// </summary>
        public DateTime AccountUpdated { get; set; }

        public WalletStatus Status { get; set; }

        public virtual List<Account> Accounts { get; set; }
        public virtual User User { get; set; }
    }
}
                        
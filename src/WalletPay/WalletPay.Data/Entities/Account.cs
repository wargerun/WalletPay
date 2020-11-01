using System.ComponentModel.DataAnnotations;

namespace WalletPay.Data.Entities
{
    public class Account
    {
        private string currency;

        /// <summary>
        /// Идетификатор счета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор кошелька
        /// </summary>
        public int WalletId { get; set; }

        /// <summary>
        /// Название счета
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Валюта
        /// e.g. RUB or EUR
        /// </summary>
        [Required]
        public string Currency
        {
            get => currency;
            set
            {
                if (value != null)
                {
                    currency = value.ToUpperInvariant();
                }
            }
        }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Amount { get; set; }

        public virtual Wallet Wallet { get; set; }
    }
}

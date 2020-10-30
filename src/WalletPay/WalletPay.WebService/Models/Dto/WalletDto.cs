
using System.Text.Json.Serialization;

using WalletPay.Data.Entities;

namespace WalletPay.WebService.Models.Dto
{
    public class WalletDto
    {
        [JsonPropertyName("user-id")]
        public int UserId { get; }

        [JsonPropertyName("status")]
        public string Status { get; }

        public WalletDto(Wallet wallet)
        {
            UserId = wallet.UserId;
            Status = wallet.Status.ToString();
        }
    }
}

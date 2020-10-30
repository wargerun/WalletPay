using System.Text.Json.Serialization;

using WalletPay.WebService.Models.Dto;

namespace WalletPay.WebService.Models.Response
{
    public class GetUserWalletResponse
    {
        [JsonPropertyName("user")]
        public UserDto User { get; set; }

        [JsonPropertyName("wallet")]
        public WalletDto Wallet { get; set; }
    }
}

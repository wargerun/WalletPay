﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

using WalletPay.Data.Entities;
using WalletPay.WebService.Models.Dto;

namespace WalletPay.WebService.Models.Response
{
    public class GetUserWalletResponse
    {
        [JsonPropertyName("user")]
        public UserDto User { get; set; }

        [JsonPropertyName("wallet")]
        public WalletDto Wallet { get; set; }

        public GetUserWalletResponse(User user, Wallet wallet, List<Account> accounts)
        {
            if (user != null)
            {
                User = new UserDto(user);
            }

            if (wallet != null)
            {
                Wallet = new WalletDto(wallet, accounts);
            }
        }
    }
}

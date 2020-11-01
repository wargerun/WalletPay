# Тестовое задание Indeed-Id

    TargetFramework:  .NET Core 3.1
    LangVersion:      C# 8.0
    ProjectDependeces {
      "WalletPay.Core.Tests" : {
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.5.0" />
        <PackageReference Include="xunit" Version="2.4.0" />
        <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
        <PackageReference Include="coverlet.collector" Version="1.2.0" />
      },
      "WalletPay.WebService" : {
        <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="3.1.9" />
      },
      "WalletPay.Data" : {
        <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="3.1.9" />
      }
    }

### WalletPay - Сервис кошелька пользователя

- Приложение имеет стандартную трёхуровневую архитектуру. Вся бизнес-логика сконцентрирована в [WalletPayService.](https://github.com/wargerun/WalletPay/blob/b26ff7f7b68d799a7e1373caa9ae3eb084e574f0/src/WalletPay/WalletPay.Core/WalletPayService.cs#L11). Контроллер проксируют запросы в сервис.
- Для хранения данных использовал базу данных SQLite
- Обмен валюты происходит через интерфейс [ICurrencyConversion](https://github.com/wargerun/WalletPay/blob/b26ff7f7b68d799a7e1373caa9ae3eb084e574f0/src/WalletPay/WalletPay.Core/CurrencyConversions/ICurrencyConversion.cs#L3), в данный момент реализованно следующее Api [XECurrencyConversion](https://github.com/wargerun/WalletPay/blob/b26ff7f7b68d799a7e1373caa9ae3eb084e574f0/src/WalletPay/WalletPay.Core/CurrencyConversions/XECurrencyConversion.cs) (Не для коммерческого использования:) )

Проекты:

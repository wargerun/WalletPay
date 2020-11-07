# WalletPay - Сервис кошелька пользователя

- Приложение имеет стандартную трёхуровневую архитектуру. Вся бизнес-логика сконцентрирована в [WalletPayService](https://github.com/wargerun/WalletPay/blob/main/src/WalletPay/WalletPay.Core/WalletPayService.cs#L11). Контроллер проксируют запросы в сервис.
- Для хранения данных использовал базу данных SQLite
- Обмен валюты происходит через интерфейс [ICurrencyConversion](https://github.com/wargerun/WalletPay/blob/main/src/WalletPay/WalletPay.Core/CurrencyConversions/ICurrencyConversion.cs#L3)

# Web Api - Методы

## GetWalletByUserId
> Получает кошелек пользователя по идентификатору пользователя

**[GET]** - /WalletPay/GetWalletByUserId  

Example /WalletPay/GetWalletByUserId?userId=1

### Response

Status Code: 200

```json
{
    "user": {
        "id": 1,
        "name": "First User For test"
    },
    "wallet": {
        "currencies-updated": "2020-01-01T00:00:00",
        "wallet-id": 1,
        "status": "Active",
        "accounts": [
            {
                "account-id": 1,
                "name": "Master schet",
                "currency": "RUB",
                "amount": 199.9846
            },
            {
                "account-id": 2,
                "name": "EUR SUPER счет",
                "currency": "EUR",
                "amount": 5199.75
            }
        ]
    }
}
```

## GetWallet
> Получает кошелек пользователя по его идентификатору

**[GET]** - /WalletPay/GetWallet  

Example /WalletPay/GetWallet?walletId=1

### Response

Status Code: 200

```json
{
    "user": {
        "id": 1,
        "name": "First User For test"
    },
    "wallet": {
        "currencies-updated": "2020-01-01T00:00:00",
        "wallet-id": 1,
        "status": "Active",
        "accounts": [
            {
                "account-id": 3,
                "name": "GRP счет",
                "currency": "GRP",
                "amount": 0.332
            },
            {
                "account-id": 4,
                "name": "GRP счет",
                "currency": "GRP",
                "amount": 1533.0
            }
        ]
    }
}
```

## createAccountInWallet
> Метод создания счета.
 
**[POST]** - /WalletPay/createAccountInWallet

JSON Body Parameters

| Name | Type | Description | Required? |
| --- | --- | --- | --- |
| WalletId | int | Идентификатор кошелька | да |
| AccountName | string | названия счета | Да |
| CodeCurrency | string | валюта хранения средств | Да |
| Amount | decimal | Сумма пополнения | Да |

### Request

```json
{
    "WalletId" : 1,
    "CodeCurrency" : "GRP",
    "AccountName": "GRP счет",
    "Amount" : 100
}
```

### Response 
Status Code: 201

```json
{
    "account-id": 5,
    "name": "GRP счет",
    "currency": "GRP",
    "amount": 100
}
```


## deposit
> Метод пополнения счета.

**[PUT]** - /WalletPay/Deposit

JSON Body Parameters

| Name | Type | Description | Required? |
| --- | --- | --- | --- |
| UserId | int | Идентификатор пользователя | да |
| AccountId | int | Идентификатор счета | Да |
| Amount | decimal | Сумма пополнения | Да |

### Request 

Status Code: 200

```json
{
    "UserId" : 1,
    "CodeCurrency" : "GBP",
    "AccountName": "GBP счет",
    "Amount" : 100
}
```

## withdraw
> Снятие средств со счета.

**[POST]** - /walletPay/withdraw

JSON Body Parameters

| Name | Type | Description | Required? |
| --- | --- | --- | --- |
| UserId | int | Идентификатор пользователя | да |
| AccountId | int | Идентификатор счета | Да |
| Amount | decimal | Сумма пополнения | Да |

### Request

```json
{
    "UserId" : 1,
    "AccountId" : 1,
    "Amount" : 666.666
}
```

### Response 
Status Code: 200

## transferBetweenAccounts
> Метод перевода средств между своими счетами

**[POST]** - /walletPay/transferBetweenAccounts

JSON Body Parameters

| Name | Type | Description | Required? |
| --- | --- | --- | --- |
| UserId | int | Идентификатор пользователя | да |
| TransferFromAccountId | int | номер счета из которого надо произвести перевод | Да |
| TransferToAccountId | int | номер счета на который надо произвести перевод | Да |
| Amount | decimal | Сумма перевода | Да |

### Request
{
    "UserId" : 1,
    "TransferFromAccountId" : 4,
    "TransferToAccountId" : 3,
    "Amount" : 7665
}

### Response 
Status Code: 200

# База данных 
Выбор пал на SQLite исключительно из - за его простоты.
- /db/init.sql - скрипт добавления пользователя и кошелька
- /db/WalletPayDb.sqlite - бд

Отношения Wallets, Accounts и Users:

![WalletPayDb.sqlite](https://github.com/wargerun/WalletPay/blob/main/db/ERDiagram.WalletPay.png)


# Проекты:
1. WalletPay.Data - Содержатся репозитории каждой из таблицы, сущности базы данных, подробнее
    * /WalletPay/src/WalletPay/WalletPay.Data/ClassDiagram.cd
    * /WalletPay/src/WalletPay/WalletPay.Data/WalletPay.Data diagrams.png
2. WalletPay.Core - Бизнес логика
3. WalletPay.Core.Tests - Тесты
4. WalletPay.WebService - Контроллеры, запросы, DTO сущности - все тут

![Test Image 3](https://github.com/wargerun/WalletPay/blob/main/src/WalletPay/Projects%20Dependencies%20Diagrams.png)

### Зависимости сервиса
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
    


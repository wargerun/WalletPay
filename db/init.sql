-- ======== USERS ========

insert into users (Name)
values('First User For test');

SELECT *
FROM USERS;

-- ======== Wallets ========

insert into Wallets (UserId, AccountUpdated, Status)
values(1, '2020-01-01', 0);

SELECT *
FROM Wallets;
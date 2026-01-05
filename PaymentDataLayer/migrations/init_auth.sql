-- init_auth.sql
-- Migration script to create Roles, Users, Categories, Currencies, Payments tables
-- Run this on your SQL Server instance referenced by the project's connection string.

SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- Roles
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Roles] (
        [RoleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Name] NVARCHAR(50) NOT NULL UNIQUE
    );
END

-- Users
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Username] NVARCHAR(100) NOT NULL UNIQUE,
        [Email] NVARCHAR(256) NULL,
        [PasswordHash] VARBINARY(MAX) NOT NULL,
        [PasswordSalt] VARBINARY(MAX) NOT NULL,
        [RoleId] INT NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(RoleId)
    );
END

-- Categories
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Categories] (
        [CategoryId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Name] NVARCHAR(100) NOT NULL UNIQUE,
        [Description] NVARCHAR(500) NULL
    );
END

-- Currencies
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Currencies] (
        [CurrencyId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Code] NVARCHAR(10) NOT NULL UNIQUE,
        [Name] NVARCHAR(100) NOT NULL
    );
END

-- Payments
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payments]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Payments] (
        [PaymentId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Amount] DECIMAL(18,2) NOT NULL,
        [CurrencyId] INT NOT NULL,
        [CategoryId] INT NULL,
        [UserId] INT NOT NULL,
        [PaymentDate] DATETIME NOT NULL,
        [Notes] NVARCHAR(1000) NULL,
        CONSTRAINT FK_Payments_Currencies FOREIGN KEY (CurrencyId) REFERENCES dbo.Currencies(CurrencyId),
        CONSTRAINT FK_Payments_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(CategoryId),
        CONSTRAINT FK_Payments_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
    );
END

-- Seed Roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Admin')
    INSERT INTO dbo.Roles (Name) VALUES ('Admin');
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'User')
    INSERT INTO dbo.Roles (Name) VALUES ('User');

-- Seed example categories
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Tuition')
    INSERT INTO dbo.Categories (Name, Description) VALUES ('Tuition', 'Tuition payments');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Books')
    INSERT INTO dbo.Categories (Name, Description) VALUES ('Books', 'Books and supplies');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Accommodation')
    INSERT INTO dbo.Categories (Name, Description) VALUES ('Accommodation', 'Housing related payments');

-- Seed example currencies
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Code = 'USD')
    INSERT INTO dbo.Currencies (Code, Name) VALUES ('USD','US Dollar');
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Code = 'EUR')
    INSERT INTO dbo.Currencies (Code, Name) VALUES ('EUR','Euro');

COMMIT TRANSACTION;

/*
How to seed an initial admin user:

- Prefer creating the first admin from the application Signup/Seed routine so the password hashing/salting uses the same code.
- If you want to insert an admin directly via SQL, produce a password hash and salt (binary) with the same PBKDF2 parameters your app will use, then insert them as hex (0x...) or with parameterized queries.

Example C# helper to generate hash+salt (use in a small console app or immediate window):

// Add this snippet into a temporary console project and run to get hex values to paste into SQL.
using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        string password = "Admin123"; // choose a secure initial password then change it
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            using (var derive = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = derive.GetBytes(32);
                Console.WriteLine("SaltHex=" + BitConverter.ToString(salt).Replace("-",""));
                Console.WriteLine("HashHex=" + BitConverter.ToString(hash).Replace("-",""));
            }
        }
    }
}

-- After you obtain hex values, insert into Users like:
-- INSERT INTO dbo.Users (Username, Email, PasswordHash, PasswordSalt, RoleId)
-- VALUES ('admin', 'admin@example.com', 0x<hashhex>, 0x<salthex>, (SELECT RoleId FROM dbo.Roles WHERE Name='Admin'));

Note: The project will enforce password policies (minimum length) at the application layer; the database stores only the salted hash and salt.
*/

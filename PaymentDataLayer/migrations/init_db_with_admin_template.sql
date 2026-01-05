-- NOTE: This file was cleaned to remove duplicated content that previously caused syntax errors.
-- Creates DBPayments database (if missing), tables, seed roles/categories/currencies
-- Also provides a template to insert an initial admin user using sqlcmd variables HASH and SALT.

-- Define sqlcmd variables with empty defaults so running without -v does not error.
:setvar HASH ""
:setvar SALT ""

SET XACT_ABORT ON;

-- Create database if it doesn't exist
IF DB_ID('DBPayments') IS NULL
BEGIN
    PRINT 'Creating database DBPayments...';
    EXEC('CREATE DATABASE [DBPayments]');
END

USE [DBPayments];
GO

BEGIN TRANSACTION;

-- Roles
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Roles] (
        [RoleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Name] NVARCHAR(50) NOT NULL UNIQUE
    );
END

-- Users (note: column name is UserId to match application queries)
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

-- Categories (match your existing schema: ID, Name nvarchar(100), IsActive nullable)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Categories] (
        [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Name] NVARCHAR(100) NOT NULL,
        [IsActive] BIT NULL
    );
END

-- Currencies (match your Code length 6)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Currencies] (
        [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Code] NVARCHAR(6) NOT NULL,
        [Name] NVARCHAR(100) NOT NULL,
        [Symbol] NVARCHAR(10) NOT NULL,
        [IsActive] BIT NULL
    );
END

-- Transactions (match your types: TransactionDate as DATE, Notes nvarchar(max), CreatedAt datetime nullable)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Transactions] (
        [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Amount] DECIMAL(18,2) NOT NULL,
        [TransactionDate] DATE NOT NULL,
        [CategoryID] INT NOT NULL,
        [CurrencyID] INT NOT NULL,
        [Notes] NVARCHAR(MAX) NULL,
        [CreatedAt] DATETIME NULL,
        CONSTRAINT FK_Transactions_Categories FOREIGN KEY (CategoryID) REFERENCES dbo.Categories(ID),
        CONSTRAINT FK_Transactions_Currencies FOREIGN KEY (CurrencyID) REFERENCES dbo.Currencies(ID)
    );
END

-- Seed Roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Admin')
    INSERT INTO dbo.Roles (Name) VALUES ('Admin');
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'User')
    INSERT INTO dbo.Roles (Name) VALUES ('User');

-- Seed example categories
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Tuition')
    INSERT INTO dbo.Categories (Name) VALUES ('Tuition');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Books')
    INSERT INTO dbo.Categories (Name) VALUES ('Books');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Accommodation')
    INSERT INTO dbo.Categories (Name) VALUES ('Accommodation');

-- Seed example currencies
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Code = 'USD')
    INSERT INTO dbo.Currencies (Code, Name, Symbol) VALUES ('USD','US Dollar','$');
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Code = 'EUR')
    INSERT INTO dbo.Currencies (Code, Name, Symbol) VALUES ('EUR','Euro','€');

-- Optional: insert initial admin user using sqlcmd variables HASH and SALT
-- To use this, run sqlcmd with -v HASH=<hashhex> SALT=<salthex> where hexes have NO leading 0x.
-- Example:
-- sqlcmd -S . -E -d DBPayments -i init_db_with_admin_template.sql -v HASH=ABCDEF... SALT=123456...

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username = 'admin')
BEGIN
    -- If variables not provided they will be empty strings (default set above)
    IF '$(HASH)' = '' OR '$(SALT)' = ''
    BEGIN
        PRINT 'Skipping admin insert: provide HASH and SALT variables when running sqlcmd to insert an admin.';
    END
    ELSE
    BEGIN
        INSERT INTO dbo.Users (Username, Email, PasswordHash, PasswordSalt, RoleId, CreatedAt)
        VALUES (
            'admin',
            'admin@example.com',
            0x$(HASH),
            0x$(SALT),
            (SELECT RoleId FROM dbo.Roles WHERE Name = 'Admin'),
            GETDATE()
        );
        PRINT 'Inserted admin user (username=admin). Please change the password after first login.';
    END
END

COMMIT TRANSACTION;

PRINT 'Migration complete.';
-- init_db_with_admin_template.sql
-- Creates DBPayments database (if missing), tables, seed roles/categories/currencies
-- Also provides a template to insert an initial admin user using sqlcmd variables HASH and SALT.

-- Usage (cmd.exe):
-- 1) Generate password hash and salt using the helper. Get HASHHEX and SALTHEX (hex without 0x prefix).
-- 2) Run:
--    sqlcmd -S . -E -i "d:\\Documents\\Study\\C#\\PaymentManagementSystem\\PaymentDataLayer\\migrations\\init_db_with_admin_template.sql" -v HASH=HASHHEX SALT=SALTHEX

SET XACT_ABORT ON;

-- Create database if it doesn't exist
IF DB_ID('DBPayments') IS NULL
BEGIN
    PRINT 'Creating database DBPayments...';
    EXEC('CREATE DATABASE [DBPayments]');
END

USE [DBPayments];
GO

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
        -- Roles
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
        BEGIN
            CREATE TABLE [dbo].[Roles] (
                [RoleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                [Name] NVARCHAR(50) NOT NULL UNIQUE
            );
        END

        -- Users (note: column name is UserId to match application queries)
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

        -- Categories (match your existing schema: ID, Name nvarchar(100), IsActive nullable)
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
        BEGIN
            CREATE TABLE [dbo].[Categories] (
                [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                [Name] NVARCHAR(100) NOT NULL,
                [IsActive] BIT NULL
            );
        END

        -- Currencies (match your Code length 6)
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies]') AND type in (N'U'))
        BEGIN
            CREATE TABLE [dbo].[Currencies] (
                [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                [Code] NVARCHAR(6) NOT NULL,
                [Name] NVARCHAR(100) NOT NULL,
                [Symbol] NVARCHAR(10) NOT NULL,
                [IsActive] BIT NULL
            );
        END

        -- Transactions (match your types: TransactionDate as DATE, Notes nvarchar(max), CreatedAt datetime nullable)
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
        BEGIN
            CREATE TABLE [dbo].[Transactions] (
                [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                [Amount] DECIMAL(18,2) NOT NULL,
                [TransactionDate] DATE NOT NULL,
                [CategoryID] INT NOT NULL,
                [CurrencyID] INT NOT NULL,
                [Notes] NVARCHAR(MAX) NULL,
                [CreatedAt] DATETIME NULL,
                CONSTRAINT FK_Transactions_Categories FOREIGN KEY (CategoryID) REFERENCES dbo.Categories(ID),
                CONSTRAINT FK_Transactions_Currencies FOREIGN KEY (CurrencyID) REFERENCES dbo.Currencies(ID)
            );
        END
-- Seed example currencies
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Code = 'USD')
    INSERT INTO dbo.Currencies (Code, Name, Symbol) VALUES ('USD','US Dollar','$');
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Code = 'EUR')
    INSERT INTO dbo.Currencies (Code, Name, Symbol) VALUES ('EUR','Euro','€');

-- Optional: insert initial admin user using sqlcmd variables HASH and SALT
-- To use this, run sqlcmd with -v HASH=<hashhex> SALT=<salthex> where hexes have NO leading 0x.
-- Example:
-- sqlcmd -S . -E -d DBPayments -i init_db_with_admin_template.sql -v HASH=ABCDEF... SALT=123456...

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username = 'admin')
BEGIN
    -- Ensure variables are provided
    IF '$(HASH)' = '' OR '$(SALT)' = ''
    BEGIN
        PRINT 'Skipping admin insert: provide HASH and SALT variables when running sqlcmd to insert an admin.';
    END
    ELSE
    BEGIN
        INSERT INTO dbo.Users (Username, Email, PasswordHash, PasswordSalt, RoleId, CreatedAt)
        VALUES (
            'admin',
            'admin@example.com',
            0x$(HASH),
            0x$(SALT),
            (SELECT RoleId FROM dbo.Roles WHERE Name = 'Admin'),
            GETDATE()
        );
        PRINT 'Inserted admin user (username=admin). Please change the password after first login.';
    END
END

COMMIT TRANSACTION;

PRINT 'Migration complete.';

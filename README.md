# ProductManagementSystem

# Product Management System Setup

This guide will help you get the **Product Management System** up and running, both for the backend and frontend applications.

## Backend Setup (ASP.NET Core)

1. Open the application solution `ProductManagementSystem.sln` in Visual Studio or your preferred IDE.
2. In the `appsettings.json` file, update the local server connection string to match your environment.

    Example:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ProductManagementSystem.Dev;Trusted_Connection=True;"
      }
    }
    ```
3. Open the **Package Manager Console** in Visual Studio (or use the terminal for .NET Core) and run the following command to apply migrations and set up the database:
    ```bash
    Update-Database
    ```
4. Run the backend application by pressing **F5** in Visual Studio or using the following .NET Core CLI command:
    ```bash
    dotnet run
    ```

## Frontend Setup (Angular)

1. Navigate to the frontend project folder and open the terminal (or command prompt).
2. Run the following command to install the necessary dependencies:
    ```bash
    npm install
    ```
3. After the installation completes, start the frontend application with the following command:
    ```bash
    ng serve
    ```
    The Angular app will be available at `http://localhost:4200`.

## Database Script

Below is the SQL script to create the necessary database and tables for the application.

```sql
-- Create the database
CREATE DATABASE [ProductManagementSystem.Dev]
CONTAINMENT = NONE
ON PRIMARY 
( NAME = N'ProductManagementSystem.Dev', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProductManagementSystem.Dev.mdf', SIZE = 8192KB, MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
LOG ON 
( NAME = N'ProductManagementSystem.Dev_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProductManagementSystem.Dev_log.ldf', SIZE = 8192KB, MAXSIZE = 2048GB, FILEGROWTH = 65536KB )
WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF;
GO

-- Create the EFMigrationsHistory table
CREATE TABLE [dbo].[__EFMigrationsHistory](
    on] [nvarchar](K___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
    (
        [MigrationId] ASC
    ) ON [PRIMARY];
GO

-- Create the Products table
CREATE TABLE [dbo].[Products](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max) NOT NULL,
    [Description] [nvarchar](max) NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
      NOT NULL,
    CONSTRAINT [TERED 
    (
        [Id] ASC
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

-- Insert sample data into Products table
SET IDENTITY_INSERT [dbo].[Products] ON;

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CreationDate]) 
VALUES (3, N'product 3', N'fdsaffdsafdsa', CAST(500.00 AS Decimal(18, 2)), CAST(N'2025-01-01T19:13:25.1662306' AS DateTime2));

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CreationDate]) 
VALUES (4, N'product2', N'product2', CAST(500.00 AS Decimal(18, 2)), CAST(N'2025-01-01T19:27:57.3874048' AS DateTime2));

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CreationDate]) 
VALUES (5, N'produc3', N'product3', CAST(700.00 AS Decimal(18, 2)), CAST(N'2025-01-01T19:28:05.6509832' AS DateTime2));

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CreationDate]) 
VALUES (6, N'product 4', N'fdsfdsfdsa', CAST(800.00 AS Decimal(18, 2)), CAST(N'2025-01-01T19:28:16.6685644' AS DateTime2));

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CreationDate]) 
VALUES (7, N'mohammed', N'9dfsafdsa', CAST(900.00 AS Decimal(18, 2)), CAST(N'2025-01-01T19:28:26.7941270' AS DateTime2));

INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [CreationDate]) 
VALUES (8, N'fdsafdsa', N'fdsafdsafd', CAST(432432423.00 AS Decimal(18, 2)), CAST(N'2025-01-01T21:48:40.3401166' AS DateTime2));

SET IDENTITY_INSERT [dbo].[Products] OFF;
GO

-- Finalize the database setup
USE [master];
GO
ALTER DATABASE [ProductManagementSystem.Dev] SET READ_WRITE;
GO

CREATE DATABASE store_Management;
GO

USE store_Management;
GO

-- ================= USERS =================

CREATE TABLE Users
(
UserId INT PRIMARY KEY IDENTITY(1,1),


Username NVARCHAR(50) NOT NULL UNIQUE,

PasswordHash NVARCHAR(255) NOT NULL,

FullName NVARCHAR(100) NOT NULL,

Role NVARCHAR(20) NOT NULL,

IsActive BIT DEFAULT 1,

CreatedAt DATETIME DEFAULT GETDATE()


);

-- ================= PRODUCTS =================

CREATE TABLE Products
(
ProductId INT PRIMARY KEY IDENTITY(1,1),

Name NVARCHAR(100) NOT NULL,

SKU NVARCHAR(50) NOT NULL UNIQUE,

Price DECIMAL(10,2) NOT NULL,

StockQuantity INT NOT NULL,

LowStockThreshold INT DEFAULT 5,

Category NVARCHAR(50),

IsActive BIT DEFAULT 1,

CreatedAt DATETIME DEFAULT GETDATE()


);

-- ================= TRANSACTIONS =================

CREATE TABLE Transactions
(
TransactionId INT PRIMARY KEY IDENTITY(1,1),


UserId INT NOT NULL,

TransactionDate DATETIME DEFAULT GETDATE(),

SubTotal DECIMAL(10,2) NOT NULL,

VAT DECIMAL(10,2) NOT NULL,

FinalTotal DECIMAL(10,2) NOT NULL,

PaymentMethod NVARCHAR(30),

FOREIGN KEY (UserId)
REFERENCES Users(UserId)


);

-- ================= TRANSACTION ITEMS =================

CREATE TABLE TransactionItems
(
TransactionItemId INT PRIMARY KEY IDENTITY(1,1),


TransactionId INT NOT NULL,

ProductId INT NOT NULL,

Quantity INT NOT NULL,

UnitPrice DECIMAL(10,2) NOT NULL,

LineTotal DECIMAL(10,2) NOT NULL,

FOREIGN KEY (TransactionId)
REFERENCES Transactions(TransactionId),

FOREIGN KEY (ProductId)
REFERENCES Products(ProductId)


);

-- ================= DEFAULT USERS =================

INSERT INTO Users
(Username, PasswordHash, FullName, Role)
VALUES
('admin', 'admin123', 'System Admin', 'Admin');

INSERT INTO Users
(Username, PasswordHash, FullName, Role)
VALUES
('cashier1', 'cash123', 'Cashier User', 'Cashier');

-- ================= DEFAULT PRODUCTS =================

INSERT INTO Products
(Name, SKU, Price, StockQuantity, Category)
VALUES
('Coffee', 'CF001', 50, 100, 'Drinks'),

('Tea', 'TE001', 30, 70, 'Drinks'),

('Burger', 'BG001', 120, 40, 'Food');

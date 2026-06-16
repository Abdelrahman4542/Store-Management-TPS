USE store_Management;
GO

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
(Name, SKU, Price, StockQuantity)
VALUES
('Coffee', 'CF001', 50, 100 ),

('Tea', 'TE001', 30, 70),

('Burger', 'BG001', 120, 40');
# Store Management & Transaction Processing System (SM-TPS)

## Overview

Store Management & Transaction Processing System (SM-TPS) is a desktop application built using **C#**, **WPF**, and the **MVVM Architecture Pattern**.

The system combines:

* Inventory Management
* Product Management
* Point of Sale (POS)
* Transaction Processing System (TPS)
* Sales Analytics Dashboard
* Transaction History & Auditing

The application uses **Microsoft SQL Server** as the backend database and supports role-based access for administrators and cashiers.

---

## Features

### Authentication

* Secure Login System
* Role-Based Access Control
* Administrator & Cashier Roles

### Inventory Control

* Add Products
* Update Products
* Delete Products (Soft Delete)
* Product Search
* Inventory Value Calculation
* Low Stock Monitoring
* Custom Low Stock Threshold

### Product Management

* Full CRUD Operations
* Real-Time Validation
* Stock Quantity Management
* SKU Management

### Point Of Sale (POS)

* Product Search
* Add Products To Cart
* Remove Products From Cart
* Automatic VAT Calculation
* Automatic Total Calculation
* Checkout Processing

### Transaction Processing System

* Create Sales Transactions
* Store Transaction Details
* Deduct Product Stock Automatically
* Transaction Logging

### History & Auditing

* View All Transactions
* Search Transactions
* View Transaction Details
* View Sold Items Per Transaction

### Dashboard & Analytics

* Total Products
* Low Stock Products
* Today's Sales
* Monthly Revenue
* Weekly Sales Chart
* Top Selling Products Chart
* Inventory Distribution Chart

---

## Additional Technical Features

### Asynchronous Operations

The application utilizes **async/await** to ensure a responsive user experience during:

* Product Loading
* Product CRUD Operations
* Transaction Processing
* Dashboard Analytics Loading
* Transaction History Retrieval

### Role-Based Interface

The application dynamically adjusts available features according to the logged-in user's role.

#### Administrator

* Dashboard
* Inventory Management
* Product Management
* POS Terminal
* Transaction History

#### Cashier

* Dashboard
* POS Terminal
* Transaction History

Administrative features are automatically hidden from cashier accounts.

### Low Stock Alert System

Each product contains a configurable:

* LowStockThreshold

When:

```text
StockQuantity <= LowStockThreshold
```

the product is automatically highlighted to notify users that replenishment is required.

### Data Integrity

The system maintains data integrity through:

* Input Validation
* Stock Availability Checks
* Automatic Stock Deduction
* SQL Server Transactions
* Soft Delete Mechanism

Deleted products are marked as inactive instead of being permanently removed from the database.

### Resource Dictionaries

Reusable application styles are centralized inside:

```text
Resources/Styles.xaml
```

to improve maintainability and consistency across the user interface.

---

## Technologies Used

### Frontend

* WPF (Windows Presentation Foundation)
* XAML

### Backend

* C#
* .NET
* Microsoft.Data.SqlClient

### Architecture

* MVVM (Model-View-ViewModel)

### Database

* Microsoft SQL Server

### Libraries

* LiveCharts
* Microsoft.Data.SqlClient

### Version Control

* Git
* GitHub

---

## Project Structure

```text
StoreManagementSystem
│
├── Models
├── ViewModels
│   ├── Products
│   ├── POS
│   └── Transactions
│
├── Views
├── Repositories
├── Services
├── Commands
├── Helpers
├── Resources
├── Database
│
└── StoreManagementSystem.sln
```

---

## MVVM Implementation

The application follows strict MVVM principles:

* Models contain business entities.
* ViewModels contain application logic.
* Views contain UI only.
* Commands are implemented using RelayCommand and AsyncRelayCommand.
* Data Binding is used throughout the application.
* ObservableCollection is used for dynamic collections.
* INotifyPropertyChanged is implemented for automatic UI updates.

---

## Validation

The system uses:

* IDataErrorInfo
* Property Validation
* Input Validation

Examples:

* Empty Product Name Prevention
* Empty SKU Prevention
* Negative Stock Prevention
* Negative Threshold Prevention
* Zero Price Prevention
* Out Of Stock Prevention

---

## Database

### Main Tables

#### Users

* UserId
* Username
* Password
* FullName
* Role

#### Products

* ProductId
* Name
* SKU
* Price
* StockQuantity
* LowStockThreshold
* IsActive

#### Transactions

* TransactionId
* UserId
* TransactionDate
* Status

#### TransactionItems

* TransactionItemId
* TransactionId
* ProductId
* ProductName
* Quantity
* UnitPrice

---

## Setup Instructions

### 1. Clone Repository

```bash
git clone https://github.com/Abdelrahman4542/Store-Management-TPS.git
```

### 2. Create Database

Run:

```sql
Database/StoreManagement.sql
```

### 3. Insert Sample Data

Run:

```sql
Database/SeedData.sql
```

### 4. Configure Connection String

Open:

```text
Helpers/DbConnection.cs
```

Update the SQL Server connection string according to your local environment.

### 5. Run Project

Open:

```text
StoreManagementSystem.sln
```

Then:

```text
Build → Rebuild Solution
```

Run the application.

---

## Screenshots

Add screenshots here:

* Login Window
* Dashboard
* Inventory View
* Products View
* POS Terminal
* Transaction History
* Transaction Details Window

---

## Team Members

* Abdelrahman Helmy
* Mohamed Osama
* Andrew Bassem
* Hesham Shahat

---

## Academic Project

This project was developed as part of the Store Management & Transaction Processing System (SM-TPS) course requirements using:

* C#
* WPF
* MVVM Architecture
* SQL Server
* Microsoft.Data.SqlClient
* LiveCharts

---

## License

This project was developed for educational and academic purposes only.

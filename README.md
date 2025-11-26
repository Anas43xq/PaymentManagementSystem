# 💰 Payment Management System

A comprehensive Windows Forms application for managing payment transactions, categories, and currencies with support for reporting and multi-theme UI.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue)
![C#](https://img.shields.io/badge/C%23-10.0-239120)
![License](https://img.shields.io/badge/license-MIT-green)

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#️-architecture)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [Usage](#-usage)
- [Project Structure](#-project-structure)
- [Database Schema](#️-database-schema)
- [Contributing](#-contributing)

## 🎯 Overview

**Payment Management System** is a robust desktop application built with C# and Windows Forms that streamlines payment tracking and financial management. The application follows a three-tier architecture pattern, ensuring maintainability, scalability, and separation of concerns.

### Key Highlights

- 🎨 **Light & Dark Theme Support** - Customizable UI themes for user preference
- 💾 **Multi-layered Architecture** - Separation of concerns with Data, Business, and Presentation layers
- 🔍 **Advanced Filtering & Reporting** - Comprehensive payment reports with Excel export
- 🌍 **Multi-Currency Support** - Manage payments in different currencies
- 📊 **Real-time Data Visualization** - DataGridView controls with alternating row styles
- 🔧 **Settings Management** - Category and currency CRUD operations
- 🐛 **Debug Console** - Built-in console for development troubleshooting

## ✨ Features

### Payment Management
- ✅ Add, edit, and delete payment records
- ✅ Track university-specific and general payments
- ✅ Search and filter transactions by multiple criteria
- ✅ Date range filtering for reports
- ✅ Category-based payment organization

### Settings & Configuration
- ✅ **Category Management** - Create and manage payment categories
- ✅ **Currency Management** - Add, edit, and configure currencies with symbols
- ✅ Soft-delete functionality for data integrity
- ✅ Real-time grid updates after modifications

### Reporting & Export
- ✅ Generate detailed payment reports
- ✅ Export to Excel with formatting
- ✅ Filter by date range, payment type, and category
- ✅ Visual data presentation with DataGridView

### User Interface
- ✅ Modern, clean UI design
- ✅ Light and Dark theme switching
- ✅ Responsive layout with proper anchoring
- ✅ Tab-based navigation
- ✅ Modal dialog forms for data entry
- ✅ Context menus for quick actions

## 🏗️ Architecture

The application follows a **three-tier architecture**:

```
┌─────────────────────────────────────────┐
│   Presentation Layer (PaymentManagement) │
│   - Windows Forms UI                     │
│   - Theme Management                     │
│   - User Input Validation                │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│   Business Logic Layer                   │
│   (PaymentBusinessLayer)                 │
│   - Business Rules                       │
│   - Data Validation                      │
│   - Service Methods                      │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│   Data Access Layer                      │
│   (PaymentDataLayer)                     │
│   - Database Operations                  │
│   - SQL Query Execution                  │
│   - Connection Management                │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│   Entity Layer (clsPaymentEntities)     │
│   - Data Models                          │
│   - Domain Objects                       │
└──────────────────────────────────────────┘
```

## 📦 Prerequisites

- **Visual Studio 2022** or later
- **.NET Framework 4.7.2** or higher
- **SQL Server** (LocalDB, Express, or Full)
- **Git** for version control

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Anas43xq/PaymentManagementSystem.git
cd PaymentManagementSystem
```

### 2. Configure Database Connection

Update the connection string in `App.config`:

```xml
<connectionStrings>
    <add name="PaymentDB" 
         connectionString="Server=YOUR_SERVER;Database=PaymentDB;Integrated Security=true;" 
         providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### 3. Create Database Schema

Run the SQL scripts to create the required database structure:

```sql
-- Categories Table
CREATE TABLE Categories (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    IsActive BIT DEFAULT 1
);

-- Currencies Table
CREATE TABLE Currencies (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(10) NOT NULL UNIQUE,
    Name NVARCHAR(50) NOT NULL,
    Symbol NVARCHAR(5),
    IsActive BIT DEFAULT 1
);

-- Payments Table
CREATE TABLE Payments (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Amount DECIMAL(18,2) NOT NULL,
    Date DATE NOT NULL,
    CategoryID INT FOREIGN KEY REFERENCES Categories(ID),
    CurrencyCode NVARCHAR(10),
    Description NVARCHAR(500),
    IsActive BIT DEFAULT 1
);
```

### 4. Build the Solution

Using Command Line:
```bash
.\build.cmd
```

Or using PowerShell:
```powershell
.\build.ps1
```

Or using Visual Studio:
- Open `PaymentManagementSystem.sln`
- Press `Ctrl+Shift+B` to build

### 5. Run the Application

- Press `F5` in Visual Studio, or
- Run `PaymentManagement\bin\Debug\PaymentManagement.exe`

## 💻 Usage

### Main Interface

The application features a tabbed interface with the following sections:

1. **University Payments** - Manage academic-related payments
2. **Other Payments** - Track miscellaneous transactions
3. **Reports** - Generate and export payment reports
4. **Settings** - Configure categories and currencies

### Managing Categories

1. Navigate to **Settings** → **Category Management**
2. Click **Add** to create a new category
3. Use the context menu to **Edit** or **Delete** existing categories
4. Double-click a row to quickly edit

### Managing Currencies

1. Navigate to **Settings** → **Currency Management**
2. Click **Add** to add a new currency
3. Specify the currency **Code**, **Name**, and **Symbol**
4. Use the context menu or double-click to edit entries

### Creating Payments

1. Go to the appropriate payment tab
2. Click **Add New Payment**
3. Fill in the payment details:
   - Amount
   - Date
   - Category
   - Currency
   - Description
4. Click **Save**

### Generating Reports

1. Navigate to the **Reports** tab
2. Select date range and filters
3. Click **Generate Report**
4. Export to Excel using the **Export** button

### Changing Theme

The application supports Light and Dark themes that automatically apply to all forms including Settings.

## 📁 Project Structure

```
PaymentManagementSystem/
├── clsPaymentEntities/          # Entity/Domain layer
│   └── clsPayment.cs
├── PaymentDataLayer/            # Data access layer
│   ├── clsPaymentRepo.cs
│   └── PaymentDataLayer.csproj
├── PaymentBusinessLayer/        # Business logic layer
│   ├── clsPaymentServices.cs
│   ├── PaymentConstants.cs
│   └── PaymentBusinessLayer.csproj
├── PaymentManagement/           # Presentation layer
│   ├── Forms/
│   │   ├── Base/
│   │   │   └── BasePaymentForm.cs
│   │   ├── Payment/
│   │   │   ├── Entry/
│   │   │   │   └── frmPaymentForm.cs
│   │   │   └── List/
│   │   │       ├── UniversityPaymentsForm.cs
│   │   │       └── OtherPaymentsForm.cs
│   │   ├── Reports/
│   │   │   └── ReportForm.cs
│   │   ├── Settings/
│   │   │   ├── CategoryManagementForm.cs
│   │   │   ├── CategoryEditForm.cs
│   │   │   ├── CurrencyManagementForm.cs
│   │   │   └── CurrencyEditForm.cs
│   │   └── MainForm.cs
│   ├── Helpers/
│   │   ├── UIHelper.cs
│   │   ├── ExcelHelper.cs
│   │   └── CurrencyConverter.cs
│   ├── Themes/
│   │   └── ThemeManager.cs
│   └── Program.cs
├── build.cmd                    # Build script for Command Prompt
├── build.ps1                    # Build script for PowerShell
└── README.md
```

## 🗄️ Database Schema

### Categories
| Column   | Type          | Description                    |
|----------|---------------|--------------------------------|
| ID       | INT           | Primary key (auto-increment)   |
| Name     | NVARCHAR(100) | Category name                  |
| IsActive | BIT           | Soft delete flag               |

### Currencies
| Column   | Type          | Description                    |
|----------|---------------|--------------------------------|
| ID       | INT           | Primary key (auto-increment)   |
| Code     | NVARCHAR(10)  | Currency code (e.g., USD, EUR) |
| Name     | NVARCHAR(50)  | Currency name                  |
| Symbol   | NVARCHAR(5)   | Currency symbol (e.g., $, €)   |
| IsActive | BIT           | Soft delete flag               |

### Payments
| Column       | Type           | Description                    |
|--------------|----------------|--------------------------------|
| ID           | INT            | Primary key (auto-increment)   |
| Amount       | DECIMAL(18,2)  | Payment amount                 |
| Date         | DATE           | Payment date                   |
| CategoryID   | INT            | Foreign key to Categories      |
| CurrencyCode | NVARCHAR(10)   | Currency code                  |
| Description  | NVARCHAR(500)  | Payment description            |
| IsActive     | BIT            | Soft delete flag               |

## 🔧 Technologies Used

- **C# 10.0** - Programming language
- **.NET Framework 4.7.2** - Application framework
- **Windows Forms** - UI framework
- **ADO.NET** - Database connectivity
- **SQL Server** - Database management system
- **EPPlus** - Excel export functionality
- **Git** - Version control

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Anas**
- GitHub: [@Anas43xq](https://github.com/Anas43xq)

## 📞 Support

For issues, questions, or suggestions, please open an issue on the [GitHub repository](https://github.com/Anas43xq/PaymentManagementSystem/issues).

---

⭐ If you find this project useful, please consider giving it a star!

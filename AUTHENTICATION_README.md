# Authentication Setup & Usage

## Overview

The Payment Management System now includes user authentication with **Signup** and **Login** forms. All passwords are securely hashed using PBKDF2-SHA256 with 100,000 iterations and a 16-byte random salt.

## Database Setup

### Step 1: Create Tables and Seed Data

Run the migration script on SQL Server to create the required tables (`Users`, `Roles`, `Categories`, `Currencies`, `Transactions`):

```bash
cd "d:\Documents\Study\C#\PaymentManagementSystem\PaymentDataLayer\migrations"
sqlcmd -S . -E -i init_db_with_admin_template.sql
```

This command:
- Creates `DBPayments` database (if missing)
- Creates all required tables with proper schema
- Seeds default roles (`Admin`, `User`)
- Seeds example categories (`Tuition`, `Books`, `Accommodation`)
- Seeds example currencies (`USD`, `EUR`)

### Step 2: Create Initial Admin User (Optional)

To insert an initial admin user, first generate a password hash and salt using the helper tool:

#### Generate Password Hash & Salt

1. Navigate to the generator folder:
```bash
cd "d:\Documents\Study\C#\PaymentManagementSystem\PaymentDataLayer\tools\GeneratePasswordHash"
```

2. Compile the runner:
```bash
csc PasswordHasher.cs Runner.cs
```

3. Run the generator and enter your desired admin password:
```bash
Runner.exe
```

4. Copy the printed `HashHex` and `SaltHex` values (without the `0x` prefix).

#### Insert Admin into Database

Run the migration script with the generated hash and salt:

```bash
cd "d:\Documents\Study\C#\PaymentManagementSystem\PaymentDataLayer\migrations"
sqlcmd -S . -E -i init_db_with_admin_template.sql -v HASH=<hashHex> SALT=<saltHex>
```

Replace `<hashHex>` and `<saltHex>` with the values from Step 3.

**Example:**
```bash
sqlcmd -S . -E -i init_db_with_admin_template.sql -v HASH=ABC123DEF456... SALT=789GHI012JKL...
```

## Architecture

### Data Layer (`PaymentDataLayer/clsPaymentRepo.cs`)

**Key Methods:**
- `GetUserByUsername(string username)` — Fetch user by username
- `CreateUser(string username, string email, string password, string roleName)` — Create a user with plaintext password (delegates to business layer for validation)
- `AddUser(ClsUser user)` — Persist a user with pre-hashed password and salt
- `ValidateUser(string username, string password)` — Authenticate user; returns user object on success, null on failure

### Business Layer (`PaymentBusinessLayer/clsPaymentServices.cs`)

**Key Methods:**
- `RegisterUser(string username, string email, string password, string roleName = "User")` — Validates input, checks uniqueness, enforces password policy (min 8 chars), calls repo to create user
- `AuthenticateUser(string username, string password)` — Validates input, calls repo to verify password

**Validation Rules:**
- **Username:** Required, must be unique
- **Password:** Minimum 8 characters
- **Email:** Optional but validated for basic format (must contain `@`)

### UI Layer

**Forms:**
- `frmLogin.cs` — Username/password login with link to signup
- `frmSignup.cs` — Create new account with password confirmation

**Session Management:**
- `Helpers/Session.cs` — Holds `Session.CurrentUser` (the authenticated user) for the lifetime of the application

**Startup:**
- `Program.cs` — Modified to show `frmLogin` before `MainForm`. Exit if login not successful.

## Usage

### Run the Application

1. **Build the solution:**
```bash
cd "d:\Documents\Study\C#\PaymentManagementSystem"
msbuild /t:Build
```

2. **Run the application:**
   - From Visual Studio or navigate to `PaymentManagement\bin\Debug\PaymentManagement.exe`

3. **Login dialog appears:**
   - Enter your credentials (username/password)
   - Click **Login** to authenticate
   - Click **Create account** to open signup form

### Sign Up for New Account

1. Click **Create account** link on the login form
2. Enter:
   - **Username** (must be unique)
   - **Email** (optional)
   - **Password** (min 8 characters)
   - **Confirm Password**
3. Click **Sign Up**
4. Success message appears; close and log in with new credentials

### Login with Existing Account

1. Enter username and password
2. Click **Login**
3. On successful authentication, `MainForm` opens and `Session.CurrentUser` is set
4. You can access `Session.CurrentUser.Username`, `Session.CurrentUser.RoleID`, etc., throughout the app

## Password Security

### PBKDF2-SHA256 Details
- **Algorithm:** PBKDF2 with SHA256
- **Iterations:** 100,000 (configurable in `PaymentDataLayer/AuthHelper.cs`)
- **Salt Length:** 16 bytes
- **Hash Length:** 32 bytes
- **Timing Attack Protection:** Uses `CryptographicOperations.FixedTimeEquals` for password verification

### Storage
- **PasswordHash** and **PasswordSalt** are stored as binary (VARBINARY) in the `Users` table
- Plaintext passwords are never stored or logged

## Extension Points

### Add Role-Based Features
- Check `Session.CurrentUser.RoleID` in forms to enable/disable features per role
- Example: Admin-only forms can check if role is Admin before allowing access

### Custom Validation
- Add additional validation in `clsPaymentServices.RegisterUser` (e.g., password complexity rules)
- Validation errors are raised as `ArgumentException`, caught in UI with friendly `MessageBox`

### Custom Password Policy
- Update `clsPaymentServices.RegisterUser` password length check or add regex validation
- Ensure the UI (`frmSignup`) displays the policy to the user

## Troubleshooting

### Login fails with "Invalid username or password"
- Verify username and password are correct
- Ensure user exists in `dbo.Users` table: `SELECT * FROM dbo.Users WHERE Username = '<username>';`

### Signup fails with "Username already exists"
- Choose a different username
- Check existing usernames: `SELECT Username FROM dbo.Users;`

### Signup fails with "Password must be at least 8 characters long"
- Enter a password with 8 or more characters

### Migration script errors
- Ensure SQL Server is running at `.` (local default instance)
- Verify Windows account has permissions to create databases/tables
- If sqlcmd not found, install SQL Server tools or use PowerShell (`Install-Module SqlServer`)

## Files Added/Modified

**New Files:**
- `clsPaymentEntities/clsUser.cs` — User entity
- `PaymentDataLayer/AuthHelper.cs` — PBKDF2 helpers
- `PaymentDataLayer/migrations/init_db_with_admin_template.sql` — Database migration
- `PaymentDataLayer/tools/GeneratePasswordHash/PasswordHasher.cs` — Password hasher utility
- `PaymentDataLayer/tools/GeneratePasswordHash/Runner.cs` — CLI runner for hasher
- `PaymentManagement/Helpers/Session.cs` — Session holder
- `PaymentManagement/Forms/Auth/frmLogin.cs` + Designer — Login form
- `PaymentManagement/Forms/Auth/frmSignup.cs` + Designer — Signup form

**Modified Files:**
- `PaymentDataLayer/clsPaymentRepo.cs` — Added user CRUD and validation methods
- `PaymentBusinessLayer/clsPaymentServices.cs` — Added `RegisterUser` and `AuthenticateUser`
- `PaymentManagement/Program.cs` — Wired login form before `MainForm`

## Next Steps (Optional)

- Add logout button/menu in `MainForm` and clear `Session.CurrentUser`
- Add role-based menu/form visibility (e.g., admin-only management screens)
- Add password change/reset functionality
- Add audit logging (who created/modified records)
- Add unit tests for `AuthHelper` and `clsPaymentServices` auth methods

---

**Project Type:** University Assignment  
**Instructor Requirements Met:**
- ✓ Signup and Login forms implemented
- ✓ At least 4 database tables: `Users`, `Roles`, `Categories`, `Currencies`, `Transactions` (5 total)
- ✓ Password security: PBKDF2-SHA256 with minimum 8-character policy

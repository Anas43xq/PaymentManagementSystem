Add-Type -AssemblyName System.Security

# Parameters
$password = "Admin@123"  # Change this to your desired admin password
$username = "admin"
$email = "admin@university.edu"

# PBKDF2 parameters
$iterations = 100000
$saltLength = 16
$hashLength = 32

# Generate random salt
$salt = New-Object byte[] $saltLength
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
$rng.GetBytes($salt)

# Create PBKDF2 hasher
$pbkdf2 = New-Object System.Security.Cryptography.Rfc2898DeriveBytes($password, $salt, $iterations, "SHA256")
$hash = $pbkdf2.GetBytes($hashLength)

# Convert to hex strings for SQL
$hashHex = -join ($hash | ForEach-Object { $_.ToString("x2") })
$saltHex = -join ($salt | ForEach-Object { $_.ToString("x2") })

Write-Host "Admin Account Created:" -ForegroundColor Green
Write-Host "Username: $username" -ForegroundColor Cyan
Write-Host "Password: $password" -ForegroundColor Cyan
Write-Host "Email: $email" -ForegroundColor Cyan
Write-Host ""
Write-Host "SQL INSERT Statement:" -ForegroundColor Yellow
Write-Host "INSERT INTO Users (Username, Email, PasswordHash, PasswordSalt, RoleId, CreatedAt)" -ForegroundColor Gray
Write-Host "VALUES ('$username', '$email', 0x$hashHex, 0x$saltHex, 1, GETDATE());" -ForegroundColor Gray
Write-Host ""
Write-Host "Hash (Hex): $hashHex" -ForegroundColor White
Write-Host "Salt (Hex): $saltHex" -ForegroundColor White

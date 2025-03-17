# LockByte - File Encryption Tool

LockByte is a simple and secure file encryption tool built with **C# and Windows Forms**. It allows users to encrypt and decrypt files using AES-256 encryption, ensuring data security and confidentiality.

![LockByte logo](/Resources/data-encryption.png)

## Features
- **Secure AES-256 Encryption**: Protect your files with strong encryption.
- **Custom Save Locations**: Choose where to save encrypted and decrypted files.
- **User-Friendly Interface**: Easy-to-use Windows Forms UI.
- **Error Handling**: Detects invalid keys and provides user feedback.

## Installation
### Prerequisites
- Windows OS
- .NET Framework (or .NET Core if using a newer version)
- Visual Studio (for development)

### Steps
1. Clone this repository:
   ```sh
   git clone https://github.com/yourusername/LockByte.git
   ```
2. Open the solution (`.sln`) file in Visual Studio.
3. Build and run the project.

## Usage
### Encrypt a File
1. Open LockByte.
2. Select a file to encrypt.
3. Enter a password.
4. Choose a location to save the encrypted file.
5. Click **Encrypt**.

### Decrypt a File
1. Open LockByte.
2. Select an encrypted `.lockbyte` file.
3. Enter the correct password.
4. Choose a location to save the decrypted file.
5. Click **Decrypt**.

## Error Handling
- Displays a message box if an incorrect password is entered.
- Handles file-related errors.

## ScreenShots

![Start-up Screen](/screenshots/startup-screen.jpg)
---
![Choosing file](/screenshots/choosing-file.jpg)
---
[View Images Folder](images/)



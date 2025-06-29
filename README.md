# ASP.NET Core Data Protection API

This project demonstrates how to use **ASP.NET Core Data Protection** to securely encrypt and decrypt data. It provides simple `/protect` and `/unprotect` endpoints to manage sensitive information.

## 🛠️ Features

- Securely **protect** (encrypt) and **unprotect** (decrypt) sensitive data.
- Supports **HTTPS** for secure data transfer.
- Customizable **data protection purpose** for scoped encryption.
- Cloud-ready with support for **persistent key storage** (e.g., Azure Blob, AWS S3).

## 📌 Requirements

Ensure the following are installed on your system:

- .NET 9.0 SDK or later: [Download .NET](https://dotnet.microsoft.com/download)

## 🚀 Getting Started

### 1. Clone the repository:
```bash
git clone https://github.com/fkucukkara/dataProtector101.git
cd aspnetcore-data-protection
```

### 2. Run the application:

```bash
dotnet run
```

The application will be available at `https://localhost:5001` (or the default port).

## 📊 API Endpoints

### ➤ **Protect Data**

Encrypts the provided data securely.

```http
POST /protect
```

**Query Parameters:**
- `data` (string, required) – The data to be encrypted.

**Example Request:**
```bash
curl -X POST "https://localhost:5001/protect?data=HelloWorld"
```

**Response:**
```json
{
  "ProtectedData": "CfDJ8M8...=="
}
```

### ➤ **Unprotect Data**

Decrypts previously protected data.

```http
POST /unprotect
```

**Query Parameters:**
- `data` (string, required) – The encrypted data to be decrypted.

**Example Request:**
```bash
curl -X POST "https://localhost:5001/unprotect?data=CfDJ8M8...=="
```

**Response:**
```json
{
  "UnprotectedData": "HelloWorld"
}
```

## 🔧 Customization

### ➤ Set Application Name
Ensure consistent key sharing across different applications.

```csharp
builder.Services.AddDataProtection()
    .SetApplicationName("MySecureApp");
```

### ➤ Persist Keys to File System
Store encryption keys securely on disk.

```csharp
using System.IO;

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/var/keys"))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(90));
```

### ➤ Custom Protectors
Use different **purposes** for better data isolation.

```csharp
var protector = provider.CreateProtector("CustomPurpose");
```

## ☁️ Cloud Integration

### ➤ Azure Blob Storage

```csharp
using Azure.Storage.Blobs;

builder.Services.AddDataProtection()
    .PersistKeysToAzureBlobStorage(new Uri("<BlobContainerUri>"));
```

### ➤ AWS S3

```csharp
using Amazon.S3;

var s3Client = new AmazonS3Client();
builder.Services.AddDataProtection()
    .PersistKeysToAWS(s3Client, "bucket-name", "keys-folder");
```

## 🧹 Cleaning Up
To **revoke** old keys, delete the key files from the storage location.

## 📝 Notes
1. Always use **HTTPS** in production.
2. Use different **purposes** for better data isolation.
3. Regularly rotate encryption keys using `SetDefaultKeyLifetime()`.

## 📖 Resources
- [Microsoft Docs: Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/)

## 📬 Contribution
Feel free to open issues or contribute via pull requests.

## License
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This project is licensed under the MIT License. See the [`LICENSE`](LICENSE) file for details.
# SecureVault — V2 | C# .NET CLI

> A complete CLI vault application rebuilt from the ground up in C# .NET — featuring AES-256 encryption, a modular OOP architecture, and a multi-account login system. This is the second iteration of SecureVault, and the foundation for a future web release.

---

## Why a Monorepo?

This project is part of a **multi-version monorepo** designed to track the full evolution of SecureVault across languages, architectures, and complexity levels.

> **Note:** The git history for V1 (C++ CLI) was lost during the monorepo restructuring. The V1 source code is intact under `v1-cpp-cli/`. This was an unfortunate git incident.

After completing V1 in C++, I chose to rebuild SecureVault in C# before moving to a GUI implementation. The goal was to train my C# and .NET skills on a real project with real constraints instead of toy examples.

---

## Planned Versions

| Version | Stack | Status |
|---|---|---|
| V1 — C++ CLI | C++17, XOR encryption, fstream | ✅ Complete |
| V2 — C# CLI | C# .NET 10, AES-256, StreamReader/Writer | ✅ Complete |
| V3 — Web Application | ASP.NET Core + SQL + TypeScript + HTML/CSS | 🔄 Planned |

## Why the change from WPF to Web?
> The original plan was a Windows desktop GUI (WPF + C++ DLL). After further research and career planning, the decision was made to pivot to a full web stack, ASP.NET Core Backend + TypeScript/HTML/CSS FrontEnd + SQL Database. This aligns better with the job market and modern development practices, and the result will be a deployed, publicly accessible application rather than a local desktop app. Which I believe will be more impressive.
---

## What's New in V2

V2 is a significant upgrade over V1 — same feature set, but rebuilt with professional-grade tools and a cleaner architecture:

- **AES-256 encryption** replaces the custom XOR cipher
- **Deterministic key derivation** via SHA-256 — the master password generates a consistent 256-bit key at every session, no key storage required
- **LINQ powered password strength analysis**
- **Structured file persistence with encrypted payloads**

---

## Features

- 🔐 **Login System** — account creation and authentication with AES-256 encrypted credentials
- 🗂️ **Password Manager** — add, retrieve, list, and delete encrypted passwords linked to sites and usernames
- 🔍 **Password Strength Analyzer** — scores passwords out of 100 based on length, uppercase, lowercase, digits, and special characters
- 🎲 **Password Generator** — generates cryptographically varied random passwords with full character set control
- 📝 **Secure Notes** — store and retrieve personal notes encrypted with your master key

---

## How It Works

- The **master password** is hashed via SHA-256 to derive a 256-bit AES key
- The **IV** is derived from the key itself
- All sensitive data is encrypted before being written to disk
- Data is persisted in structured `.txt` files using a `field|encrypted_payload` format
- The entire application is driven by a centralized **App Loop** with an `AppState` enum managing navigation

---

## 🔒 Security Model

| Component | V1 (C++) | V2 (C#) |
|---|---|---|
| Encryption | Custom XOR cipher | AES-256 CBC |
| Key derivation | User-defined integer key | SHA-256 from master password |
| IV | None | Derived from key via SHA-256 |
| Persistence | `.dat` files | `.txt` files with encrypted payloads |

---

## Built With

- **C# .NET 10**
- **AES-256 CBC** — `System.Security.Cryptography`
- **SHA-256** — deterministic key derivation
- **LINQ** — password strength analysis
- **StreamReader / StreamWriter** — file persistence
- **OOP** — classes, structs, properties, enums
- **Visual Studio Community**

---

## Project Structure

```
v2-csharp-cli/
└── src/
    ├── main.cs
    ├── App.cs
    ├── AppState.cs
    ├── Login.cs
    ├── EncryptionService.cs
    ├── PasswordManager.cs
    ├── PasswordStrengthAnalyzer.cs
    ├── PasswordGenerator.cs
    └── SecureNotes.cs
```

---

## How to Use

1. Clone the repository and open the `.sln` file in Visual Studio
2. Build and run the project (`Ctrl+F5`)
3. Create an account with a username and master password
4. Log in — your master password generates the AES key for the session
5. Navigate the menu to access all modules
6. **Important** — your master password is the only key to your data. It is never stored. All encrypted data is unrecoverable without it.

---

## What's Next — Final Version (Not Planned to be V3 yet)

V3 will be a full **Web application** built with:
- ASP.NET Core — REST API backend
- SQL — structured data storage replacing file-based persistence
- TypeScript + HTML/CSS — modern frontend interface
- Deployed online — publicly accessible via a real URL

The goal is to rebuild SecureVault as a production-grade web application. Showing full-stack architecture, database design, authentication, and cloud deployment.

---

## Author

**Yuman Khoufache** — First-year Computer Science student, self-directed learner, and aspiring software developer.

---

## Special Thanks

- **Claude (Anthropic)** — minor debugging assistance (Learning and using C#Syntax was easier than C++ lol)

---

## License

© 2026 Yuman Khoufache — No copyright restrictions.

---

🔗 [GitHub](https://github.com/YumanKh) | [V1 — C++ CLI](../v1-cpp-cli/) | V3 — Coming Soon


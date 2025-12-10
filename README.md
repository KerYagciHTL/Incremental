# Incremental Game

A modern incremental/idle game built with Avalonia UI and .NET 8, featuring a custom big number system and passive income mechanics.

## 🎮 Features

- **Passive Income System**: Automatically earn money every second without any interaction
- **BigNum System**: Custom number formatting supporting suffixes (K, M, B, T, Qd, Qn, Sx, Sp, Oc, No, De, and beyond)
- **Upgrade System**: Purchase upgrades to boost your income
  - **2× Cash**: Doubles your cash multiplier
  - **+1 Base Cash**: Increases base cash per tick
- **Clean UI**: Modern dark-themed interface built with Avalonia
- **Fullscreen Mode**: Immersive gaming experience

## 🏗️ Project Structure

```
Incremental/
├── BigNum/              # Custom big number library
│   ├── BigNum.cs       # Main BigNum class with arithmetic operators
│   └── Suffix.cs       # Enum for number suffixes (K, M, B, T, etc.)
├── Incremental/        # Core game logic library
│   └── Money.cs        # Money management system
├── UI/                 # Avalonia UI application
│   ├── Program.cs      # Entry point
│   ├── MainWindow.cs   # Main game window
│   └── Stackdisplay.cs # Game UI components
└── Incremental.Tests/  # Unit tests
    ├── BigNumTests.cs  # BigNum calculation tests
    └── MoneyTests.cs   # Money system tests
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Linux, macOS, or Windows

### Installation

1. Clone this repository:
   ```bash
   git clone <repository-url>
   cd Incremental
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

### Running the Game

Run the game using:
```bash
dotnet run --project UI/UI.csproj
```

The game will launch in fullscreen mode.

## 🎯 How to Play

### Basic Mechanics

- **Passive Income**: The game automatically generates 1 money per second
- **Manual Income**: Press `C` to manually gain cash
- **Upgrades**: Click on upgrade cards to purchase them and boost your income

### Keyboard Controls

- `C` - Manually gain cash
- `ESC` - Exit fullscreen

### Upgrade Strategy

1. Start by saving up for the **+1 Base Cash** upgrade to increase your passive income rate
2. Once you have steady income, purchase **2× Cash** to multiply your earnings
3. Balance between both upgrades for optimal growth

## 🧪 Testing

Run the test suite:
```bash
dotnet test
```

All tests should pass:
- **BigNum Tests**: Validates number parsing, formatting, and arithmetic operations
- **Money Tests**: Ensures proper money calculations

## 🛠️ Development

### Building Individual Projects

```bash
# Build BigNum library
dotnet build BigNum/BigNum.csproj

# Build UI
dotnet build UI/UI.csproj

# Build tests
dotnet build Incremental.Tests/Incremental.Tests.csproj
```

### Running Tests with Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 📊 BigNum System

The custom BigNum system supports arithmetic with large numbers using suffixes:

- **None**: Base numbers (1-999)
- **K**: Thousands (1,000+)
- **M**: Millions (1,000,000+)
- **B**: Billions (1,000,000,000+)
- **T**: Trillions
- **Qd**: Quadrillions
- **Qn**: Quintillions
- **Sx**: Sextillions
- **Sp**: Septillions
- **Oc**: Octillions
- **No**: Nonillions
- **De**: Decillions
- And many more...

### Example Operations

```csharp
var a = new BigNum(600, Suffix.K);  // 600K
var b = new BigNum(500, Suffix.K);  // 500K
var result = a + b;                 // 1.1M

var c = new BigNum(0.5, Suffix.M);  // 0.5M
var d = new BigNum(0.2, Suffix.M);  // 0.2M
var (diff, success) = c - d;        // 300K
```

## 🔧 Technical Details

- **Framework**: .NET 8.0
- **UI Framework**: Avalonia 11.x
- **Language**: C# 12
- **Architecture**: Multi-project solution with separation of concerns
- **Timer**: Non-blocking DispatcherTimer for passive income
- **Testing**: xUnit with 25+ unit tests

## 📝 License & Copyright

**Copyright © 2025 Nico. All Rights Reserved.**

This project and all of its contents are the exclusive property of Nico.

### Terms of Use

- ❌ **NO COPYING**: This code may not be copied, reproduced, or distributed in any form
- ❌ **NO MODIFICATION**: Creating derivative works is strictly prohibited
- ❌ **NO COMMERCIAL USE**: This software may not be used for commercial purposes
- ❌ **NO REDISTRIBUTION**: Sharing or publishing this code is not permitted
- ⚠️ **VIEWING ONLY**: This repository is available for viewing and personal learning only

**All rights, title, and interest in and to this software remain with Nico.**

Any unauthorized use, copying, modification, or distribution of this software is strictly prohibited and may result in severe civil and criminal penalties.

## 👤 Author

**Nico**

This project was created and developed entirely by Nico in December 2025.

## 🤝 Contributing

This is a personal project and is **not accepting contributions**.

---

**Made with ❤️ by Nico | December 2025**

**All credits and ownership belong exclusively to Nico - Forever and Always**


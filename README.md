<p align="center">
  <a href="https://dotnet.microsoft.com/" target="_blank">
    <img src="https://raw.githubusercontent.com/dotnet/brand/main/logo/dotnet-logo.svg" width="120" alt=".NET Logo">
  </a>
  <h1 align="center">Sarasavi Library Admin Portal</h1>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET%20Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Framework">
  <img src="https://img.shields.io/badge/ASP.NET%20MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET MVC">
  <img src="https://img.shields.io/badge/Tailwind%20CSS-06B6D4?style=for-the-badge&logo=tailwindcss&logoColor=white" alt="Tailwind CSS">
</p>

<p align="center">
<a href="https://github.com/dotnet/core/actions"><img src="https://img.shields.io/badge/build-passing-brightgreen.svg" alt="Build Status"></a>
<a href="https://www.nuget.org/"><img src="https://img.shields.io/nuget/v/Microsoft.AspNet.Mvc" alt="Latest Stable Version"></a>
<a href="https://opensource.org/licenses/MIT"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License"></a>
</p>

---

## 🚀 About The Project

This project delivers an elegant, high-performance administrative console built on **.NET MVC** and styled with **Tailwind CSS**. It provides a fully unified workspace tailored for library staff to seamlessly catalog resources, manage user accounts, oversee loans, and handle public requests in real time.

### Key Architectural Pillars

- **Robust MVC Backend:** Built using the enterprise reliability of the .NET architecture, featuring clean separation of concerns, rapid server routing, and strongly typed layouts.
- **Modern Adaptive Frontend:** A beautiful, responsive interface compiled instantly using Tailwind CSS utility classes, utilizing custom refined SVGs for a fast, vector-perfect layout.
- **Enterprise Workflows:** Includes ready-to-use structural sections built for high-throughput daily operations, ensuring library data integrity across transactions.

---

## 🛠️ Getting Started

Follow these steps to spin up the local development environment.

### Prerequisites

- Visual Studio 2022 (with _Web Development_ workload enabled)
- .NET SDK (v8.0+ or compatible .NET Framework developer pack)
- SQL Server LocalDB (or equivalent configured database instance)

### Installation

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/SarasaviLibrary.git](https://github.com/DenethKavinda/Library-Management.git)
   cd SarasaviLibrary
   ```

Restore NuGet Dependencies:
Open the solution (SarasaviLibrary.sln) in Visual Studio or use the CLI:

Bash
dotnet restore
Database Configuration & Setup:
Verify your connection string inside appsettings.json (or Web.config), then run the initialization migrations:

Bash
dotnet ef database update
Run the Application:
Launch via Visual Studio (F5) or execute the terminal runtime command:

Bash
dotnet run
Open your browser and navigate to https://localhost:5001 to access the portal dashboard.

<div align="center">

<img src="https://capsule-render.vercel.app/api?type=waving&color=0:512BD4,100:06B6D4&height=220&section=header&text=Sarasavi%20Library&fontSize=55&fontColor=ffffff&animation=fadeIn&fontAlignY=35&desc=Admin%20Portal%20%E2%80%94%20.NET%20MVC%20%2B%20Tailwind%20CSS&descAlignY=55&descSize=18" width="100%"/>

<img src="https://readme-typing-svg.demolab.com?font=Fira+Code&size=22&duration=3000&pause=800&color=512BD4&center=true&vCenter=true&width=650&lines=Enterprise+library+management+system+%F0%9F%93%9A;Built+on+ASP.NET+MVC+%E2%9A%A1;Styled+with+Tailwind+CSS+%F0%9F%8E%A8;Manage+catalogs%2C+loans+%26+users+in+real+time" alt="Typing SVG" />

<br/>

<a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET%20Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" /></a>
<a href="https://dotnet.microsoft.com/en-us/apps/aspnet"><img src="https://img.shields.io/badge/ASP.NET%20MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" /></a>
<a href="https://tailwindcss.com/"><img src="https://img.shields.io/badge/Tailwind%20CSS-06B6D4?style=for-the-badge&logo=tailwindcss&logoColor=white" /></a>
<a href="https://www.microsoft.com/en-us/sql-server"><img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" /></a>

<br/><br/>

<a href="https://github.com/dotnet/core/actions"><img src="https://img.shields.io/badge/build-passing-brightgreen.svg?style=flat-square" /></a>
<a href="https://www.nuget.org/"><img src="https://img.shields.io/nuget/v/Microsoft.AspNet.Mvc?style=flat-square" /></a>
<a href="https://opensource.org/licenses/MIT"><img src="https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square" /></a>
![GitHub repo size](https://img.shields.io/github/repo-size/DenethKavinda/Library-Management?style=flat-square&color=512BD4)
![GitHub last commit](https://img.shields.io/github/last-commit/DenethKavinda/Library-Management?style=flat-square&color=06B6D4)

</div>

<br/>

## 🚀 About The Project

**Sarasavi Library Admin Portal** delivers an elegant, high-performance administrative console built on **.NET MVC** and styled with **Tailwind CSS**. It provides a fully unified workspace tailored for library staff to seamlessly catalog resources, manage user accounts, oversee loans, and handle public requests in real time.

<br/>

### 🏛️ Key Architectural Pillars

| Pillar | Description |
|---|---|
| 🧱 **Robust MVC Backend** | Built on the enterprise reliability of .NET architecture — clean separation of concerns, rapid server routing, and strongly typed layouts. |
| 🎨 **Modern Adaptive Frontend** | A responsive interface compiled instantly with Tailwind CSS utility classes, using refined custom SVGs for a crisp, vector-perfect layout. |
| ⚙️ **Enterprise Workflows** | Ready-to-use structural sections built for high-throughput daily operations, ensuring library data integrity across every transaction. |

<br/>

## 🧰 Tech Stack

<div align="center">
<img src="https://skillicons.dev/icons?i=dotnet,cs,tailwind,mssql,visualstudio,html,css,js&theme=dark" />
</div>

<br/>

## ✨ Features

- 📚 Catalog management — add, edit, and organize library resources
- 👥 User & membership account administration
- 🔄 Real-time loan tracking (checkouts, returns, overdue alerts)
- 📥 Handle public/patron requests from a unified dashboard
- 📊 Reporting & operational overview for staff
- 🔐 Role-based access for admin staff
- 💨 Fast, responsive UI powered by Tailwind CSS

<br/>

## 🎬 Preview

<div align="center">
<img src="https://user-images.githubusercontent.com/placeholder/dashboard-demo.gif" width="700" alt="Admin dashboard preview - replace with your own screen recording" />

<sub>👆 Replace this with a real screenshot or screen recording of the admin dashboard</sub>
</div>

<br/>

## 🛠️ Getting Started

Follow these steps to spin up the local development environment.

### ✅ Prerequisites

- 🖥️ Visual Studio 2022 (with the **Web Development** workload enabled)
- 🔷 .NET SDK (v8.0+ or a compatible .NET Framework developer pack)
- 🗄️ SQL Server LocalDB (or an equivalent configured database instance)

<br/>

### 📦 Installation

**1️⃣ Clone the repository**

```bash
git clone https://github.com/DenethKavinda/Library-Management.git
cd Library-Management
```

**2️⃣ Restore NuGet dependencies**

Open the solution (`SarasaviLibrary.sln`) in Visual Studio, or use the CLI:

```bash
dotnet restore
```

**3️⃣ Configure & set up the database**

Verify your connection string inside `appsettings.json` (or `Web.config`), then run the initialization migrations:

```bash
dotnet ef database update
```

**4️⃣ Run the application**

Launch via Visual Studio (**F5**) or execute the terminal runtime command:

```bash
dotnet run
```

Open your browser and navigate to **https://localhost:5001** to access the portal dashboard. 🎉

<br/>

## 📁 Project Structure

```
SarasaviLibrary/
├── Controllers/        # MVC controllers (Catalog, Users, Loans, Requests)
├── Models/             # Entity & view models
├── Views/              # Razor views styled with Tailwind CSS
├── wwwroot/             # Static assets (CSS, JS, images)
├── Data/                # DbContext & EF Core migrations
├── appsettings.json     # App & connection configuration
└── SarasaviLibrary.sln
```

<br/>

## 🗺️ Roadmap

- [ ] Email notifications for overdue loans
- [ ] Barcode / QR-based checkout
- [ ] Analytics dashboard with charts
- [ ] Multi-branch library support

<br/>

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the project
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

<br/>

## 📄 License

This project is distributed under the **MIT License**. See [LICENSE](LICENSE) for details.

<br/>

<div align="center">

### 👨‍💻 Author

**Deneth Kavinda**

[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/DenethKavinda)

<br/>

<img src="https://capsule-render.vercel.app/api?type=waving&color=0:06B6D4,100:512BD4&height=120&section=footer" width="100%"/>

</div>

<div align="center">

# 🛍️ Store.Web
### Enterprise E-Commerce Platform Built with Clean Architecture & Specification Pattern

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20%26%20Onion-blue?style=for-the-badge&logo=diagram-project&logoColor=white)](#-system-architecture)
[![Specification Pattern](https://img.shields.io/badge/Pattern-Specification-orange?style=for-the-badge)](#-architecture-patterns)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellowgreen?style=for-the-badge)](LICENSE)
[![Author](https://img.shields.io/badge/Author-Omar%20Alfarouk-orange?style=for-the-badge&logo=github&logoColor=white)](https://github.com/OmarAlfar0uk)

<p align="center">
  <a href="#-key-features">Key Features</a> •
  <a href="#-system-architecture">System Architecture</a> •
  <a href="#-tech-stack">Tech Stack</a> •
  <a href="#-project-structure">Project Structure</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-author">Author</a>
</p>

</div>

---

## 📌 Executive Overview

**Store.Web** is a high-performance e-commerce backend built with ASP.NET Core, engineered around Clean Architecture and enterprise design patterns. It provides an extensible catalog and inventory system featuring Brands, Categories, and Products with dynamic filtering, sorting, and pagination supported by the **Specification Pattern** and generic repository abstractions.

> [!NOTE]
> Employs the **Specification Pattern** to encapsulate complex LINQ query logic (sorting, filtering, pagination, and eager loading of navigation properties) cleanly outside repository classes.

---

## ✨ Key Features

| ⚡ Feature | 💡 Description | 🛠 Engineering Detail |
|---|---|---|
| **📦 Dynamic Product Catalog** | Products categorized by `ProductBrand` and `ProductType` | Entity Framework Core relational mappings with foreign keys |
| **🔍 Specification Pattern** | Composable querying (search, filter, sort, paginate) | Decouples query logic via `ISpecification<T>` |
| **🗄️ Generic Repository & UoW** | Reusable data access layer | Centralized repository with Unit of Work pattern |
| **⚡ High Performance** | Asynchronous queries with projection support | Minimized query overhead and optimized indexing |
| **🛡️ Layer Isolation** | Clear boundary between Core and Infrastructure | Zero database dependencies in domain models |

---

## 🏛 System Architecture

```mermaid
flowchart TD
    subgraph Web["🌐 Presentation Layer"]
        Controllers["Controllers (Products, Brands, Types)"]
    end

    subgraph Service["📐 Application & Service Layer"]
        ProductSvc["Product Service"]
        ServiceAbst["Service Abstractions & DTOs"]
    end

    subgraph Core["🏛️ Core Domain"]
        Product["Product Entity"]
        Brand["ProductBrand Entity"]
        Type["ProductType Entity"]
        Spec["ISpecification<T>"]
    end

    subgraph Infra["⚙️ Infrastructure & Persistence"]
        StoreContext["StoreDbContext"]
        Repo["Generic Repository & UnitOfWork"]
        SQL[("SQL Server")]
    end

    Web --> ServiceAbst
    Service --> Core
    Service --> Repo
    Repo --> StoreContext
    StoreContext --> SQL
```

---

## ⚡ Tech Stack

| Category | Technology | Purpose |
|---|---|---|
| **Platform** | ![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23_12-239120?style=flat-square&logo=csharp&logoColor=white) | Enterprise Web API framework |
| **Patterns** | ![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-Onion-00599C?style=flat-square) ![Specification](https://img.shields.io/badge/Pattern-Specification-orange?style=flat-square) | Clean domain modeling and composable query construction |
| **Data & ORM** | ![EF Core](https://img.shields.io/badge/EF_Core-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![SQL Server](https://img.shields.io/badge/MS_SQL_Server-CC292B?style=flat-square&logo=microsoftsqlserver&logoColor=white) | Relational persistence, fluent configurations, and migrations |

---

## 📂 Project Structure

```text
Store.Web/
├── Core/
│   ├── DomainLayer/             # Entities: Product, ProductBrand, ProductType, BaseEntity
│   ├── Service/                 # Business logic services
│   └── ServiceAbstraction/      # Service contracts & interfaces
├── Infrastructure/
│   ├── Persistence/             # StoreDbContext, Migrations, Repository implementations
│   └── Presentaion/             # Web API Presentation controllers
├── Store.Web/                   # Application host, Program.cs & config
└── Store.Web.sln
```

---

## 🚀 Getting Started

1. **Clone repository:**
   ```bash
   git clone https://github.com/OmarAlfar0uk/Store.Web.git
   cd Store.Web
   ```

2. **Build & Run:**
   ```bash
   dotnet restore
   dotnet run --project Store.Web
   ```

---

## 👨‍💻 Author

**Omar Alfarouk**  
*Full-Stack .NET & Software Engineer*  

- 🌐 **GitHub:** [@OmarAlfar0uk](https://github.com/OmarAlfar0uk)
- 💼 **LinkedIn:** [omar-alfarouk](https://www.linkedin.com/in/omar-alfarouk-252471251/)
- 📧 **Email:** [omaralfarouk646@gmail.com](mailto:omaralfarouk646@gmail.com)

---

<div align="center">
  <sub>Built with ❤️ by Omar Alfarouk. Licensed under the <a href="LICENSE">MIT License</a>.</sub>
</div>

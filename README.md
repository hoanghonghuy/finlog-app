# FinLog - Personal Finance API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?style=flat&logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?style=flat&logo=docker)

**FinLog** is a robust RESTful Generic API designed for personal finance management. It provides a complete backend solution for tracking expenses, managing accounts, planning budgets, and analyzing financial habits.

This project is built with **Clean Architecture** principles, ensuring scalability, maintainability, and ease of testing.

---

## Features

### Core Modules
- **Transactions Management**: Record income and expenses with detailed descriptions and dates.
- **Smart Accounts (Wallets)**: 
  - Manage multiple accounts (Cash, Bank, Credit Card).
  - **Auto-Sync**: Account balances automatically update when transactions are created, edited, or deleted.
- **Categories**: Organize finance with custom categories (e.g., Food, Salary, Rent).
- **Budgets**: Set monthly spending limits per category to keep track of goals.

### Advanced Capabilities
- **Reporting & Statistics**: Real-time monthly income/expense summaries and date-range filtering.
- **Data Integrity**: 
  - **Safe Deletion**: Deleting categories/accounts preserves historical transaction data (Foreign Keys set to NULL).
  - **Robust Logic**: Handles complex scenarios like reverting balances when editing transaction amounts or switching accounts.

---

## Technology Stack

- **Framework**: .NET 8 (Core Web API)
- **Database**: PostgreSQL 16
- **ORM**: Entity Framework Core (Code-First)
- **Containerization**: Docker & Docker Compose
- **Mapping**: AutoMapper
- **Logging**: Serilog
- **Documentation**: Swagger / OpenAPI

---

## Project Structure

```
finlog-app/
├── backend/                # Backend Source Code
│   ├── src/
│   │   ├── FinLog.Api/     # Controllers, Filters, DI
│   │   ├── FinLog.Core/    # Entities, Interfaces, DTOs
│   │   └── FinLog.Infrastructure/ # DbContext, Repositories
│   ├── docker-compose.yml  # Database Container Config
│   ├── FinLog.sln          # .NET Solution File
│   └── .env.example        # Environment Config Template
└── README.md
```

---

## Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) or Docker Compose.
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

### Installation

1.  **Clone the repository**
    ```bash
    git clone https://github.com/yourusername/finlog-app.git
    cd finlog-app
    ```

2.  **Configure Environment**
    Navigate to the `backend` directory and set up your environment variables.
    ```bash
    cd backend
    cp .env.example .env
    ```
    *Modify `.env` if you wish to change the default database credentials or ports.*

3.  **Start Database**
    Run the PostgreSQL container using Docker Compose.
    ```bash
    docker-compose up -d
    ```

4.  **Apply Database Migrations**
    Initialize the database structure.
    ```bash
    dotnet ef database update --project src/FinLog.Infrastructure --startup-project src/FinLog.Api
    ```

5.  **Run the Application**
    ```bash
    dotnet run --project src/FinLog.Api
    ```

### Exploring the API

Once running, the API is available at: `http://localhost:5000`

- **Swagger UI**: Visit [http://localhost:5000/swagger](http://localhost:5000/swagger) for interactive API documentation and testing.

---

## Security & CORS

- **CORS**: The API is configured to allow requests from **any origin** (`AllowAll` policy) by default to facilitate seamless frontend development (e.g., React/Vue running on localhost:3000).

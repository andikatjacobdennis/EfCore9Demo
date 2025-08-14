# EfCore9Demo

**A .NET 9 + Entity Framework Core 9 sample demonstrating a three-level one-to-many relationship**:

```

Blog → Post → Comment

```

This project shows how to:

- Define entity relationships using **Code-First EF Core 9**
- Configure **DbContext** with cascade delete behavior
- Manage database schema using **migrations**
- Seed hierarchical data (Blogs, Posts, and Comments)
- Perform CRUD operations with **eager loading** using `.Include()` and `.ThenInclude()`
- Use a `.bat` script for quick, repeatable setup

---

##  Repository Overview

```

EfCore9Demo/
├── EfCore9Demo.sln
├── EfCore9Demo.App/
│   ├── EfCore9Demo.App.csproj
│   ├── Program.cs
│   ├── AppDbContext.cs
│   ├── Models/
│   │   ├── Blog.cs
│   │   ├── Post.cs
│   │   └── Comment.cs
│   ├── Migrations/
│   │   └── …migration files…
│   └── setup-efcore9-demo.bat
├── README.md
└── LICENSE (MIT)

````

- **`EfCore9Demo.App`**: Main console application containing entity definitions, `DbContext`, program logic, migrations, and the setup script.
- **`setup-efcore9-demo.bat`**: Automates creation of solution/project scaffolding and EF Core dependencies.
- **LICENSE**: Project is licensed under **MIT**, suitable for reuse and adaptation.

---

##  Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- **SQL Server** (modify provider for PostgreSQL, SQLite, etc.)
- EF Core CLI tool (optional but recommended):
  ```bash
  dotnet tool install --global dotnet-ef
````

---

### Option A: Setup with Script (Windows)

Run the included setup script:

```bash
cd EfCore9Demo
setup-efcore9-demo.bat
```

This will:

* Create the solution and console app
* Add EF Core 9 packages and the SQL Server provider
* Prepare the environment for migrations

---

### Option B: Manual Setup

Alternatively, manually replicate the setup:

```bash
dotnet new sln -n EfCore9Demo
dotnet new console -n EfCore9Demo.App --framework net9.0
dotnet sln EfCore9Demo.sln add EfCore9Demo.App/EfCore9Demo.App.csproj
cd EfCore9Demo.App
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
```

---

### Run the Example

From the project directory:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

**Expected output:**

```
Blog: https://example.com
  Post: Hello EF Core 9
    Comment by Alice: Nice article!
```

---

## Customization & Extensions

* **Switch database provider**: For SQLite, PostgreSQL, or Cosmos DB, replace `UseSqlServer()` in `AppDbContext` and adjust packages.
* **Extend the model**: Add more entities or deeper relationships.
* **Web API version**: Convert this to a Minimal API or MVC Web API using ASP.NET Core 9.

---

## License

This project is open-sourced under the [MIT License](LICENSE). Feel free to fork, customize, and contribute.

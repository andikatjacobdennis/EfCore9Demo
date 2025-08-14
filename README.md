# Entity Framework Core 9 Demo

**A .NET 9 + Entity Framework Core 9 sample demonstrating multiple relationship types:**

```

One-to-One:   Blog ↔ BlogDetail
One-to-Many:  Blog → Post → Comment
Many-to-Many: Post ↔ Tag

````

This project shows how to:

- Define **one-to-one**, **one-to-many**, and **many-to-many** relationships using **Code-First EF Core 9**
- Configure **DbContext** with cascade delete behavior
- Store all `DateTime` properties as SQL Server `datetime2`
- Manage database schema using **migrations**
- Seed hierarchical data (`Blog`, `BlogDetail`, `Post`, `Comment`, `Tag`)
- Insert additional data at runtime in `Program.cs`
- Perform CRUD operations with **eager loading** using `.Include()` and `.ThenInclude()`
- Use the `reset-and-run-efcore9-demo.bat` script for quick, repeatable setup

---

## Entity Relationship Diagram

```mermaid
erDiagram
    Blog ||--o{ Post : has
    Post ||--o{ Comment : has
    Blog ||--|| BlogDetail : "details"
    Post }o--o{ Tag : tagged_with

    Blog {
        int BlogId
        string Url
        string Title
        string OwnerName
        string OwnerEmail
        datetime2 CreatedAt
    }

    BlogDetail {
        int BlogId
        string Description
        datetime2 LastUpdated
    }

    Post {
        int PostId
        int BlogId
        string Title
        string Content
        decimal Rating
        double ReadTimeMinutes
        bool IsPublished
        datetime2 PublishedAt
    }

    Comment {
        int CommentId
        int PostId
        string AuthorName
        string AuthorEmail
        string Text
        enum Status
        datetime2 CreatedAt
    }

    Tag {
        int TagId
        string Name
    }
````

---

## Repository Overview

```
EfCore9Demo/
├── EfCore9Demo.sln
├── EfCore9Demo.App/
│   ├── EfCore9Demo.App.csproj
│   ├── Program.cs
│   ├── AppDbContext.cs
│   ├── Models/
│   │   ├── Blog.cs
│   │   ├── BlogDetail.cs
│   │   ├── Post.cs
│   │   ├── Comment.cs
│   │   └── Tag.cs
│   ├── Migrations/
│   │   └── …migration files…
│   ├── reset-and-run-efcore9-demo.bat
│   └── setup-efcore9-demo.bat
├── README.md
└── LICENSE (MIT)
```

* **`EfCore9Demo.App`**: Main console application containing entity definitions, `DbContext`, seeding logic, migrations, and runtime data insertion.
* **`reset-and-run-efcore9-demo.bat`**: Deletes migrations, drops DB, creates fresh migration, applies it, and runs the app in one step.
* **`setup-efcore9-demo.bat`**: Original setup script for scaffolding solution and EF Core dependencies.
* **LICENSE**: MIT License, suitable for reuse.

---

## Getting Started

### Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* **SQL Server** (change provider in `AppDbContext` for SQLite, PostgreSQL, etc.)
* EF Core CLI tool (installed automatically by the `.bat` script if missing)

---

### Option A: Run with Script (Windows)

Run the included automation script:

```bash
cd EfCore9Demo
reset-and-run-efcore9-demo.bat
```

This will:

1. Remove existing migrations
2. Drop the current database
3. Create a new migration from your model
4. Apply the migration
5. Run the console app (showing both seeded and runtime-inserted data)

---

### Option B: Manual Setup

```bash
dotnet new sln -n EfCore9Demo
dotnet new console -n EfCore9Demo.App --framework net9.0
dotnet sln EfCore9Demo.sln add EfCore9Demo.App/EfCore9Demo.App.csproj
cd EfCore9Demo.App
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0

dotnet ef database drop -f
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

---

## Example Output

**Expected console output after running the script:**

```
Blog: Tech Insights (https://example.com)
Owner: John Doe (john@example.com)
Created: 2025-08-15 14:00:00
  Blog Detail: A blog about cutting-edge tech.
  Last Updated: 2025-08-15 14:00:00
  Post: Hello EF Core 9
    Tags: EFCore, CSharp
    Comment by Alice (alice@example.com): Nice article!
  Post: Advanced EF Core 9 Queries
    Tags: EFCore
    Comment by Charlie (charlie@example.com): Looking forward to trying these out!

Blog: Data Insights (https://datainsights.com)
Owner: Jane Smith (jane@datainsights.com)
Created: 2025-08-15 14:23:11
  Blog Detail: Exploring data technologies and trends.
  Last Updated: 2025-08-15 14:23:11
  Post: Understanding EF Core Seeding
    Tags: Database
    Comment by Eve (eve@example.com): Very helpful, thanks!
```

---

## Customization & Extensions

* **Switch provider**: Change `UseSqlServer()` in `AppDbContext` to use SQLite, PostgreSQL, or Cosmos DB.
* **Extend the model**: Add new entities or deeper relationships.
* **Web API version**: Convert to Minimal API or MVC Web API with ASP.NET Core 9.

---

## License

This project is open-sourced under the [MIT License](LICENSE).
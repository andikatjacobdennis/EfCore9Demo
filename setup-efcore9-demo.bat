@echo off
REM ================================================
REM EF Core 9 Three-Level Example Setup Script
REM Creates .NET 9 solution, project, and installs packages
REM ================================================

REM 1. Create Solution
dotnet new sln -n EfCore9Demo

REM 2. Create Console App targeting .NET 9
dotnet new console -n EfCore9Demo.App --framework net9.0

REM 3. Add Project to Solution
dotnet sln EfCore9Demo.sln add EfCore9Demo.App\EfCore9Demo.App.csproj

REM 4. Navigate into the project folder
cd EfCore9Demo.App

REM 5. Install EF Core 9 core and tools
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0

REM 6. Install SQL Server provider (change if using PostgreSQL/SQLite)
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0

REM 7. Install dotnet-ef CLI globally if not already installed
dotnet tool install --global dotnet-ef

echo.
echo ================================================
echo Project setup complete!
echo Next steps:
echo 1. Add your Models and DbContext files in EfCore9Demo.App
echo 2. Run "dotnet ef migrations add InitialCreate" to create the database schema
echo 3. Run "dotnet ef database update" to apply the migration
echo ================================================
pause

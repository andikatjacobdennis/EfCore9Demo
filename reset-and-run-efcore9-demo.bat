@echo off
setlocal

echo =======================================
echo EF Core 9 Demo Full Reset & Run
echo =======================================

REM Step 1: Remove old migrations folder
echo Removing old migrations...
IF EXIST "EfCore9Demo.App\Migrations" (
    rmdir /S /Q "EfCore9Demo.App\Migrations"
    echo Migrations folder deleted.
) ELSE (
    echo No migrations folder found.
)

REM Step 2: Drop existing database
echo Dropping database...
dotnet ef database drop -p EfCore9Demo.App -f

REM Step 3: Build project before migration
echo Building project...
dotnet build EfCore9Demo.App

REM Step 4: Create a unique migration name using date-time
for /f "tokens=2-4 delims=/ " %%a in ('date /t') do (
    set today=%%c%%a%%b
)
for /f "tokens=1-2 delims=: " %%a in ("%time%") do (
    set now=%%a%%b
)
set migrationName=Init_%today%_%now%

echo Creating new migration: %migrationName%...
dotnet ef migrations add %migrationName% -p EfCore9Demo.App

REM Step 5: Update database
echo Updating database...
dotnet ef database update -p EfCore9Demo.App

REM Step 6: Run the application
echo Running application...
dotnet run --project EfCore9Demo.App

echo =======================================
echo Setup complete!
pause
endlocal

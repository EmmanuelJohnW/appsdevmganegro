# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a **Windows Forms motorcycle rental system** built with C# targeting .NET 8 (`net8.0-windows`). The solution file is at `motorcycle-rental-system/ahhh/ahhh.slnx` and the project at `motorcycle-rental-system/ahhh/ahhh/ahhh.csproj`.

## Build & Run

Build via .NET CLI (from repo root):
```powershell
dotnet build AppsdevFinal\motorcycle-rental-system\ahhh\ahhh\ahhh.csproj
```

Run the compiled executable:
```powershell
dotnet run --project AppsdevFinal\motorcycle-rental-system\ahhh\ahhh\ahhh.csproj
```

To open and build in Visual Studio 2022, open `motorcycle-rental-system\ahhh\ahhh.slnx`.

There are no automated tests in this project.

## Architecture

**Form flow** (navigation uses `Hide()`/`Show()` — forms are never `Close()`d during navigation to stay in memory):

```
WelcomeForm → RegisterForm → LoginForm → Motors
           └─────────────→ LoginForm ─┘
```

**Forms:**
- `WelcomeForm` — Landing screen; entry point (`Program.cs` starts here)
- `RegisterForm` — Writes new `username,password` line to `users.txt`
- `LoginForm` — Reads `users.txt` and matches credentials; opens `Motors` on success
- `Motors` — Main rental UI; shows 8 motorcycle options (ADV, NMAXX, AEROX, PCX, CLICK, MIO, FAZZIO, GEAR). On load, reads `rentals.txt` to disable already-rented items. On rent confirmation, appends to `rentals.txt` and disables that motor's button.
- `MotorDetails`, `Form1`, `Form2` — Unused/placeholder forms

**Data storage** (SQLite database at `Application.StartupPath`, i.e., `bin\Debug\rental.db`):
- Tables are created automatically on first run by `SupabaseService`'s static constructor.
- No external database or credentials needed — the `.db` file lives next to the executable.

**Schema:**
- `users` — `id, username (unique), password, created_at`
- `motorcycles` — `id, name, model, daily_rate (REAL), is_available (INTEGER 0/1), created_at`
- `rentals` — `id, motorcycle_id (FK), username, rented_at, returned_at, status`

## SQLite Setup

No setup required. The database file `rental.db` is created automatically in `bin\Debug\` on first launch via `SupabaseService.EnsureDbCreated()`.

NuGet package in use: `Microsoft.Data.Sqlite` v9.0.0.

## Key Design Constraints

- Passwords are stored as plaintext (no hashing).
- SQLite stores booleans as integers (0/1); `Motors.GetBool` handles this.
- `Session.Username` is a static string set at login and read by `Motors` to scope rental operations.
- The LoginForm background is the `motor_rental_logo` embedded resource — **do not change `LoginForm.Designer.cs`** to preserve the logo.
- Motor images are stored as loose files under `Resources/` and referenced at runtime (not embedded resources).

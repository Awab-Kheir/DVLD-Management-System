# Driving & Vehicle License Department (DVLD) Management System

A desktop-based Driving & Vehicle License Department management system built with C#, .NET Framework 4.8, Windows Forms, ADO.NET, and Microsoft SQL Server.

The application manages people, system users, driving license applications, tests, drivers, local licenses, international licenses, renewals, replacements, detained licenses, and related licensing workflows.

## Features

- People management
- System user management
- Local driving license applications
- First-time driving license issuance
- Vision, written, and street test management
- Test appointment scheduling
- Retake test applications
- Driving license renewal
- Lost license replacement
- Damaged license replacement
- License detention and release
- International driving license issuance
- Driver management
- License history
- Application type management
- Test type management
- Login and user account management

## Architecture

The project follows a 3-Tier Architecture:

```text
Presentation Layer
        |
        v
Business Logic Layer
        |
        v
Data Access Layer
        |
        v
Microsoft SQL Server
```

The solution is divided into the following main projects:

```text
DVLD/
    Windows Forms presentation layer

DVLD_Business/
    Business logic layer

DVLD_DataAccess/
    ADO.NET data access layer

DVLDSetup/
    Visual Studio Setup Project

database/
    SQL Server database creation and demo data script

DVLD-People-Images/
    Runtime directory for person images
```

## Technologies

- C#
- .NET Framework 4.8
- Windows Forms
- ADO.NET
- Microsoft SQL Server
- 3-Tier Architecture
- Object-Oriented Programming (OOP)
- Visual Studio

## Requirements

To run the project, you need:

- Windows
- Visual Studio with .NET desktop development support
- .NET Framework 4.8
- Microsoft SQL Server
- SQL Server Management Studio (recommended)

The included database script was generated using SQL Server compatibility level 160.

## Database Setup

The repository includes a complete SQL script with the database schema and sanitized demo data:

```text
database/MyDVLD.sql
```

Open Microsoft SQL Server Management Studio and execute:

```text
database/MyDVLD.sql
```

The script creates the following database:

```text
MyDVLD
```

The included data is intended for demonstration and testing.

Person image paths have been removed from the published database script.

## Database Connection

The application reads its SQL Server connection string from the following environment variable:

```text
DVLD_CONNECTION_STRING
```

Example using Windows Authentication:

```powershell
[Environment]::SetEnvironmentVariable(
    "DVLD_CONNECTION_STRING",
    "Server=.;Database=MyDVLD;Trusted_Connection=True;TrustServerCertificate=True;",
    "User"
)
```

Example using SQL Server Authentication:

```powershell
[Environment]::SetEnvironmentVariable(
    "DVLD_CONNECTION_STRING",
    "Server=.;Database=MyDVLD;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;",
    "User"
)
```

Replace the values with your own SQL Server configuration.

After setting the environment variable, restart Visual Studio before running the application.

## Person Images

Person images are stored outside the database.

The application reads the image storage directory from:

```text
DVLD_IMAGES_PATH
```

You can use the included empty folder:

```text
DVLD-People-Images/
```

Example:

```powershell
[Environment]::SetEnvironmentVariable(
    "DVLD_IMAGES_PATH",
    "C:\Path\To\DVLD\DVLD-People-Images",
    "User"
)
```

Replace the path with the actual location of the folder on your computer.

Real person images are not included in this repository.

## Demo Login

A demo account is included in the database script:

```text
Username: demo_admin
Password: demo123
```

This account is intended only for local demonstration of the application.

## Running the Application

Open the Visual Studio solution:

```text
DVLD/DVLD.sln
```

Make sure that both environment variables are configured:

```text
DVLD_CONNECTION_STRING
DVLD_IMAGES_PATH
```

Then build and run the solution from Visual Studio.

## Project Structure

```text
.
├── DVLD/
├── DVLD_Business/
├── DVLD_DataAccess/
├── DVLDSetup/
├── DVLD-People-Images/
├── Icons/
├── database/
│   └── MyDVLD.sql
├── .gitignore
└── README.md
```

## Data Privacy

The database included in this repository contains sanitized demo data.

Local person image paths and real person images are excluded from the repository.

Build artifacts, database backups, local Visual Studio files, and machine-specific files are also excluded using `.gitignore`.

## Learning Context

This project was implemented independently based on the DVLD project requirements provided by Programming Advices.

Course material was used as a learning and reference resource while implementing and practicing concepts such as layered architecture, database access, business logic, validation, and Windows Forms development.

## Screenshots

Application screenshots will be added to this repository to demonstrate the main workflows and user interface.

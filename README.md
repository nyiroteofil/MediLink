# MediLink - Outpatient Clinic Management System

![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![MAUI](https://img.shields.io/badge/.NET_MAUI-Cross--Platform-blue)
![EF Core](https://img.shields.io/badge/EF_Core-TPT-green)
![JWT](https://img.shields.io/badge/Auth-JWT-orange)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-red)

A full-stack outpatient clinic management system built with ASP.NET Core 9 and .NET MAUI, 
featuring secure authentication, role-based access control, appointment management, 
and real-time doctor-patient messaging.

> Still under development as a BSc thesis project at the University of Pannonia Faculty of Information Technology, Veszprém.
> Supervisor: Süle Péter

---

 Screenshots
<img width="1920" height="1032" alt="Dashboard" src="https://github.com/user-attachments/assets/6b5bcc89-a145-4c44-87fb-f4bcf9d4e2ad" /><br>
##<img width="1920" height="1080" alt="User Schedule Editor" src="https://github.com/user-attachments/assets/2856b571-2f7e-48c5-8e5a-7a5279d04c57" /><br>
<img width="1920" height="1032" alt="Administrator Page" src="https://github.com/user-attachments/assets/acbf7a9e-e9c1-4e26-a48a-492a55ef08b0" />



---

## Features

###  Security & Authentication
- **PBKDF2 password hashing** with cryptographically random salt (128-bit) 
  using `Microsoft.AspNetCore.Cryptography.KeyDerivation`
- **JWT authentication** with HMAC-SHA512 signed tokens and claims-based identity
- **Role-based authorization** via `[Authorize(Roles = "...")]` — 
  Patients, Doctors, Assistants and Administrators each have different access levels
- **Secure token storage** on the client using platform-native `SecureStorage` 
  (Windows Credential Manager / Android Keystore / iOS Keychain)
- **Client-side token expiry check** — expired tokens are detected locally 
  without an extra server round-trip
- Protection against **over-posting attacks** via DTOs on all endpoints

### User Management
- Role-based user hierarchy: `Patient`, `SpecialistDoctor`, 
  `MedicalAssistant`, `Administrator`
- Administrator panel for creating, suspending and reactivating user accounts
- Automatic seeding of the first administrator account on first launch
- Account status enforcement at login — suspended accounts are rejected with a meaningful error

### Appointment System
- Patients can request appointments with a reason of visit
- Doctors can view, accept or deny pending requests
- Accepted requests are promoted to full `Appointment` events
- Appointment history visible to both doctor and patient

### Messaging
- Direct doctor <-> patient messaging with conversation threads
- WhatsApp-style chat UI with sent/received bubble differentiation
- Messages stored persistently in the database
- Conversations tied to a `Chat` entity linking doctor and patient

### Dashboard
- Role-aware dashboard — doctors and patients see different widgets
- Doctor widgets: pending requests count, appointments this week, active patient count
- Today's events list
- Personalized greeting using name extracted from JWT claims

### Schedule
- Calendar view using Syncfusion `SfCalendar`
- Events filtered by selected date
- Supports appointments, medication reminders and custom events

### Admin Panel
- Tabbed interface: Add User / Active Users / Suspended Users
- Role filter (Patient, Doctor, Assistant, Admin) within each tab
- One-click suspend / reactivate with confirmation dialog
- Create any user type directly from the admin panel

---

## Architecture

### Backend — ASP.NET Core 9
- **RESTful API** with controller/service/data separation
- **Entity Framework Core** with **Table Per Type (TPT)** inheritance strategy
  for both the `User` and `Event` hierarchies
- **Fluent API** configuration for complex relationships, 
  cascade delete behavior and disambiguation of FK ambiguities
- **DTOs** on all endpoints to prevent over-posting and circular reference issues
- `ReferenceHandler.IgnoreCycles` configured globally for JSON serialization

### Frontend - .NET MAUI
- **Shell navigation** with flyout menu
- **Role-based menu visibility** — menu items shown/hidden based on JWT role claim
- **ApiHandler** singleton managing `HttpClient` lifetime, 
  token attachment and secure storage
- Direct property assignment pattern for UI updates (no MVVM toolkit dependency)
- Syncfusion component library for calendar and UI controls

### Database - SQL Server
- TPT inheritance produces clean, normalized tables per entity type
- Junction tables for N:M relationships (Doctor↔Patient, Doctor↔Assistant, 
  Medication↔Ingredient)
- Explicit FK properties on all 1:N relationships to avoid EF Core shadow property ambiguity

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core 9 |
| ORM | Entity Framework Core 9 (TPT) |
| Database | SQL Server |
| Frontend | .NET MAUI |
| Authentication | JWT + PBKDF2 |
| UI Components | Syncfusion MAUI Toolkit |
| IDE | Visual Studio 2022, Microsoft SQL Server Management Studio 2022 |

---

## Prerequisites

- Visual Studio 2022 or later (with .NET MAUI module)
- .NET 9 SDK
- SQL Server (local or remote)
- Syncfusion license (community license available free)

---

## Setup & Installation

### 1. Clone the repository
```bash
git clone https://github.com/nyiroteofil/MediLink.git
cd ./MediLink
```
### 2. Set SQL server connection string

Update `appsettings.json` with your SQL Server connection string:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MediLinkDB;
    Trusted_Connection=yes;TrustServerCertificate=True"
}
```

### 3. Run database migrations
```bash
cd MediLink_BackEnd
dotnet ef database update
```

### 4. Run the backend
```bash
dotnet run --project MediLink_BackEnd
```
The server will create and seed the default administrator account:
- **Username:** `admin`
- **Password:** `Admin123`
> ⚠️ Change the default admin password immediately after first login.

The API will be available at `https://localhost:7056`
Swagger UI: `https://localhost:7056/swagger`

### 5. Configure the frontend
In `MauiProgram.cs` set the API base URL:
```csharp
#if DEBUG
    Environment.SetEnvironmentVariable("MEDILINK_URL", "https://localhost:7056/api");
#endif
```

### 6. Run the frontend
Open `MediLine_FrontEnd.sln` in Visual Studio and run the Windows target.

---

## Known Limitations & Planned Improvements

These are known issues acknowledged for future development:

- **JWT refresh tokens** not yet implemented — users must re-login after token expiry (15 min)
- **Real-time messaging** planned — currently requires manual page refresh
- **Appointment acceptance flow** partially implemented — 
  status update works but full `Appointment` event creation pending
- **Chat contact name** display issue under investigation

---

## 👤 Author

**Teofil Nyirő**<br>
BSc Computer Science<br>
University of Pannonia, Veszprém<br>
Supervisor: Péter Süle

---

## 📄 License

This project is for academic and portfolio purposes.

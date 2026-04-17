# 🛠️ Service Booking Platform API

A RESTful Web API built with **ASP.NET Core 10** for managing service bookings between customers and service providers. The API supports role-based access control, JWT authentication, and full CRUD operations across all resources.

---

## ✨ Features

- User registration and login with **JWT authentication**
- **Role-based authorization** across all endpoints (Admin, ServiceProvider, User)
- CRUD management for Users, Service Providers, Services, and Bookings
- **Booking status history** tracking per booking
- **Review system** linked one-to-one with completed bookings
- Service categorization and image support
- Password hashing via ASP.NET Core Identity's `IPasswordHasher`
- Interactive API docs via **Scalar** (OpenAPI)
- Database migrations managed with **Entity Framework Core**

---

## 🏗️ Architecture

The project follows a clean, layered architecture with clear separation of concerns:

```
ServiceBookingPlatformApi/
├── Controllers/         # API endpoints (Auth, Users, ServiceProviders, Services, Bookings)
├── DTOs/                # Input/output data transfer objects per resource
├── Entities/            # EF Core domain models (Users, Services, Bookings)
├── Domain/Enums/        # Shared enumerations (RoleType)
├── Data/                # AppDbContext and EF model configuration
├── Repositories/
│   ├── Interfaces/      # IGenericRepository<T>, IAuthService
│   ├── Implementations/ # GenericRepository<T>, AuthService
│   └── UnitOfWork.cs    # Coordinates repository access and saves
├── Mappings/            # AutoMapper profiles per resource
└── Migrations/          # EF Core database migrations
```

The data access layer uses a **Generic Repository + Unit of Work** pattern, keeping controllers lean and persistence logic consistent across all entity types.

---

## 🧱 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 (.NET 10) |
| ORM | Entity Framework Core 10 |
| Database | SQL Server (via `Microsoft.EntityFrameworkCore.SqlServer`) |
| Authentication | JWT Bearer (`Microsoft.AspNetCore.Authentication.JwtBearer`) |
| Password Hashing | ASP.NET Core Identity `IPasswordHasher<T>` |
| Object Mapping | AutoMapper 16 |
| API Documentation | Scalar + Microsoft.AspNetCore.OpenApi |

---

## 👥 Roles

| Role | Value | Description |
|---|---|---|
| `Admin` | 1 | Full access to all endpoints |
| `ServiceProvider` | 2 | Can manage and view their own services |
| `User` | 3 | Can create and manage their own bookings |

---

## 📡 API Endpoints

### Auth — `api/auth`
| Method | Endpoint | Access | Description |
|---|---|---|---|
| POST | `/register` | Public | Register a new user |
| POST | `/login` | Public | Login and receive a JWT token |

### Users — `api/users`
| Method | Endpoint | Access | Description |
|---|---|---|---|
| GET | `/` | Admin | Get all users |
| GET | `/{id}` | Admin, User (own) | Get user by ID |
| POST | `/` | Public | Create a user |
| PUT | `/{id}` | Admin, User (own) | Update a user |
| DELETE | `/{id}` | Admin | Delete a user |

### Service Providers — `api/serviceproviders`
| Method | Endpoint | Access | Description |
|---|---|---|---|
| GET | `/` | Admin | Get all service providers |
| GET | `/{id}` | Admin | Get provider by ID |
| POST | `/` | Admin | Create a service provider profile |
| PUT | `/{id}` | Admin | Update a service provider |
| DELETE | `/{id}` | Admin | Delete a service provider |

### Services — `api/services`
| Method | Endpoint | Access | Description |
|---|---|---|---|
| GET | `/` | Admin, ServiceProvider | Get all services |
| GET | `/{id}` | Admin | Get service by ID |
| POST | `/` | Admin, ServiceProvider | Create a service |
| PUT | `/{id}` | Admin, ServiceProvider | Update a service |
| DELETE | `/{id}` | Admin, ServiceProvider | Delete a service |

### Bookings — `api/bookings`
| Method | Endpoint | Access | Description |
|---|---|---|---|
| GET | `/` | Admin, User | Get all bookings |
| GET | `/{id}` | Admin, User | Get booking by ID |
| POST | `/` | Admin, User | Create a booking |
| PUT | `/{id}` | Admin, User | Update a booking |
| DELETE | `/{id}` | Admin, User | Cancel/delete a booking |

---

## 🗄️ Data Model

```
User
  Id, Username, Email, PasswordHash, PhoneNumber, Role, CreatedAt
  └── ServiceProvider (optional, 1-to-1)
        Id, UserId, BusinessName, Description, Address, PhoneNumber

ServiceCategory
  Id, Name, Description
  └── Services[]

Service
  Id, Name, Description, Price, Duration, CategoryId, ServiceProviderId
  └── Images[] (ServiceImage)

Booking
  Id, ServiceId, UserId, ProviderId, Status, BookingDate, CreatedAt
  └── StatusHistory[] (BookingStatus)
  └── Review (optional, 1-to-1)

Review
  Id, BookingId, UserId, Rating, Comment, CreatedAt

BookingStatus
  Id, BookingId, Status, ChangedAt
```

---

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server (or SQL Server Express)

### Setup

1. **Clone the repo**
   ```bash
   git clone https://github.com/RamiAsad1/ServiceBookingPlatform.git
   cd ServiceBookingPlatform/ServiceBookingPlatformApi
   ```

2. **Configure the connection string**

   Update `appsettings.json` with your SQL Server connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=ServiceBookingDb;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Configure JWT settings**

   Also in `appsettings.json`, set a strong secret key:
   ```json
   {
     "Jwt": {
       "Key": "YourLongAndSecureSecretKeyHere",
       "Issuer": "ServiceBookingApi",
       "Audience": "ServiceBookingApiClients",
       "ExpireMinutes": 60
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the API**
   ```bash
   dotnet run
   ```

6. **Explore the API docs**

   Navigate to `https://localhost:{port}/scalar` in your browser to access the interactive Scalar API reference.

---

## 🔐 Authentication

All protected endpoints require a `Bearer` token in the `Authorization` header:

```
Authorization: Bearer <your_jwt_token>
```

Obtain a token by calling `POST /api/auth/login` with a valid email and password. The token includes the user's role as a claim, which is used to enforce endpoint-level authorization.

---

## 📄 License

This project is for portfolio and learning purposes.

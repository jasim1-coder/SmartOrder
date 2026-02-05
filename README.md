# SmartOrder – Clean Architecture Demo

> An ASP.NET Core API demonstrating **Clean Architecture**, **Domain-Driven Design**, and **SOLID principles** for learning and portfolio purposes.

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-blue)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![DDD](https://img.shields.io/badge/Design-DDD-green)](https://martinfowler.com/tags/domain%20driven%20design.html)

---

## 📋 Overview

SmartOrder is a backend project designed to demonstrate **Clean Architecture and Domain-Driven Design principles** commonly discussed in backend developer interviews. This project focuses on code organization, domain modeling, and business logic separation rather than being a complete application.

**Purpose:** Portfolio demonstration, architecture discussions, and code review material.

---

## ✨ Key Features

### Architecture Patterns Demonstrated
- ✅ **Rich domain models** with encapsulated business logic
- ✅ **Aggregate boundaries** to prevent tight coupling
- ✅ **Layered architecture** with clear separation of concerns
- ✅ **Business rule enforcement** in appropriate layers
- ✅ **Authorization patterns** for ownership-based access control

---

## 🏛️ Architecture

```
┌─────────────────────────────────────────┐
│           API Layer                     │
│  (Controllers, Middleware, DTOs)        │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│       Application Layer                 │
│  (Use Cases, Validation, Auth)          │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│         Domain Layer                    │
│  (Entities, Aggregates, Rules)          │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│      Infrastructure Layer               │
│  (EF Core, Repositories, Data)          │
└─────────────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Responsibility | Dependencies |
|-------|---------------|--------------|
| **Domain** | Core business logic, aggregates, value objects | None (pure C#) |
| **Application** | Use case orchestration, cross-aggregate validation | Domain only |
| **Infrastructure** | Database, external services, persistence | Domain & Application |
| **API** | HTTP concerns, request/response mapping | Application only |

---

## 🎯 Key Design Decisions

### Domain-Driven Design in Action

#### Aggregates & Boundaries
```
Order (Aggregate Root)
├── OrderItems (Owned Entities)
├── Customer (Reference by ID)
└── Product (Reference by ID)

Customer (Aggregate Root)
├── Account Status
└── Eligibility Rules

Product (Aggregate Root)
├── Pricing
└── Availability Status
```

**Why this matters:** 
- Prevents N+1 queries and circular dependencies
- Each aggregate can be tested in isolation
- Changes to one aggregate don't cascade unexpectedly

#### Business Rules Enforcement

**Example: Order Creation**
```csharp
// ❌ Wrong: Business logic in controller
// ✅ Right: Business logic in domain, coordination in application layer

Application Layer:
- Checks customer eligibility
- Verifies product availability
- Validates ownership

Domain Layer:
- Enforces order state transitions
- Maintains invariants
- Calculates totals
```

---

## 💼 Business Domain

SmartOrder models an e-commerce order management system with the following rules:

### Core Features
- **Customer Management:** Account status, eligibility checks
- **Product Catalog:** Pricing, inventory, active status
- **Order Processing:** Creation, payment, cancellation workflows

### Business Constraints
| Rule | Implementation Layer |
|------|---------------------|
| Blocked customers cannot create orders | Application |
| Inactive products cannot be added | Application |
| Only order owner can modify it | Application |
| Order total must match item sum | Domain |
| Paid orders cannot be cancelled | Domain |
| Items cannot be added after payment | Domain |

---

## 🧪 Testing Strategy

```
Domain Tests (Unit)
├── State transitions
├── Business invariants
├── Aggregate behavior
└── Value object validation

```

**No mocks in domain tests** – Pure business logic verification

---

## 🛠️ Technology Stack

| Category | Technology |
|----------|-----------|
| Framework | ASP.NET Core 8.0 |
| Language | C# 12 |
| ORM | Entity Framework Core |
| Testing | xUnit |
| Patterns | Clean Architecture, DDD |

---

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full instance)

### Quick Start
```bash
# Clone the repository
git clone https://github.com/jasim1-coder/smartorder.git

# Navigate to project
cd smartorder

# Restore dependencies
dotnet restore

# Apply migrations
dotnet ef database update --project src/Infrastructure

# Run the API
dotnet run --project src/API

# Run tests
dotnet test
```

---

## 📁 Project Structure

```
SmartOrder/
├── src/
│   ├── Domain/              # Core business logic
│   │   ├── Aggregates/
│   │   ├── ValueObjects/
│   │   └── Interfaces/
│   ├── Application/         # Use cases
│   │   ├── Orders/
│   │   ├── Products/
│   │   └── Customers/
│   ├── Infrastructure/      # Data access
│   │   ├── Persistence/
│   │   └── Repositories/
│   └── API/                 # HTTP layer
│       └── Controllers/
└── tests/
    ├── Domain.Tests/
    ├── Application.Tests/
    └── API.Tests/
```

## 📝 Project Scope

### What This Project Demonstrates
✅ Clean Architecture layer organization  
✅ Domain-Driven Design tactical patterns  
✅ Business logic separation from infrastructure  
✅ Unit testing of domain logic  

### What This Project Doesn't Include
❌ Authentication or user management  
❌ Production deployment configuration  
❌ Complete e-commerce features  
❌ UI or frontend components  

**Note:** This is a learning and demonstration project, not a production-ready application. It focuses on architecture and design patterns rather than feature completeness.

---

## 📫 Contact

**Muhammed Jasim**  
[LinkedIn](https://www.linkedin.com/in/muhd-jasim-t/) | [GitHub](https://github.com/jasim1-coder) 

---




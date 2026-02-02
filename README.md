# SmartOrder

SmartOrder is an enterprise-style backend system built to demonstrate
**Clean Architecture, SOLID principles, and Domain-Driven Design (DDD)**
using **ASP.NET Core and Entity Framework Core**.

## 🎯 Purpose
This project is intentionally designed to go beyond simple CRUD
and focuses on:
- Rich domain models
- Explicit business rules
- Testable architecture
- Long-term maintainability

## 🏗️ Architecture
The solution follows Clean Architecture:

- **Domain** – Core business rules (no framework dependencies)
- **Application** – Use cases and orchestration
- **Infrastructure** – EF Core, database access (to be added)
- **API** – Thin HTTP layer

## 🧠 Domain-Driven Design
- Aggregates: `Order`
- Value Objects: `Money`
- Business invariants enforced in domain
- No anemic models

## 🧪 Testing
- Domain rules tested using xUnit
- No database dependency in core tests

## 🚀 Tech Stack
- ASP.NET Core
- EF Core
- C#
- xUnit
- Clean Architecture
- DDD-lite

## 📌 Note
This project is built to reflect how real enterprise systems
are structured, not as a tutorial or demo.

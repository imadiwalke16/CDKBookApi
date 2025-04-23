Service Book App – Backend
A robust backend API for the Service Book App, built to streamline vehicle service management including appointments, service history, and real-time notifications. The backend is designed with scalability, maintainability, and testability in mind using Clean Architecture and TDD practices.

Frontend repo could be found here -
https://github.com/imadiwalke16/AP

🚀 Tech Stack
Framework: .NET Web API

Architecture: Clean Architecture

Language: C#

Database: PostgreSQL (EF Core Code-First)

Authentication: JWT Bearer Tokens

Real-Time Communication: SignalR

Testing: xUnit + Moq

Documentation: Swagger / OpenAPI

🧱 Project Structure

CDKBOOKAPI/
│
├── Application/           # Use cases, DTOs, interfaces  # Domain models, enums, core business logic
├── Infrastructure/        
│   └── Data/              # AppDbContext, EF configurations
│   └── Repositories/      # Data access implementations
├── WebAPI/                # Controllers, Middleware, Program.cs
├── Tests/                 # Unit and integration tests

🧭 Development Approach
🔄 Clean Architecture
The backend follows the principles of Clean Architecture, ensuring separation of concerns:

🧪 Test-Driven Development (TDD)
Unit tests are written before business logic.

All critical business rules are covered with tests.

We use xUnit for test orchestration and Moq for mocking dependencies.

🛠️ CI-Ready Practices
Code is modular and testable.

Code formatting, linting, and consistent naming conventions are enforced.

📡 API Overview
Authentication
JWT-based login

Role-based access control (planned)

Core Features
Vehicles: Register, update, delete, and fetch user vehicles

Appointments: Book service slots with preferred date/time

Service History: View previous services

Notifications:

Real-time via SignalR

Promotional and service-status messages

Mark as read, pull-to-refresh

📂 Database Schema
Code-First EF Core

PostgreSQL for relational data

Full migrations tracking with rollback safety

🔄 Real-Time Notifications
Using SignalR, users receive:

Service status updates

Promotional offers

Alerts on appointment changes

Frontend fetches and displays notifications using:

🚧 Setup & Installation
Prerequisites
.NET SDK

PostgreSQL 

# Clone repo
git clone "project-url"
cd project-name

# Apply EF migrations
dotnet ef database update

# Run the app
dotnet run --project WebAPI
Swagger Docs
Access interactive API documentation at:
tbd

🧪 Running Tests
dotnet test

Tests include:

Unit Tests (Application)

Integration Tests (Repositories, Controllers)

📌 Future Enhancements
CI/CD via GitHub Actions

Role-based access and admin panel

Multi-language support

🤝 Contributing
We follow a review-first Git process. All PRs must:

Pass all tests

Be reviewed by a lead developer

Have clear commit messages

📫 Contact & Support
For questions, suggestions, or contributions, open an issue or contact the maintainer.
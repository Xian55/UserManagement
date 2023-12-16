# API for Managing Users

**API for managing users has been developed utilizing .NET8**

## Utilized Technologies

- .NET 8
- SQLite (Operational in Memory)
- Core Entity Framework 8.0
- Dapper
- Dynamic LINQ
- MediatR
- FluentValidation
- Swagger

## Design Pattern

- The API employs the Command Query Responsibility Segregation (CQRS) design pattern. This approach separates read and write operations for more efficient and scalable data handling, enhancing the system's overall performance and maintainability.

## Architectural Design of the System

The design of the system follows a layered approach, reminiscent of an onion. This structure comprises several strata, where each stratum is autonomous and relies only on itself and the strata beneath it in the hierarchy.

### Domain Layer

Encompasses all the essential entities and business logic pertinent to the domain of user management. It's the core layer that defines the fundamental operational rules and data structures.

### Application Layer

The focus is on orchestrating domain logic and serving as the conduit between the user interface and the domain. This layer handles application-specific logic and coordinates tasks.

### Infrastructure Layer

Supports the other layers by providing external capabilities like database access, file systems, and network interfaces. It essentially bridges the gap between the application's core functionalities and external resources.

## Key Features

- Implementation using cutting-edge .NET8 framework.
- Utilization of an in-memory SQLite database for efficient data handling.
- Integration of Entity Framework Core 8.0 for robust database operations.
- Dapper for simplified database interactions.
- Dynamic LINQ for flexible query operations.
- MediatR for effective mediation pattern implementation.
- FluentValidation for advanced data validation.
- Swagger for interactive API documentation and easier testing.
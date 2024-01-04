# API for Managing Users

**API for managing users has been developed utilizing .NET7**

## Utilized Technologies

- [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [MongoDB Driver](https://github.com/mongodb/mongo-csharp-driver)
- MongoDB with [EF Core Provider](https://github.com/mongodb/mongo-efcore-provider)
- [Core Entity Framework 7.0](https://github.com/dotnet/efcore)
- [Dynamic LINQ](https://github.com/zzzprojects/System.Linq.Dynamic.Core)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- Containerization with Docker
- Self hosted MongoDB
- [xUnit](https://github.com/xunit/xunit)
- [Moq](https://github.com/devlooped/moq)
- [Moq.EntityFrameworkCore](https://github.com/MichalJankowskii/Moq.EntityFrameworkCore)
- [FluentAssertion](https://github.com/fluentassertions/fluentassertions)
- [Github workflow](https://resources.github.com/ci-cd/)

## Design Pattern

- The API employs the Command Query Responsibility Segregation ([CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)) design pattern. This approach separates read and write operations for more efficient and scalable data handling, enhancing the system's overall performance and maintainability.

## Architectural Design of the System

- The design of the system follows a layered approach, reminiscent of an onion. This structure comprises several strata, where each stratum is autonomous and relies only on itself and the strata beneath it in the hierarchy.

### Domain Layer

- Encompasses all the essential entities and business logic pertinent to the domain of user management. It's the core layer that defines the fundamental operational rules and data structures.

### Application Layer

- The focus is on orchestrating domain logic and serving as the conduit between the user interface and the domain. This layer handles application-specific logic and coordinates tasks.

### Infrastructure Layer

- Supports the other layers by providing external capabilities like database access, file systems, and network interfaces. It essentially bridges the gap between the application's core functionalities and external resources.

### Persistance Layer
- It is responsible for storing and retrieving data from our database, ensuring that data remains consistent and is available whenever needed.
- Implemented using EntityFramework with MongoDB, which provides a robust and efficient way to interact with our database. It abstracts the complexities of raw database queries and allows us to interact with our data in a more intuitive and object-oriented way.

## Key Features

- Implementation using cutting-edge .NET7 framework.
- Utilization of an NoSQL MongoDB database for efficient data handling.
- Integration of Entity Framework Core 7.0 for robust database operations.
- Dynamic LINQ for flexible query operations.
- MediatR for effective mediation pattern implementation.
- FluentValidation for advanced data validation.
- Swagger for interactive API documentation and easier testing.
- CRUD operations on **User** model, where using `PUT` method to update/replace the **User** model.
- On startup in case the database has not yet been initialized, the models going to be created. On the other hand seeds the database with example data.
- The REST.API utilizes api versioning.

## Run the app
```
docker compose build
docker compose up -d
docker compose down
```

## Future improvements

- Integrate [caching](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-7.0) solution
- Integrate Heath Checks via [IHealthCheck](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.diagnostics.healthchecks.ihealthcheck?view=dotnet-plat-ext-8.0)
# EF Core Commands for VS Code and .NET CLI
Visual Studio Code and .NET CLI
  dotnet tool install --global dotnet-ef
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet ef migrations add InitialCreate
  dotnet ef database update

# Clean Architecture
- Domain
- Application
- Infrastructure
- Presentation

Domain (Core Layer)
    => Application (Core Layer)
        => Infrastructure (Periphery Layer)
            => Presentation (Periphery Layer)
        => Presentation (Periphery Layer)

# Domain event
- Event triggered when an event occur in a domain and has to be consumed within the same bounded context
- Is consumed by the in-memory message bus
- Is consumed synchronously

# Ingretaion
- Event triggered when an event occur in a domain and has to be consumed by external microservices
- Is consumed by centralized message bus such as rabbitmq
- Is consumed asynchrously



# EF Core Commands for VS Code and .NET CLI
Visual Studio Code and .NET CLI
  dotnet tool install --global dotnet-ef
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet ef migrations add InitialCreate
  dotnet ef database update

# Run EF Migration using cli
dotnet ef migrations add InitialPostgresCreate -o Data/Migrations -p Ordering.Infrastructure.csproj -s ../OrderingAPI/OrderingAPI.csproj
# Run EF Update using cli
dotnet ef database update --project Ordering.Infrastructure.csproj --startup-project ../OrderingAPI/OrderingAPI.csproj   

#
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



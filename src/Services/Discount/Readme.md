EF Core Commands for VS and .NET CLI
Visual Studio Code and .NET CLI
  dotnet tool install --global dotnet-ef
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet ef migrations add InitialCreate
  dotnet ef database update
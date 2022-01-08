
dotnet-6-registration-login-api
https://github.com/cornflourblue/dotnet-6-registration-login-api


dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0

Add migrations
dotnet ef migrations add firstmigration --context SqliteDataContext --project UserAPI.csproj
dotnet ef migrations add firstpsqlmigration --context DataContext --project UserAPI.csproj

dotnet ef migrations list --context DataContext --project UserAPI.csproj

dotnet ef database update firstpsqlmigration --context DataContext --project UserAPI.csproj


Implementing RESTful Microservice using ASP.NET Core Minimal web API with CRUD on PostgreSQL
https://www.tutlinks.com/minimal-web-api-with-crud-on-postgresql-dotnet-6/

Use multiple environments in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-6.0
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_ENVIRONMENT=Production


dotnet run --environment "Staging"
dotnet run --environment "Production"
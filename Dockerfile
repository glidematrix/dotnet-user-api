FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish -o /app/published-app


FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/UserAPI.dll" ]

# docker build . -t userapi
# docker run --env-file .env --rm --name userapi -p 8081:4000 userapi

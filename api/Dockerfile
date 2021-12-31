# The build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app --configuration Release

# The runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

# The value 'container' is used in Program.cs to set the URL for Google Cloud Run
ENV ASPNETCORE_ENVIRONMENT=container
ENTRYPOINT [ "dotnet", "/app/api.dll" ]
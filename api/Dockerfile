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

# Environment variables for the connection string
# See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0#evcp
# By default uses the localhost
# See: https://cloud.google.com/run/docs/configuring/environment-variables
# ENV MongoDbConnectionSettings_ConnectionString=mongodb://localhost:27017
ENTRYPOINT [ "dotnet", "/app/Api.dll" ]
# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app

# Copy the project files into the container
COPY . .

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use a smaller runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose port 5358 for the application
EXPOSE 5302

# Set the entry point for the application
ENTRYPOINT ["dotnet", "DictionaryGrpcService.dll"]
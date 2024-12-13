# Step 1: Build the application using the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container for the build stage
WORKDIR /app

# Copy the solution file and project files for all necessary projects to restore dependencies
COPY ERP-PROJET.sln ./
COPY src/API/API.csproj src/API/
COPY src/Application/Application.csproj src/Application/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/

# Restore the dependencies defined in the solution file
RUN dotnet restore ERP-PROJET.sln

# Step 2: Copy the entire source code to the container (needed for building the app)
COPY . ./

# Build and publish the API project in Release mode to the /app/publish directory
RUN dotnet publish src/API/API.csproj -c Release -o /app/publish

# Step 3: Use the official .NET Runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory inside the container for the runtime stage
WORKDIR /app

# Copy the built and published app from the previous build stage
COPY --from=build /app/publish .

# Expose port 8080 to access the app externally
EXPOSE 8080

# Set the entry point for running the API project
ENTRYPOINT ["dotnet", "API.dll"]

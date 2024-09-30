# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["RssQueueConsumer.csproj", "."]
RUN dotnet restore "RssQueueConsumer.csproj"

# Copy the rest of the application code
COPY . .

# Create the build output directory
RUN mkdir -p /app/build

# Build the project with verbose logging for diagnostics
RUN dotnet build "RssQueueConsumer.csproj" -c Release -o /app/build -v diag

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "RssQueueConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RssQueueConsumer.dll"]

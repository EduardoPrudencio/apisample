# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the entire project directory
COPY . . 

# Restore dependencies
RUN dotnet restore "RssQueueConsumer.csproj"

# Build the project
RUN dotnet build "RssQueueConsumer.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "RssQueueConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RssQueueConsumer.dll"]

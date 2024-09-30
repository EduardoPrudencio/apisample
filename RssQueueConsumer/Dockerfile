# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the entire project and restore dependencies
COPY ["RssQueueConsumer/RssQueueConsumer.csproj", "RssQueueConsumer/"]
RUN dotnet restore "RssQueueConsumer/RssQueueConsumer.csproj"

# Copy the remaining files and build the project
COPY ../RssQueueConsumer ./RssQueueConsumer
WORKDIR "/src/RssQueueConsumer"
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RssQueueConsumer.dll"]

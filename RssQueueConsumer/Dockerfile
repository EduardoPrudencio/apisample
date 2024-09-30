#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RssQueueConsumer.csproj", "RssQueueConsumer/"]
RUN dotnet restore "RssQueueConsumer/RssQueueConsumer.csproj"
COPY . .
WORKDIR "/src/RssQueueConsumer"
RUN dotnet build "/src/RssQueueConsumer/RssQueueConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/RssQueueConsumer/RssQueueConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RssQueueConsumer.dll"]

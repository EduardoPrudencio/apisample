
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RssReaderContainer/RssReaderContainer.csproj", "RssReaderContainer/"]
COPY ["ApiSample.Domain/ApiSample.Domain.csproj", "ApiSample.Domain/"]
COPY ["RssReader/RssReader.csproj", "RssReader/"]
COPY ["RabbitMQManager/RabbitMQManager.csproj", "RabbitMQManager/"]
RUN dotnet restore "./RssReaderContainer/RssReaderContainer.csproj"
COPY . .
WORKDIR "/src/RssReaderContainer"
RUN dotnet build "RssReaderContainer.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RssReaderContainer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RssReaderContainer.dll"]

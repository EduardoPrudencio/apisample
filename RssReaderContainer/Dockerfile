#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RssReaderContainer/RssReaderContainer.csproj", "RssReaderContainer/"]
COPY ["RssReader/RssReader.csproj", "RssReader/"]
RUN dotnet restore "RssReaderContainer/RssReaderContainer.csproj"
COPY . .
WORKDIR "/src/RssReaderContainer"
RUN dotnet build "RssReaderContainer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RssReaderContainer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RssReaderContainer.dll"]
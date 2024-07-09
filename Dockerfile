FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PortfolioService/Consumers/API/API.csproj", "PortfolioService/Consumers/API/"]
COPY ["PortfolioService/Adpters/Data/Data.csproj", "PortfolioService/Adpters/Data/"]
COPY ["PortfolioService/Core/Domain/Domain.csproj", "PortfolioService/Core/Domain/"]
COPY ["PortfolioService/Core/Application/Application.csproj", "PortfolioService/Core/Application/"]
RUN dotnet restore "./PortfolioService/Consumers/API/API.csproj"
COPY . .
WORKDIR "/src/PortfolioService/Consumers/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
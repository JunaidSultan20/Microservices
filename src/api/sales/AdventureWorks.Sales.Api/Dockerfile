#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/api/sales/AdventureWorks.Sales.Api/AdventureWorks.Sales.Api.csproj", "src/api/sales/AdventureWorks.Sales.Api/"]
COPY ["src/common/AdventureWorks.Common/AdventureWorks.Common.csproj", "src/common/AdventureWorks.Common/"]
COPY ["src/common/AdventureWorks.Messaging/AdventureWorks.Messaging.csproj", "src/common/AdventureWorks.Messaging/"]
COPY ["src/common/AdventureWorks.Contracts/AdventureWorks.Contracts.csproj", "src/common/AdventureWorks.Contracts/"]
COPY ["src/common/AdventureWorks.Middlewares/AdventureWorks.Middlewares.csproj", "src/common/AdventureWorks.Middlewares/"]
COPY ["src/services/sales/AdventureWorks.Sales.Customers/AdventureWorks.Sales.Customers.csproj", "src/services/sales/AdventureWorks.Sales.Customers/"]
COPY ["src/services/sales/AdventureWorks.Sales.Domain/AdventureWorks.Sales.Domain.csproj", "src/services/sales/AdventureWorks.Sales.Domain/"]
COPY ["src/services/sales/AdventureWorks.Sales.Infrastructure/AdventureWorks.Sales.Infrastructure.csproj", "src/services/sales/AdventureWorks.Sales.Infrastructure/"]
RUN dotnet restore "src/api/sales/AdventureWorks.Sales.Api/AdventureWorks.Sales.Api.csproj"
COPY . .
WORKDIR "/src/src/api/sales/AdventureWorks.Sales.Api"
RUN dotnet build "AdventureWorks.Sales.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorks.Sales.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Sales.Api.dll"]
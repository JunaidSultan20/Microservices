#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdventureWorks.Sales.API/AdventureWorks.Sales.API.csproj", "AdventureWorks.Sales.API/"]
COPY ["Sales.Infrastructure/Sales.Infrastructure.csproj", "Sales.Infrastructure/"]
COPY ["Sales.Application/Sales.Application.csproj", "Sales.Application/"]
COPY ["AdventureWorks.Common/AdventureWorks.Common.csproj", "AdventureWorks.Common/"]
COPY ["Sales.Domain/Sales.Domain.csproj", "Sales.Domain/"]
RUN dotnet restore "AdventureWorks.Sales.API/AdventureWorks.Sales.API.csproj"
COPY . .
WORKDIR "/src/AdventureWorks.Sales.API"
RUN dotnet build "AdventureWorks.Sales.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorks.Sales.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Sales.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/api/gateway/AdventureWorks.Gateway.Api/AdventureWorks.Gateway.Api.csproj", "src/api/gateway/AdventureWorks.Gateway.Api/"]
COPY ["src/common/AdventureWorks.Common/AdventureWorks.Common.csproj", "src/common/AdventureWorks.Common/"]
RUN dotnet restore "src/api/gateway/AdventureWorks.Gateway.Api/AdventureWorks.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/src/api/gateway/AdventureWorks.Gateway.Api"
RUN dotnet build "AdventureWorks.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorks.Gateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Gateway.Api.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/api/identity/AdventureWorks.Identity.Api/AdventureWorks.Identity.Api.csproj", "src/api/identity/AdventureWorks.Identity.Api/"]
COPY ["src/services/identity/AdventureWorks.Identity.Application/AdventureWorks.Identity.Application.csproj", "src/services/identity/AdventureWorks.Identity.Application/"]
COPY ["src/common/AdventureWorks.Common/AdventureWorks.Common.csproj", "src/common/AdventureWorks.Common/"]
COPY ["src/services/identity/AdventureWorks.Identity.Domain/AdventureWorks.Identity.Domain.csproj", "src/services/identity/AdventureWorks.Identity.Domain/"]
COPY ["src/services/identity/AdventureWorks.Identity.Infrastructure/AdventureWorks.Identity.Infrastructure.csproj", "src/services/identity/AdventureWorks.Identity.Infrastructure/"]
RUN dotnet restore "src/api/identity/AdventureWorks.Identity.Api/AdventureWorks.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/api/identity/AdventureWorks.Identity.Api"
RUN dotnet build "AdventureWorks.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorks.Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Identity.Api.dll"]
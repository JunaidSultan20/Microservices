#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdventureWorks.Identity.API/AdventureWorks.Identity.API.csproj", "AdventureWorks.Identity.API/"]
COPY ["Identity.Application/Identity.Application.csproj", "Identity.Application/"]
COPY ["AdventureWorks.Common/AdventureWorks.Common.csproj", "AdventureWorks.Common/"]
COPY ["Identity.Domain/Identity.Domain.csproj", "Identity.Domain/"]
COPY ["Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity.Infrastructure/"]
RUN dotnet restore "AdventureWorks.Identity.API/AdventureWorks.Identity.API.csproj"
COPY . .
WORKDIR "/src/AdventureWorks.Identity.API"
RUN dotnet build "AdventureWorks.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorks.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Identity.API.dll"]
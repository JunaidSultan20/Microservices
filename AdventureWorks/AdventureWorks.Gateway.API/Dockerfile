#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 90
EXPOSE 543

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdventureWorks.Gateway.API/AdventureWorks.Gateway.API.csproj", "AdventureWorks.Gateway.API/"]
RUN dotnet restore "AdventureWorks.Gateway.API/AdventureWorks.Gateway.API.csproj"
COPY . .
WORKDIR "/src/AdventureWorks.Gateway.API"
RUN dotnet build "AdventureWorks.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventureWorks.Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Gateway.API.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NavigationModule.API/NavigationModule.API.csproj", "NavigationModule.API/"]
COPY ["NavigationModule.Authentication/NavigationModule.Authentication.csproj", "NavigationModule.Authentication/"]
RUN dotnet restore "NavigationModule.API/NavigationModule.API.csproj"
COPY . .
WORKDIR "/src/NavigationModule.API"
RUN dotnet build "NavigationModule.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NavigationModule.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NavigationModule.API.dll"]
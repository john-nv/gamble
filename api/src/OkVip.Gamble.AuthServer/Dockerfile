#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 44344

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OkVip.Gamble.AuthServer/OkVip.Gamble.AuthServer.csproj", "src/OkVip.Gamble.AuthServer/"]
COPY ["src/OkVip.Gamble.EntityFrameworkCore/OkVip.Gamble.EntityFrameworkCore.csproj", "src/OkVip.Gamble.EntityFrameworkCore/"]
COPY ["src/OkVip.Gamble.Domain/OkVip.Gamble.Domain.csproj", "src/OkVip.Gamble.Domain/"]
COPY ["src/OkVip.Gamble.Domain.Shared/OkVip.Gamble.Domain.Shared.csproj", "src/OkVip.Gamble.Domain.Shared/"]
COPY ["src/OkVip.Gamble.Extensions/OkVip.Gamble.Extensions.csproj", "src/OkVip.Gamble.Extensions/"]
RUN dotnet restore "./src/OkVip.Gamble.AuthServer/./OkVip.Gamble.AuthServer.csproj"
COPY . .
WORKDIR "/src/src/OkVip.Gamble.AuthServer"
RUN dotnet build "./OkVip.Gamble.AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OkVip.Gamble.AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OkVip.Gamble.AuthServer.dll", "--urls=http://0.0.0.0:44344"]
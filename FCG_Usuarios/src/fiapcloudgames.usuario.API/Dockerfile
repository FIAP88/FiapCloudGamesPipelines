# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/fiapcloudgames.usuario.API/fiapcloudgames.usuario.API.csproj", "src/fiapcloudgames.usuario.API/"]
COPY ["src/fiapcloudgames.usuario.Application/fiapcloudgames.usuario.Application.csproj", "src/fiapcloudgames.usuario.Application/"]
COPY ["src/fiapcloudgames.usuario.Domain/fiapcloudgames.usuario.Domain.csproj", "src/fiapcloudgames.usuario.Domain/"]
COPY ["src/fiapcloudgames.usuario.Shared/fiapcloudgames.usuario.Shared.csproj", "src/fiapcloudgames.usuario.Shared/"]
COPY ["src/fiapcloudgames.usuario.Ioc/fiapcloudgames.usuario.Ioc.csproj", "src/fiapcloudgames.usuario.Ioc/"]
COPY ["src/fiapcloudgames.usuario.Infrastructure/fiapcloudgames.usuario.Infrastructure.csproj", "src/fiapcloudgames.usuario.Infrastructure/"]
RUN dotnet restore "./src/fiapcloudgames.usuario.API/fiapcloudgames.usuario.API.csproj"
COPY . .
WORKDIR "/src/src/fiapcloudgames.usuario.API"
RUN dotnet build "./fiapcloudgames.usuario.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./fiapcloudgames.usuario.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fiapcloudgames.usuario.API.dll"]
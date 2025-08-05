# Etapa base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
 
# NENHUMA CONFIGURA��O DO DATADOG AQUI.
# O Azure ir� injetar o tracer automaticamente.
 
# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
# Copia apenas os arquivos de projeto para aproveitar cache
COPY FiapCloudGamesAPI/*.csproj FiapCloudGamesAPI/
RUN dotnet restore FiapCloudGamesAPI/FiapCloudGamesAPI.csproj
 
# Copia o restante do c�digo
COPY . .
 
# Publica o projeto
RUN dotnet publish FiapCloudGamesAPI/FiapCloudGamesAPI.csproj -c Release -o /app/publish
 
# Etapa final de runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
 
ENTRYPOINT ["dotnet", "FiapCloudGamesAPI.dll"]
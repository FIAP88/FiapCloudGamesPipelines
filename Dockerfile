# Etapa base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ===== Adiciona Datadog Tracer =====
# Baixa e instala o tracer para Linux (use sempre a versão estável mais recente)
ADD https://github.com/DataDog/dd-trace-dotnet/releases/download/v2.46.0/datadog-dotnet-apm-linux-musl.tar.gz /tmp/datadog.tar.gz
RUN mkdir -p /opt/datadog && tar -xvzf /tmp/datadog.tar.gz -C /opt/datadog

# Variáveis para habilitar auto-instrumentação no .NET Core
ENV CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER={918728DD-259F-4A6A-AC2B-B85E1B658318} \
    CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so \
    DD_DOTNET_TRACER_HOME=/opt/datadog

# ================================

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os arquivos de projeto para aproveitar cache
COPY FiapCloudGamesAPI/*.csproj FiapCloudGamesAPI/
RUN dotnet restore FiapCloudGamesAPI/FiapCloudGamesAPI.csproj

# Copia o restante do código
COPY . .

# Publica o projeto
RUN dotnet publish FiapCloudGamesAPI/FiapCloudGamesAPI.csproj -c Release -o /app/publish

# Etapa final de runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FiapCloudGamesAPI.dll"]
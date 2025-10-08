# 🎮 FIAP Cloud Games — API de Usuários

API REST para gerenciamento de usuários da plataforma **FIAP Cloud Games**, desenvolvida em **.NET 8** seguindo **Clean Architecture** e práticas modernas de desenvolvimento.

---

## 🧬 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Pré-requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Execução](#execução)
- [Endpoints da API](#endpoints-da-api)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Testes](#testes)
- [Docker](#docker)
- [Monitoramento e Logs](#monitoramento-e-logs)
- [Segurança](#segurança)
- [Performance](#performance)
- [Deploy](#deploy)
- [Licença](#licença)
- [Equipe](#equipe)
- [Suporte](#suporte)

---

## 📘 Sobre o Projeto

A **API de Usuários** é um microserviço responsável pelo gerenciamento completo de usuários da plataforma **FIAP Cloud Games**.

### ✨ Funcionalidades Principais
- 👤 Cadastro de novos usuários  
- 🔍 Consulta e listagem de usuários  
- ✏️ Atualização de dados do usuário  
- 🗑️ Exclusão de usuários  
- ✅ Validação de dados e hash de senhas  
- 🕒 Auditoria com timestamps automáticos  

---

## 💻 Tecnologias

### 🧩 Core
- **.NET 8** — Framework principal  
- **C# 12** — Linguagem  
- **ASP.NET Core** — Framework web  

### 🗄️ Banco de Dados
- **SQL Server**  
- **Entity Framework Core 8.0.20**  
- **EF Core Proxies (Lazy Loading)**  

### 🛠️ Arquitetura e Padrões
- **Clean Architecture**  
- **Repository Pattern**  
- **Dependency Injection**  
- **CQRS (Command Query Responsibility Segregation)**  

### 🤞 Documentação e Testes
- **Swagger / OpenAPI**  
- **xUnit** e **MSTest**  

---

## 🧱 Arquitetura

O projeto segue o padrão **Clean Architecture**, organizado em camadas bem definidas:

```
┌── Presentation (API)
│    fiapcloudgames.usuario.API
├── Application
│    fiapcloudgames.usuario.Application
├── Domain
│    fiapcloudgames.usuario.Domain
└── Infrastructure
     fiapcloudgames.usuario.Infrastructure
```

**Camadas:**
1. **API (Presentation)** — Controllers e entrada da aplicação  
2. **Application** — Casos de uso, DTOs e serviços  
3. **Domain** — Entidades e regras de negócio  
4. **Infrastructure** — Persistência, EF Core e implementações externas  
5. **IoC / Shared** — Injeção de dependências e utilitários  

---

## ⚙️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB ou Azure)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)  
- [Git](https://git-scm.com/)  

---

## 🚀 Instalação

```bash
# Clonar o repositório
git clone https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames/_git/FCG_Usuarios
cd FCG_Usuarios

# Restaurar pacotes
dotnet restore

# Compilar
dotnet build
```

---

## 🔧 Configuração

### 1. String de Conexão

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ConnFiapCloudGames": "Server=localhost;Database=usuarios-db;User Id=sa;Password=Senha123;TrustServerCertificate=True;"
  }
}
```

### 2. Migrations

```bash
# Criar migration inicial
dotnet ef migrations add InitialCreate --project src/fiapcloudgames.usuario.Infrastructure --startup-project src/fiapcloudgames.usuario.API

# Aplicar migrations
dotnet ef database update --project src/fiapcloudgames.usuario.Infrastructure --startup-project src/fiapcloudgames.usuario.API
```

---

## ▶️ Execução

### Desenvolvimento
```bash
dotnet watch run --project src/fiapcloudgames.usuario.API
```

Acesse:
- 🌐 HTTP: `http://localhost:5000`
- 🔒 HTTPS: `https://localhost:5001`
- 📄 Swagger: `https://localhost:5001/swagger`

### Produção
```bash
dotnet publish -c Release -o ./publish
dotnet ./publish/fiapcloudgames.usuario.API.dll
```

---

## 📡 Endpoints da API

**Base URL:** `https://localhost:5001/api/usuarios`

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `POST` | `/` | Cadastra um novo usuário | 201, 400 |
| `GET` | `/{id}` | Obtém usuário por ID | 200, 404 |
| `GET` | `/` | Lista todos os usuários | 200 |
| `PUT` | `/` | Atualiza dados do usuário | 200, 400, 404 |
| `DELETE` | `/{id}` | Exclui usuário por ID | 204, 404 |

---

## 🧩 Estrutura do Projeto

```
src/
├── fiapcloudgames.usuario.API/              # 🌐 Apresentação
├── fiapcloudgames.usuario.Application/      # 🧠 Lógica de aplicação
├── fiapcloudgames.usuario.Domain/           # 💡 Regras de negócio
├── fiapcloudgames.usuario.Infrastructure/   # 🗄️ Persistência (EF Core)
├── fiapcloudgames.usuario.Ioc/              # ⚙️ Injeção de dependências
└── fiapcloudgames.usuario.Shared/           # 🧰 Utilitários

tests/
├── fiapcloudgames.usuario.UnitTests/        # 🧪 Unit Tests
└── fiapcloudgames.usuario.IntegrationTests/ # 🔗 Integration Tests
```

---

## 🧪 Testes

```bash
# Executar todos os testes
dotnet test

# Testes unitários
dotnet test tests/fiapcloudgames.usuario.UnitTests/

# Testes de integração
dotnet test tests/fiapcloudgames.usuario.IntegrationTests/
```

---

## 🐳 Docker

### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/fiapcloudgames.usuario.API/fiapcloudgames.usuario.API.csproj", "src/fiapcloudgames.usuario.API/"]
RUN dotnet restore "src/fiapcloudgames.usuario.API/fiapcloudgames.usuario.API.csproj"
COPY . .
WORKDIR "/src/src/fiapcloudgames.usuario.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fiapcloudgames.usuario.API.dll"]
```

### docker-compose.yml
```yaml
version: '3.8'
services:
  api:
    build: .
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__ConnFiapCloudGames=Server=sql-server;Database=fiap;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;
    depends_on:
      - sql-server

  sql-server:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123
    ports:
      - "1433:1433"
```

---

## 🩺 Monitoramento e Logs

- 🟢 **Information** — Operações normais  
- 🟡 **Warning** — Situações de atenção  
- 🔴 **Error / Critical** — Falhas e exceções  

**Health Check:** `GET /health`

---

## 🔐 Segurança

- 🔑 Hash de senhas  
- 🧱 Validação de entrada  
- 🔒 HTTPS obrigatório  
- ⚠️ Tratamento global de exceções  
- 🪪 Autenticação JWT *(planejada)*  
- 🚦 Rate Limiting *(planejado)*  

---

## ⚡ Performance

- Lazy Loading (EF Core)  
- Connection Pooling  
- Command Timeout configurado (40s)  
- Operacões assíncronas (`async/await`)  

---

## ☁️ Deploy

### Azure
- Azure SQL Database  
- Azure App Service  
- Application Insights *(planejado)*  

### Variáveis de Ambiente
```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__ConnFiapCloudGames="sua-connection-string"
```

---

## 🧹 Padrões de Código

- Clean Code + SOLID  
- C# 12  
- Testes obrigatórios para novas features  
- Documentação XML + Swagger/OpenAPI  

---

## 📄 Licença

Projeto de uso **educacional** desenvolvido pela **FIAP**.  
Distribuição e uso restritos ao ambiente acadêmico.

---

## 👥 Equipe

- 💻 **Desenvolvimento:** Equipe FIAP Cloud Games  
- 🧱 **Arquitetura:** Clean Architecture Pattern  
- ⚙️ **Stack:** .NET 8, SQL Server, EF Core  

---

## 🆘 Suporte

- 📘 **Swagger UI:** [https://localhost:5001/swagger](https://localhost:5001/swagger)  
- 🐞 **Issues:** [Azure DevOps](https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames/_git/FCG_Usuarios)

---

> 🎮 **FIAP Cloud Games** — Transformando a experiência de jogos na nuvem.


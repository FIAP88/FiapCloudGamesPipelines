# FiapCloudGamesAPI 

FiapCloudGamesAPI é uma plataforma de venda de jogos digitais com funcionalidades para gerenciamento de servidores para partidas online. Essa aplicação oferece recursos para cadastro de usuários, gerenciamento de jogos e bibliotecas, controle de perfis e permissões, entre outros.

## 📌 Funcionalidades Implementadas

- Cadastro de Usuário
- Cadastro de Jogos
- Categorias de Jogos
- Bibliotecas de Jogos
- Compra de Jogos
- Fornecedores
- Gestão de Perfil e Permissões

## 🚀 Tecnologias Utilizadas

- [.NET 8 (LTS)](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [SQL Server](https://www.microsoft.com/sql-server)
- Autenticação via JWT (Bearer Token)
- Testes Unitários e de Integração com xUnit, Moq e Bogus

## 🚀 Tecnologias Necessárias Instalação

- SDK dotnet 8: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0
- Sql Server Express: https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0x416&culture=pt-br&country=br
- Pacote dotnet-ef: dotnet tool install --global dotnet-ef

## ⚙️ Como Rodar o Projeto Localmente

1. Abra o prompt de comando: (Win + R) Digite "CMD" e pressione "Enter".
   
2. Clone este repositório:
    ```bash
    git clone https://github.com/RyanBrayan/TechChallenge-03-06.git
    cd TechChallenge-03-06/FiapCloudGamesAPI
    ```    
3. Aplique as migrations existentes com o comando:
    
    - Execute update do EF
    ```bash
   dotnet ef database update
    ```

4. Execute o projeto com o comando:
    ```bash
    dotnet run
    ```

5. A API estará disponível em: `http://localhost:5030/swagger/index.html` (ou conforme configurado).

## 🔐 Autenticação

A API utiliza autenticação via **JWT Bearer Token**. Será necessário incluir o token no header de cada requisição protegida:

```
> "Por favor, insira 'Bearer' [espaço] e o token JWT"

Authorization: Bearer {token_aqui}

```

Para criação do Token é necessário acessar o endpoint `api/login` e inserir os dados do usuário administrador, listado abaixo:
```
email: "joao@email.com"
senha: "Te$te123"
```

## 🧪 Rodando os Testes

> Certifique-se de que o projeto não esteja rodando antes de executar os testes.
> Utilize o comando "Ctrl + C" para interromper o projeto.

Direcione-se ao diretório de testes
```bash
cd ..
cd FiapCloudGamesTest
```
Execute os testes unitários com:
```bash
dotnet test
```

## 📂 Estrutura do Projeto

- `Configurations` – Modelagem do banco de dados
- `Entidade` – DTOs, Requests
- `Infra` – Correlation
- `Apresentação` – Controllers, autenticação

## 💪 Utilização com Docker

A aplicação pode ser executada utilizando containers Docker, facilitando o setup do ambiente.

### 📦 Como Executar com Docker

1. Certifique-se de ter o Docker instalado: [Instalar Docker](https://www.docker.com/get-started)

2. No diretório raiz da aplicação, execute o seguinte comando para construir a imagem Docker:

```bash
docker build -t fiapcloudgamesapi .
```

3. Em seguida, execute o container:

```bash
docker run -d -p 5030:80 --name fiapcloudgamesapi-container fiapcloudgamesapi
```

4. A aplicação estará disponível em: [http://localhost:5030/swagger/index.html](http://localhost:5030/swagger/index.html)
5. Docker: [https://hub.docker.com/r/gabrielpaulino/fiapcloudgamesapi](https://hub.docker.com/r/gabrielpaulino/fiapcloudgamesapi)

### 📁 Exemplo de Dockerfile

```dockerfile
# Etapa base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
 
# NENHUMA CONFIGURAÇÃO DO DATADOG AQUI.
# O Azure irá injetar o tracer automaticamente.
 
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

```

## 📊 Monitoramento com Prometheus e Grafana

A aplicação está preparada para exposição de métricas compatíveis com Prometheus, que podem ser visualizadas e monitoradas com Grafana.

### 🔧 Configuração do Prometheus

1. Adicione o `prometheus.yml` com o seguinte conteúdo:

```yaml
global:
  scrape_interval: 15s

scrape_configs:
  - job_name: 'cloudgames-api'
    metrics_path: /metrics
    scheme: https
    static_configs:
      - targets: ['fiap-cloudgames-api-fugyafhxcre6cxfq.canadacentral-01.azurewebsites.net']

```

2. Execute o Prometheus com:

```bash
docker run -d -p 9090:9090 -v $(pwd)/prometheus.yml:/etc/prometheus/prometheus.yml prom/prometheus
```

### 📈 Configuração do Grafana

1. Execute o Grafana:

```bash
docker run -d -p 3000:3000 grafana/grafana
```

2. Acesse [http://localhost:3000](http://localhost:3000), login padrão:

- **Usuário:** admin
- **Senha:** admin

3. Configure uma nova fonte de dados do tipo Prometheus apontando para `http://host.docker.internal:9090`

4. Importe dashboards prontos ou crie visualizações personalizadas.

## Azure DevOps

[https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames](https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames)

## ✒️ Autores

- Gabriel Aljarila dos Santos (gabrielaljarila@gmail.com)
- Gabriel Paulino Farias da Silva (gabriel.paulino@edge.ufal.br)
- Frederico Lopes Vieira (fredericolv@gmail.com)
- Leonardo Neves Perles (leonardo.perles@hotmail.com)
- Ryan Brayan Ferreira Rodrigues (ryanbrayanf@gmail.com)

## 📄 Licença

Este projeto é de uso educacional. Requer ferramentas com licença compatível como .NET SDK (gratuito) e SQL Server (versão gratuita).


---

Desenvolvido com ❤️ usando .NET e conteúdos ensinados na Pós Tech - Arquitetura de Sistemas .Net da FIAP.

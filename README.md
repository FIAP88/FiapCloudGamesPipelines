# FiapCloudGames (TechChallenge-09-01-26) - Arquitetura de Microsserviços

Bem-vindo à FiapCloudGames, uma plataforma de venda de jogos digitais. Esta versão do projeto foi re-arquitetada de um sistema monolítico para uma **arquitetura de microsserviços**, visando maior escalabilidade, resiliência e manutenibilidade.

A solução é composta por APIs independentes para cada domínio de negócio (Usuários, Jogos e Pagamentos) e utiliza um fluxo assíncrono com filas para processar pagamentos de forma robusta.

## 🏛️ Arquitetura da Solução

A arquitetura atual é distribuída e consiste nos seguintes componentes principais:

* **API de Usuários**: Responsável pelo cadastro, autenticação de usuários e geração de tokens JWT. É o ponto de entrada para a segurança da plataforma.
* **API de Jogos**: Gerencia todo o ciclo de vida dos jogos, incluindo cadastro, consulta, categorias e bibliotecas dos usuários.
* **API de Pagamentos**: Inicia o processo de compra, registrando a intenção de pagamento e publicando uma mensagem em uma fila para processamento assíncrono.
* **Function de Pagamentos (Azure Function)**: Um processo serverless que é acionado por novas mensagens na fila de pagamentos, executando a lógica de processamento final da transação.
* **Azure Service Bus**: Atua como o message broker, garantindo a comunicação assíncrona e resiliente entre a API de Pagamentos e a Function.
* **RabbitMQ"": Responsavel por controlar o fluxo de troca de mensagens entre os micro serviços.

# Arquitetura do Sistema

Esta arquitetura descreve um sistema baseado em microsserviços, projetado para ser escalável e resiliente, especialmente no processamento de pagamentos. O sistema é composto por um ponto de entrada único (API Gateway), múltiplos serviços independentes e um fluxo de processamento de pagamentos assíncrono que utiliza filas de mensagens no ecossistema da Microsoft Azure.

## Fluxo de funcionamento no Kubernetes
<img width="4081" height="1630" alt="mermaid-diagram-2026-01-09-201922" src="https://github.com/user-attachments/assets/82575d3e-071e-4a7f-bf54-79c5f95264a9" />

A arquitetura utiliza Kubernetes (AKS) para orquestração de containers. O acesso externo à aplicação é realizado por meio de um Azure Load Balancer integrado ao NGINX Ingress Controller, responsável pelo roteamento das requisições HTTP para os serviços internos conforme o caminho da URL. Cada microserviço é exposto por um Service do tipo ClusterIP e executado em múltiplos Pods gerenciados por Deployments, com escalabilidade automática via Horizontal Pod Autoscaler (HPA) baseado em consumo de CPU. As informações sensíveis são armazenadas em Secrets e a observabilidade é realizada através do Application Insights.

## Fluxo de comunicação dos microsserviços
![Fluxo Micro serviços](https://github.com/user-attachments/assets/46272efc-6e53-47ff-8415-2786397d1462)

## Desenho de arquitetura representando o fluxo de funcionamento
1.  **Ponto de Entrada (Entry Point)**: O **Usuário** interage com o sistema através de um **GATEWAY** (como o **Azure API Management**). Este componente atua como um ponto de entrada único, roteando as requisições para os microsserviços apropriados.

2.  **Microsserviços de Negócio**:
    * **MS Usuários**: Responsável por toda a lógica de negócio relacionada a usuários, como autenticação, cadastro e gerenciamento de perfis. Ele possui seu próprio banco de dados, o **BD Usuários**.
    * **MS Jogos**: Responsável pela lógica relacionada aos jogos. Quando uma ação neste serviço requer um pagamento (ex: compra de um jogo ou item), ele se comunica com o Microsserviço de Pagamentos. Ele também possui um banco de dados dedicado, o **BD Jogos**.

3.  **Início do Fluxo de Pagamento**:
    * O **MS Jogos** uma mensagem para o RabbitMQ, contendo as informações do pedido, que será armazenado em uma pilha de mensagens.  
    * O **MS Pagamentos** Acessar o RabbitMQ e retira da pilha as mensagens enviadas pelo **MS Jogos**.
    * O **MS Pagamentos**, ao invés de processar o pagamento imediatamente (de forma síncrona), adota uma abordagem assíncrona. Ele cria um registro inicial do pagamento no **BD Pagamentos** e envia uma mensagem com os detalhes da transação para uma fila.

## Fluxo de Processamento Assíncrono de Pagamentos

Esta é a parte central e mais detalhada do processo, focada em resiliência e tratamento de erros utilizando os serviços da Azure.

1.  **Enfileiramento (Queuing)**: O **MS Pagamentos** publica a mensagem de pagamento na **Fila de pagamentos** (utilizando o **Azure Service Bus**). Isso desacopla a criação do pagamento de seu processamento efetivo. O serviço que solicitou o pagamento recebe uma resposta imediata, sem precisar esperar a conclusão do processo.

2.  **Processamento (Processing)**:
    * A fila do Service Bus está configurada com um **TRIGGER** que invoca automaticamente uma **Azure Function**, a **Function Pagamentos**.
    * Esta função consome a mensagem da fila e executa a lógica de negócio para processar o pagamento (ex: comunicar-se com um gateway de pagamento externo, validar dados, etc.).
    * Após o processamento bem-sucedido, a **Function Pagamentos** atualiza o status da transação no **BD Pagamentos** (ex: de "pendente" para "aprovado").

3.  **Tratamento de Erros (Dead-Letter Queue)**:
    * Se a **Function Pagamentos** falhar ao processar uma mensagem, o Azure Service Bus possui um mecanismo nativo de *dead-lettering*.
    * Após um número configurável de tentativas de re-processamento falharem, a mensagem é automaticamente movida para uma sub-fila especial, a **Dead-Letter Queue (DLQ)**, aqui representada como **Fila de pagamentos POISON**.
    * Isso impede que uma mensagem com erro bloqueie o processamento de outras mensagens válidas e permite que a equipe de desenvolvimento analise e re-processe manualmente as transações que falharam.


> **Nota**: Nesta configuração local, não utilizaremos um API Gateway. As chamadas serão feitas diretamente para cada microsserviço em sua respectiva porta.

## 📂 Repositórios do Projeto

Cada microsserviço reside em seu próprio repositório para garantir a autonomia das equipes e dos pipelines de CI/CD.

* **API de Usuários**: `git clone https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames/_git/FCG_Usuarios`
* **API de Jogos**: `git clone https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames/_git/FCG_Jogos`
* **API de Pagamentos**: `git clone https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames/_git/FIAPCloudGames`
* **Function de Pagamentos**: `git clone https://dev.azure.com/FIAPCloudGamesPaulino/FIAPCloudGames/_git/Pagamentos_functions`

## 🚀 Tecnologias Utilizadas

- **Backend**: .NET 8 (STS) para todas as APIs e a Function
- **Banco de Dados**: SQL Server (um banco de dados por microsserviço)
- **ORM**: Entity Framework Core
- **Mensageria**: Azure Service Bus e RabbitMQ
- **Computação Serverless**: Azure Functions
- **Autenticação**: JWT (Bearer Token), gerenciado pela API de Usuários
- **Testes**: xUnit, Moq, Bogus (no projeto de Usuários)

## ✅ Pré-requisitos para Execução Local

Antes de começar, garanta que você tenha as seguintes ferramentas instaladas:

1.  **SDK .NET 8**: [Link para download](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)
2.  **SQL Server Express**: [Link para download](https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0x416&culture=pt-br&country=br)
3.  **Azure Functions Core Tools**: `npm install -g azure-functions-core-tools@4`
4.  **Conta no Azure**: Você precisará de uma conta para configurar o Service Bus.
5.  **Git**: Para clonar os repositórios.

## ⚙️ Configuração e Execução do Ambiente Local (Passo a Passo)

Siga estes passos para configurar e executar toda a solução na sua máquina.

### Passo 1: Clonar todos os Repositórios

Abra seu terminal e clone cada um dos quatro repositórios em uma pasta principal de sua escolha.

### Passo 2: Configurar o Banco de Dados

Cada microsserviço (Usuários, Jogos, Pagamentos) possui seu próprio banco de dados.

1.  Verifique sua string de conexão do SQL Server Express.
2.  Para **cada uma das 3 APIs**, navegue até a pasta do projeto e execute os seguintes comandos:

    ```bash
    # Exemplo para a API de Usuários
    cd FiapCloudGamesAPI.Users

    # Abra o arquivo appsettings.Development.json e configure sua ConnectionString
    # Ex: "DefaultConnection": "Server=.\\SQLEXPRESS;Database=DbCloudGamesUsers;Trusted_Connection=True;TrustServerCertificate=True;"

    # Aplique as migrations para criar o banco e as tabelas
    dotnet ef database update
    ```
3.  **Repita o passo 2** para as APIs de Jogos e Pagamentos, lembrando de usar nomes de bancos de dados diferentes (ex: `DbCloudGamesGames`, `DbCloudGamesPayments`).

### Passo 3: Configurar o Azure Service Bus

1.  Acesse o [Portal do Azure](https://portal.azure.com/).
2.  Crie um recurso do tipo **Service Bus**.
3.  Dentro do seu Service Bus, crie uma **Fila (Queue)** chamada `pagamentos`.
4.  Habilite a opção **"Habilitar dead-lettering em caso de expiração de mensagem"** na criação da fila.
5.  Vá para **"Shared access policies"** e copie a connection string principal (`Primary Connection String`).

### Passo 4: Configurar as Variáveis de Ambiente

As variáveis de ambiente necessárias para a execução dos serviços podem ser configuradas localmente, para fins de desenvolvimento, ou no ambiente Kubernetes, utilizando ConfigMaps e Secrets. No ambiente Kubernetes, essas configurações são criadas por script e aplicadas diretamente no cluster.

### Ambiente Local
1.  **API de Pagamentos**: No arquivo `appsettings.Development.json`, adicione a connection string do Azure Service Bus.
2.  **Function de Pagamentos**: No arquivo `local.settings.json`, adicione a mesma connection string. O arquivo deve se parecer com isso:
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "ServiceBusConnectionString": "SUA_CONNECTION_STRING_DO_SERVICE_BUS"
      }
    }
    ```
### Ambiente Kubernetes
1.  **API de Pagamentos**: Essa configuração deve ser externalizada para um ConfigMap ou Secret, de acordo com a sensibilidade da informação, e injetada no container por meio do Deployment.
2.  **Function de Pagamentos**: Assim como a API, as variáveis de ambiente da Function devem ser configuradas por meio de ConfigMaps e Secrets, definidos nos arquivos YAML da pasta k8s/.
3.  **Criação das Variáveis no Cluster**: A criação das variáveis de ambiente no Kubernetes é realizada por meio de um script de automação, responsável por:
   Gerar connection strings dos bancos de dados
   Configurar chaves de fila (Storage / Service Bus)
   Registrar chaves JWT e Application Insights
   Criar e atualizar os Secrets no cluster

O script pode ser encontrado na pasta env nos arquivos de Deploymente localizados na pasta k8s/
    
### Passo 5: Rodar a Solução Completa

> **IMPORTANTE**: Cada serviço deve ser executado em um terminal diferente, simultaneamente.

1.  **Terminal 1 - API de Usuários**:
    ```bash
    cd [PASTA-DA-API-DE-USUARIOS]
    dotnet run
    # Geralmente executa em: http://localhost:5001
    ```

2.  **Terminal 2 - API de Jogos**:
    ```bash
    cd [PASTA-DA-API-DE-JOGOS]
    dotnet run
    # Geralmente executa em: http://localhost:5002
    ```

3.  **Terminal 3 - API de Pagamentos**:
    ```bash
    cd [PASTA-DA-API-DE-PAGAMENTOS]
    dotnet run
    # Geralmente executa em: http://localhost:5003
    ```

4.  **Terminal 4 - Function de Pagamentos**:
    ```bash
    cd [PASTA-DA-FUNCTION-DE-PAGAMENTOS]
    func start
    # Geralmente executa em: http://localhost:7071
    ```

Ao final, você terá 4 processos rodando. A solução está pronta para uso!

## 🔐 Autenticação e Fluxo de Uso

1.  **Obter o Token**: Faça uma requisição `POST` para a **API de Usuários** no endpoint `/api/login` para obter seu JWT Bearer Token.
    * **URL**: `http://localhost:5001/api/login`
    * **Body**:
        ```json
        {
          "email": "joao.silva@fiapcloudgames.com",
          "senha": "123456"
        }
        ```

2.  **Fazer Chamadas Autenticadas**: Para acessar endpoints protegidos (ex: comprar um jogo na API de Jogos), inclua o token no header `Authorization`.
    * **Exemplo**: Para chamar a API de Jogos na porta `5002`:
        `Authorization: Bearer {seu_token_aqui}`

## 🧪 Rodando os Testes

Os testes unitários e de integração estão presentes apenas no projeto da **API de Usuários**.

1.  Navegue até a pasta de testes do projeto de usuários.
2.  Execute o comando:
    ```bash
    dotnet test
    ```

## ✒️ Autores

- Gabriel Aljarila dos Santos (gabrielaljarila@gmail.com)
- Gabriel Paulino Farias da Silva (gabriel.paulino@edge.ufal.br)
- Frederico Lopes Vieira (fredericolv@gmail.com)
- Leonardo Neves Perles (leonardo.perles@hotmail.com)
- Ryan Brayan Ferreira Rodrigues (ryanbrayanf@gmail.com)

---

Desenvolvido com ❤️ usando .NET e conteúdos ensinados na Pós Tech - Arquitetura de Sistemas .Net da FIAP.

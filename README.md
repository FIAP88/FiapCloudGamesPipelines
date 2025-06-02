# FiapCloudGamesAPI (TechChallenge-03-06)

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

- [.NET 9 (STS)](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [SQL Server](https://www.microsoft.com/sql-server)
- Autenticação via JWT (Bearer Token)
- Testes Unitários e de Integração com xUnit, Moq e Bogus

## ⚙️ Como Rodar o Projeto Localmente

1. Clone este repositório:
    ```bash
    git clone https://github.com/RyanBrayan/TechChallenge-03-06.git

    cd TechChallenge-03-06/FiapCloudGamesAPI
    ```

2. Execute o projeto com o comando:
    ```bash
    dotnet run
    ```

3. A API estará disponível em: `http://localhost:5030` (ou conforme configurado).

## 🛠️ Configuração do Banco de Dados

A aplicação utiliza SQL Server. Para criar a base de dados:

1. Altere a `ConnectionString` no arquivo `appsettings.json` com os dados corretos do seu SQL Server.
2. Aplique as migrations existentes com o comando:
    ```bash
    dotnet ef database update
    ```

> Certifique-se de que o SQL Server esteja ativo e acessível localmente ou via rede.

## 🔐 Autenticação

A API utiliza autenticação via **JWT Bearer Token**. Será necessário incluir o token no header de cada requisição protegida:

```
> "Por favor, insira 'Bearer' [espaço] e o token JWT"

Authorization: Bearer {token_aqui}

```

## 🧪 Rodando os Testes

Execute os testes unitários e de integração com:

```bash
dotnet test
```

## 📂 Estrutura do Projeto

- `Configurations` – Modelagem do banco de dados
- `Entidade` – DTOs, Requests
- `Infra` – Correlation
- `Apresentação` – Controllers, autenticação

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

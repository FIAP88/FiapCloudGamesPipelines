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

## 🚀 Tecnologias Necessárias Instalação

- SDK dotnet 9: https://dotnet.microsoft.com/pt-br/download/dotnet/9.0
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

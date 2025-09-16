# DbCONNECTOR

DbCONNECTOR é um projeto em C# que visa facilitar o uso, configuração e o acesso a bancos de dados de diferentes fornecedores.
Com suporte inicial para PostgreSQL. O projeto oferece, uma API flexível, desacoplada e fácil de usar.

---

## Objetivo

- Servir como estrutura base para projetos que dependem da configuração e acesso a um banco de dados.
- Fornecer uma API simples para configurar e conectar ao PostgreSQL.  
- Foco em modularidade.

---

## Estrutura do Projeto

```
DbConnector/
├── src/					
│ ├── DbConnector.Adapters/     # Adapter para PostgreSQL
│ ├── DbConnector.Core/		    # Builder + configuração de conexão
│ ├── DbConnector.Exceptions/   # Tratamento de exceções
│ ├── DbConnector.Models/       # Entidades
│ └── DbConnector.Repositories/ # Classes que encapsulam operações ao banco de dados
├── samples/				
│ └── ConsoleApp/			    # Exemplo de uso
├── tests/					
│ └── DbConnector.Tests/	    # Testes
```

---

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/download/)
- [Docker](https://www.docker.com/) (opcional, para rodar containerizado)

---

## Como rodar o projeto localmente

1. Clone o repositório

```bash
git clone https://github.com/kkaicdev/DbConnector.git
```

2. Compile a solução

```bash
dotnet build
```

3. Rode os testes com xUnit
```bash
dotnet test
```

4. Execute o exemplo de uso

```bash
dotnet run --project samples/ConsoleApp.csproj
```

## Como rodar o projeto via Docker

1. Build da imagem

```bash
docker build -t dbconnector .
```

2. Rodando o container

```bash
docker run --rm dbconnector
```

3. Execute os testes com xUnit:

```bash
dotnet test
```

⚠️ Por padrão, a aplicação está configurada para se conectar a um banco PostgreSQL em `localhost:5432`.
Você pode ajustar as credenciais/configurações do banco em samples/ConsoleApp.cs OU passar via parâmetros pelo Docker com:

```bash
docker run --rm \
  -e DB_SERVER=localhost \
  -e DB_USER=postgres \
  -e DB_PASSWORD=admin \
  -e DB_NAME=testedb \
  dbconnector
```

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

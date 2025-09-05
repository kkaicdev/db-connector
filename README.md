﻿# DbConnector

DbConnector é uma biblioteca em C# que facilita a configuração e o acesso a bancos de dados.
Com suporte inicial para PostgreSQL. O projeto utiliza os padrões Builder e Adapter (GOF), 
fornecendo uma API flexível, desacoplada e fácil de usar.

---

## Objetivo

- Fornecer uma API simples para configurar e conectar ao PostgreSQL.  
- Servir como estrutura base para projetos que dependem da configuração e acesso a um banco de dados.
- Promover um design modular, separando claramente Core, Adapters, Models e Repositories.

---

## Estrutura do Projeto

```
DbConnector/
├── src/					
│ ├── DbConnector.Core/		      # Builder + configuração de conexão
│ ├── DbConnector.Adapters/     # Adapter para PostgreSQL
│ ├── DbConnector.Models/       # Entidades
│ └── DbConnector.Repositories/ # Classes que encapsulam operações ao banco de dados
├── samples/				
│ └── ConsoleApp/			          # Exemplo de uso
├── tests/					
│ └── DbConnector.Tests/	      # Testes
```

---

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/download/)

[!] Ajuste as credenciais/configurações do banco em ConsoleApp e DbConnectionConfig (Server, Port, User, Passw e Db).

---

## Como rodar o projeto

1. Clone o repositório

```bash
git clone https://github.com/kkaicdev/DbConnector.git
cd DbConnector
```

2. Compile a solução

```bash
dotnet build
```

3. Execute o ConsoleApp:

```bash
dotnet run --project samples/ConsoleApp
```

4. Execute os testes com xUnit:

```bash
dotnet test
```

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

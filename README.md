# DbConnector

DbConnector é uma biblioteca em C# para facilitar a configuração e o acesso a bancos de dados.
Implementada inicialmente para PostgreSQL. O projeto utiliza os padrões GOF Builder e Adapter, para oferecer uma API flexível e desacoplada.

---

## Objetivo

- Fornecer uma API simples para configurar e conectar ao PostgreSQL.  
- Servir como estrutura inicial para projetos que dependem da configuração e acesso a um banco de dados.

---

## Estrutura do Projeto

```
DbConnector/
├── src/					
│ ├── DbConnector.Core/		# Builder + configuração de conexão
│ └── DbConnector.Adapters/ # Adapter para PostgreSQL
├── samples/				
│ └── ConsoleApp/			# Exemplo de uso
├── tests/					
│ └── DbConnector.Tests/	# Testes
```

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/download/)

[!] Ajuste a configuração do banco (Server, Port, User, Passw e Db) dentro do ConsoleApp ou DbConnectionConfig.

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

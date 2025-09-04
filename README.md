# DbConnector

**DbConnector** � uma biblioteca em C# para facilitar a configura��o e o acesso a bancos de dados, inicialmente implementada para **PostgreSQL**.  
O projeto utiliza **padr�es de projeto GoF** como **Builder** e **Adapter** para oferecer uma API flex�vel e desacoplada.

---

## Objetivo

- Fornecer uma API simples para configurar e conectar ao PostgreSQL.  
- Servir como estrutura inicial para projetos que dependem da configura��o e acesso a um banco de dados.
- Aplicar **TDD** com testes automatizados.

---

## Estrutura do Projeto

DbConnector/
??? src/					
? ??? DbConnector.Core/		# Builder + configura��o de conex�o
? ??? DbConnector.Adapters/ # Adapter para PostgreSQL
??? samples/				
? ??? ConsoleApp/			# Exemplo de uso
??? tests/					
? ??? DbConnector.Tests/	# Testes

---

## Pr�-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/download/)

[!] Ajuste a configura��o do banco (Server, Port, User, Passw e Db) dentro do ConsoleApp ou DbConnectionConfig.

--

## Como rodar o projeto

1. Clone o reposit�rio

```bash
git clone https://github.com/kkaicdev/DbConnector.git
cd DbConnector
```

2. Compile a solu��o

```bash
dotnet build
```

3. Execute o ConsoleApp:

```bash
dotnet run --project samples/ConsoleApp
```

4. Como rodar os testes:

O projeto usa xUnit. Voc� pode executar com

```bash
dotnet test
```

## Licen�a

Este projeto est� licenciado sob a [MIT License](LICENSE).
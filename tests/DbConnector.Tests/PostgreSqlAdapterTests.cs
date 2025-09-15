using DbConnector.Core;
using DbConnector.Adapters;
using Xunit;

public class PostgreSqlAdapterTests
{
    [Fact]
    public async Task PostgreAdapter_Connect_ExecuteAndQuery()
    {
        var cfg = new DbConnectionBuilder()
            .WithServer("192.168.1.18")
            .WithPort(5432)
            .WithDatabase("testedb")
            .WithUser("postgres")
            .WithPassword("admin")
            .Build();

        await using var db = new PostgreSqlAdapter(cfg);
        await db.ConnectAsync();

        try
        {
            await db.ExecuteAsync("DROP TABLE IF EXISTS test_tdd;");
            await db.ExecuteAsync("CREATE TABLE test_tdd(id SERIAL PRIMARY KEY, name TEXT);");

            // Inserção 
            await db.ExecuteAsync(
                "INSERT INTO test_tdd(name) VALUES(@name);",
                new Dictionary<string, object?> { ["@name"] = "Alice" }
            );

            // Leitura
            var rowsList = new List<IDictionary<string, object?>>();
            await foreach (var r in db.QueryAsync("SELECT name FROM test_tdd"))
                rowsList.Add(r);

            Assert.Contains(rowsList, r => r["name"]?.ToString() == "Alice");
        }
        finally
        {
            await db.ExecuteAsync("DROP TABLE IF EXISTS test_tdd;");
        }


        
    }
}
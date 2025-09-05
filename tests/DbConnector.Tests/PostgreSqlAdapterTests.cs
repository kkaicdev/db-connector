using DbConnector.Core;
using DbConnector.Adapters;
using Xunit;

public class PostgreSqlAdapterTests
{
    [Fact]
    public void PostgreAdapter_Connect_ExecuteAndQuery()
    {
        var cfg = new DbConnectionBuilder()
            .WithServer("192.168.1.18")
            .WithPort(5432)
            .WithDatabase("testedb")
            .WithUser("postgres")
            .WithPassword("admin")
            .Build();

        using var db = new PostgreSqlAdapter(cfg);
        db.Connect();

        try
        {
            db.Execute("DROP TABLE IF EXISTS test_tdd;");
            db.Execute("CREATE TABLE test_tdd(id SERIAL PRIMARY KEY, name TEXT);");

            db.Execute("INSERT INTO test_tdd(name) VALUES('Alice');");

            var rows = db.Query("SELECT name FROM test_tdd");
            Assert.Contains(rows, r => r["name"]?.ToString() == "Alice");
        }
        finally
        {
            db.Execute("DROP TABLE IF EXISTS test_tdd;");
        }


        
    }
}
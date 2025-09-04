using DbConnector.Adapters;
using DbConnector.Core;

class ConsoleApp
{
    static void Main()
    {
        var cfg = new DbConnectionBuilder()
            .WithServer("192.168.1.18")
            .WithPort(5432)
            .WithUser("postgres")
            .WithPassword("admin")
            .WithDatabase("testedb")
            .Build();

        using var db = new PostgreSqlAdapter(cfg);
        db.Connect();

        db.Execute("CREATE TABLE IF NOT EXISTS users(id SERIAL PRIMARY KEY, name TEXT);");
        db.Execute("INSERT INTO users(name) VALUES('Oda'),('Donquixote'),('Trace');");

        var rows = db.Query("SELECT id, name FROM users ORDER BY id LIMIT 5;");
        foreach (var row in rows)
            Console.WriteLine($"{row["id"]}: {row["name"]}");
    }
}
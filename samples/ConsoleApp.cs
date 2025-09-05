using DbConnector.Adapters;
using DbConnector.Core;
using DbConnector.Repositories;

class ConsoleApp
{
    static void Main()
    {
        new AppRunner().Run();
    }
}

class AppRunner
{
    public void Run()
    {
        var cfg = DbConnectionBuilder.Create()
            .WithServer("192.168.1.18")
            .WithPort(5432)
            .WithUser("postgres")
            .WithPassword("admin")
            .WithDatabase("testedb")
            .Build();

        using var db = new PostgreSqlAdapter(cfg);
        db.Connect();

        var repo = new UserRepository(db);
        repo.CreateTable();
        repo.InsertUsers("Flamengo", "Biskela", "Lake");

        foreach (var user in repo.GetUsers(100))
            Console.WriteLine($"{user.Id}: {user.Name}");
    }
}

using DbConnector.Adapters;
using DbConnector.Core;
using DbConnector.Repositories;
using DbConnector.Exceptions;

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
        try
        {
            var cfg = DbConnectionBuilder.Create()
                .WithServer("localhost")
                .WithPort(5432)
                .WithUser("postgres")
                .WithPassword("admin")
                .WithDatabase("testedb")
                .Build();

            using var db = new PostgreSqlAdapter(cfg);
            db.Connect();

            var repo = new UserRepository(db);
            repo.CreateTable();
            repo.InsertUsers("Doflamingo", "Lake", "Alaska");

            foreach (var user in repo.GetUsers(100))
                Console.WriteLine($"{user.Id}: {user.Name}");
        }
        catch (RepositoryException ex)
        {
            Console.WriteLine($"[Repository Error] {ex.Message}");
        }
        catch (DatabaseConnectionException ex)
        {
            Console.WriteLine($"[Connection Error] {ex.Message}");
        }
    }
}

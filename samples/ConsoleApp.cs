using DbConnector.Adapters;
using DbConnector.Core;
using DbConnector.Repositories;
using DbConnector.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class ConsoleApp
{
    static async Task Main()
    {
        // configura os serviços do aplicativo
        using var serviceProvider = ConfigureServices();

        // obtém o logger principal para o postgresql
        var logger = serviceProvider.GetRequiredService<ILogger<PostgreSqlAdapter>>();

        var runner = new AppRunner(logger);
        await runner.RunAsync();
    }

    static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddLogging(cfg =>
            {
                cfg.AddConsole();
                cfg.SetMinimumLevel(LogLevel.Information);
                cfg.AddFilter("DbConnector", LogLevel.Debug);
            })
            .BuildServiceProvider();
    }
}

class AppRunner
{
    private readonly ILogger<PostgreSqlAdapter> _logger;

    public AppRunner(ILogger<PostgreSqlAdapter> logger)
    {
        _logger = logger;
    }

    public async Task RunAsync()
    {
        try
        {
            var cfg = DbConnectionBuilder.Create()
                .WithServer(Environment.GetEnvironmentVariable("DB_SERVER") ?? "192.168.1.18")
                .WithPort(int.TryParse(Environment.GetEnvironmentVariable("DB_PORT"), out var port) ? port : 5432)
                .WithUser(Environment.GetEnvironmentVariable("DB_USER") ?? "postgres")
                .WithPassword(Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "admin")
                .WithDatabase(Environment.GetEnvironmentVariable("DB_NAME") ?? "testedb")
                .Build();

            await using var db = new PostgreSqlAdapter(cfg, _logger);
            await db.ConnectAsync();

            var repo = new UserRepository(db);

            await repo.CreateTableAsync();
            await repo.InsertUsersAsync("Kaidou", "Lake", "Alaska");

            await foreach (var user in repo.GetUsersAsync(200))
                Console.WriteLine($"{user.Id}: {user.Name}");
        }
        catch (RepositoryException ex)
        {
            _logger.LogError($"[Repository Error] {ex.Message}");
        }
        catch (DatabaseConnectionException ex)
        {
            _logger.LogError($"[Connection Error] {ex.Message}");
        }
    }
}

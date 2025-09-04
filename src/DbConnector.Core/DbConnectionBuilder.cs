namespace DbConnector.Core;

public class DbConnectionBuilder
{
    private readonly DbConnectionConfig _cfg = new();

    public DbConnectionBuilder WithServer(string server) { _cfg.Server = server; return this; }
    public DbConnectionBuilder WithPort(int port)        { _cfg.Port = port; return this; }
    public DbConnectionBuilder WithUser(string user) { _cfg.User = user; return this; }
    public DbConnectionBuilder WithPassword(string pwd) { _cfg.Password = pwd; return this; }
    public DbConnectionBuilder WithDatabase(string db)   { _cfg.Database = db; return this; }
    public DbConnectionBuilder WithTimeout(int seconds)  { _cfg.TimeoutSeconds = seconds; return this; }

    public DbConnectionConfig Build() => _cfg;
}

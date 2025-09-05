namespace DbConnector.Core;

public class DbConnectionBuilder
{
    private string _server = "";
    private int _port = 5432;
    private string _user = "";
    private string _password = "";
    private string _database = "";
    private int _timeoutSeconds = 30;

    public static DbConnectionBuilder Create() => new();

    public DbConnectionBuilder WithServer(string server) { _server = server; return this; }
    public DbConnectionBuilder WithPort(int port) { _port = port; return this; }
    public DbConnectionBuilder WithUser(string user) { _user = user; return this; }
    public DbConnectionBuilder WithPassword(string password) { _password = password; return this; }
    public DbConnectionBuilder WithDatabase(string database) { _database = database; return this; }
    public DbConnectionBuilder WithTimeout(int seconds) { _timeoutSeconds = seconds; return this; }

    public DbConnectionConfig Build()
    {
        if (string.IsNullOrWhiteSpace(_server))
            throw new InvalidOperationException("Server cannot be empty.");
        if (string.IsNullOrWhiteSpace(_user))
            throw new InvalidOperationException("User cannot be empty.");
        if (string.IsNullOrWhiteSpace(_database))
            throw new InvalidOperationException("Database cannot be empty.");

        return new DbConnectionConfig(
            server: _server,
            port: _port,
            user: _user,
            password: _password,
            database: _database,
            timeoutSeconds: _timeoutSeconds
        );
    }
}

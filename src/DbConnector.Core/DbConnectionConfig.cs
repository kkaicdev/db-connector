namespace DbConnector.Core;

public class DbConnectionConfig
{
    public string Server { get; }
    public int Port { get; }
    public string User { get; }
    public string Password { get; }
    public string Database { get; }
    public int TimeoutSeconds { get; }

    internal DbConnectionConfig(
        string server,
        int port,
        string user,
        string password,
        string database,
        int timeoutSeconds)
    {
        Server = server ?? throw new ArgumentNullException(nameof(server));
        User = user ?? throw new ArgumentNullException(nameof(user));
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Password = password ?? string.Empty;
        Port = port;
        TimeoutSeconds = timeoutSeconds;
    }
}

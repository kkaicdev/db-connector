namespace DbConnector.Core;

public class DbConnectionConfig
{
    public string Server { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
    public string Database { get; set; } = "";
    public int TimeoutSeconds { get; set; } = 30;
}
using Npgsql;
using DbConnector.Core;

namespace DbConnector.Adapters;

public class PostgreSqlAdapter : IDatabaseAdapter
{
    private readonly DbConnectionConfig _config;
    private NpgsqlConnection? _connection;

    public PostgreSqlAdapter(DbConnectionConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    } 

    public void Connect()
    {
        if (_connection != null)
            return;

        var connString = new NpgsqlConnectionStringBuilder
        {
            Host = _config.Server,
            Port = _config.Port,
            Username = _config.User,
            Password = _config.Password,
            Database = _config.Database,
            Timeout = _config.TimeoutSeconds
        }.ConnectionString;

        _connection = new NpgsqlConnection(connString);
        _connection.Open();
        Console.WriteLine("Connected to database.");
    }

    public IEnumerable<IDictionary<string, object?>> Query(string sql)
    {
        EnsureConnected();

        using var cmd = new NpgsqlCommand(sql, _connection);
        using var rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < rdr.FieldCount; i++)
                row[rdr.GetName(i)] = rdr.IsDBNull(i) ? null : rdr.GetValue(i);

            yield return row;
        }
    }

    public int Execute(string sql)
    {
        EnsureConnected();

        using var cmd = new NpgsqlCommand(sql, _connection);
        return cmd.ExecuteNonQuery();
    }

    private void EnsureConnected()
    {
        if (_connection == null)
            throw new InvalidOperationException("Database connection is not established.");
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _connection = null;
        Console.WriteLine("Disconnected from database.");
    } 
}

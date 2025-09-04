using Npgsql;
using DbConnector.Core;

namespace DbConnector.Adapters;

public class PostgreSqlAdapter : IDatabaseAdapter
{
    private readonly DbConnectionConfig _config;
    private NpgsqlConnection? _connection;

    public PostgreSqlAdapter(DbConnectionConfig config) => _config = config;

    public void Connect()
    {
        var stringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = _config.Server,
            Port = _config.Port,
            Username = _config.User,
            Password = _config.Password,
            Database = _config.Database,
            Timeout = _config.TimeoutSeconds
        };
        _connection = new NpgsqlConnection(stringBuilder.ConnectionString);
        _connection.Open();
    }

    public IEnumerable<IDictionary<string, object?>> Query(string sql)
    {
        if (_connection == null) 
            throw new InvalidOperationException("Not connected.");

        using var cmd = new NpgsqlCommand(sql, _connection);
        using var rdr = cmd.ExecuteReader();
        var list = new List<IDictionary<string, object?>>();

        while (rdr.Read())
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < rdr.FieldCount; i++)
                row[rdr.GetName(i)] = rdr.IsDBNull(i) ? null : rdr.GetValue(i);
            list.Add(row);
        }

        return list;
    }

    public int Execute(string sql)
    {
        if (_connection == null) 
            throw new InvalidOperationException("Not connected.");

        using var cmd = new NpgsqlCommand(sql, _connection);
        return cmd.ExecuteNonQuery();
    }

    public void Dispose() => _connection?.Dispose();
}

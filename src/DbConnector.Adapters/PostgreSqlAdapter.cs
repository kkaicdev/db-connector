using Npgsql;
using DbConnector.Core;
using DbConnector.Exceptions;
using Microsoft.Extensions.Logging;

namespace DbConnector.Adapters;

public class PostgreSqlAdapter : IDatabaseAdapter, IAsyncDisposable
{
    private readonly DbConnectionConfig _config;
    private readonly ILogger<PostgreSqlAdapter>? _logger;
    private NpgsqlConnection? _connection;

    public PostgreSqlAdapter(DbConnectionConfig config, ILogger<PostgreSqlAdapter>? logger = null)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger;
    } 

    public async Task ConnectAsync()
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

        try
        {
            _logger?.LogInformation("[+] Connecting to database {Database} at {Host}:{Port}...", _config.Database, _config.Server, _config.Port);
            await _connection.OpenAsync();
            _logger?.LogInformation("[+] Connected.");
        }
        catch (Npgsql.NpgsqlException ex)
        {
            _logger?.LogError(ex, "[!] Error connecting to database {Database}", _config.Database);
            throw new DatabaseConnectionException("Could not connect to database.", ex);
        }
    }

    public async IAsyncEnumerable<IDictionary<string, object?>> QueryAsync(
        string sql,
        IDictionary<string, object?>? parameters = null)
    {
        EnsureConnected();

        await using var cmd = new NpgsqlCommand(sql, _connection);

        if (parameters != null)
        {
            foreach (var param in parameters)
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
        }

        await using var rdr = await cmd.ExecuteReaderAsync();

        while (await rdr.ReadAsync())
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < rdr.FieldCount; i++)
                row[rdr.GetName(i)] = rdr.IsDBNull(i) ? null : rdr.GetValue(i);

            yield return row;
        }
    }

    public async Task<int> ExecuteAsync(
        string sql, 
        IDictionary<string, object?>? parameters = null)
    {
        EnsureConnected();

        await using var cmd = new NpgsqlCommand(sql, _connection);

        if (parameters != null)
        {
            foreach (var param in parameters)
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
        }

        return await cmd.ExecuteNonQueryAsync();
    }

    private void EnsureConnected()
    {
        if (_connection == null)
            throw new InvalidOperationException("Database connection is not established.");
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }

        _logger?.LogInformation("[+] Disconnected from database.");
    } 
}

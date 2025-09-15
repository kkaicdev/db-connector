namespace DbConnector.Core;

public interface IDatabaseAdapter : IAsyncDisposable
{
    Task ConnectAsync();

    IAsyncEnumerable<IDictionary<string, object?>> QueryAsync(
        string sql,
        IDictionary<string, object?>? parameters = null
    );

    Task<int> ExecuteAsync(
        string sql,
        IDictionary<string, object?>? parameters = null
    );
}
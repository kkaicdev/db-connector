namespace DbConnector.Core;

public interface IDatabaseAdapter : IDisposable
{
    void Connect();
    int Execute(string sql);
    IEnumerable<IDictionary<string, object?>> Query(string sql);
}
using DbConnector.Core;
using DbConnector.Models;
using DbConnector.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace DbConnector.Repositories;

public class UserRepository
{
    private readonly IDatabaseAdapter _db;

    public UserRepository(IDatabaseAdapter db) => _db = db;

    public async Task CreateTableAsync()
    {
        var sql = @"
            CREATE TABLE IF NOT EXISTS users (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL
            );";

        try
        {
            await _db.ExecuteAsync(sql);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error at creating table 'users'.", ex);
        }
        
    }

    public async Task InsertUsersAsync(params string[] names)
    {
        try
        {
            foreach (var name in names)
            {
                var sql = "INSERT INTO users(name) VALUES(@name);";
                await _db.ExecuteAsync(sql, new Dictionary<string, object?> { ["@name"] = name });
            }
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error inserting users.", ex);
        }
    }

    public async IAsyncEnumerable<User> GetUsersAsync(int limit)
    {
        var sql = "SELECT id, name FROM users ORDER BY id LIMIT @limit;";

        await foreach (var row in _db.QueryAsync(sql, new Dictionary<string, object?> { ["@limit"] = limit }))
        {
            User user;
            try
            {
                user = new User
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"]?.ToString() ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving users.", ex);
            }

            yield return user;
        }
    }
}

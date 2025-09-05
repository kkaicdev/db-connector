using DbConnector.Core;
using DbConnector.Models;
using System.Collections.Generic;
using System.Linq;

namespace DbConnector.Repositories;

public class UserRepository
{
    private readonly IDatabaseAdapter _db;

    public UserRepository(IDatabaseAdapter db) => _db = db;

    public void CreateTable()
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS users (
            id SERIAL PRIMARY KEY,
            name TEXT NOT NULL
        );";
        _db.Execute(sql);
    }

    public void InsertUsers(params string[] names)
    {
        foreach (var name in names)
        {
            var sql = $"INSERT INTO users(name) VALUES('{name.Replace("'", "''")}');";
            _db.Execute(sql);
        }
    }

    public IEnumerable<User> GetUsers(int limit)
    {
        var sql = $"SELECT id, name FROM users ORDER BY id LIMIT {limit};";

        foreach (var row in _db.Query(sql))
        {
            yield return new User
            {
                Id = Convert.ToInt32(row["id"]),
                Name = row["name"]?.ToString() ?? string.Empty
            };
        }
    }
}

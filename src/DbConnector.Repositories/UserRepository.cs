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

    public void CreateTable()
    {
        var sql = @"
            CREATE TABLE IF NOT EXISTS users (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL
            );";

        try
        {
            _db.Execute(sql);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error at creating table 'users'.", ex);
        }
        
    }

    public void InsertUsers(params string[] names)
    {
        try
        {
            foreach (var name in names)
            {
                var sql = $"INSERT INTO users(name) VALUES('{name.Replace("'", "''")}');";
                _db.Execute(sql);
            }
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error inserting users.", ex);
        }
    }

    public IEnumerable<User> GetUsers(int limit)
    {
        var sql = $"SELECT id, name FROM users ORDER BY id LIMIT {limit};";

        try
        {
            var results = new List<User>();

            foreach (var row in _db.Query(sql))
            {
                results.Add(new User
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"]?.ToString() ?? string.Empty
                });
            }

            return results;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error retrieving users.", ex);
        }
    }

}

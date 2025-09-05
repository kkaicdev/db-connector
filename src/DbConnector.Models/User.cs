namespace DbConnector.Models;

public record User
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}
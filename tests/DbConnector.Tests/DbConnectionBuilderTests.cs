using DbConnector.Core;
using Xunit;

public class DbConnectionBuilderTests
{
    [Fact]
    public void DbConnectionBuilder_CreateConfig()
    {
        var cfg = new DbConnectionBuilder()
            .WithServer("localhost")
            .WithPort(5432)
            .WithDatabase("demo")
            .WithUser("postgres")
            .WithPassword("postgres")
            .WithTimeout(30)
            .Build();

        Assert.Equal("localhost", cfg.Server);
        Assert.Equal(5432, cfg.Port);
        Assert.Equal("demo", cfg.Database);
        Assert.Equal("postgres", cfg.User);
        Assert.Equal("postgres", cfg.Password);
        Assert.Equal(30, cfg.TimeoutSeconds);
    }
}

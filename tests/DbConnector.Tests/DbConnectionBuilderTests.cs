using Xunit;
using DbConnector.Core;
using System;

namespace DbConnector.Tests
{
    public class DbConnectionBuilderTests
    {
        [Fact]
        public void Build_WithValidParameters_ReturnsConfig()
        {
            var config = DbConnectionBuilder.Create()
                .WithServer("127.0.0.1")
                .WithPort(5432)
                .WithUser("postgres")
                .WithPassword("admin")
                .WithDatabase("testdb")
                .WithTimeout(30)
                .Build();

            Assert.Equal("127.0.0.1", config.Server);
            Assert.Equal(5432, config.Port);
            Assert.Equal("postgres", config.User);
            Assert.Equal("admin", config.Password);
            Assert.Equal("testdb", config.Database);
            Assert.Equal(30, config.TimeoutSeconds);
        }

        [Fact]
        public void Build_MissingRequired_ThrowsException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
                DbConnectionBuilder.Create()
                    .WithUser("")
                    .WithPassword("admin")
                    .WithServer("127.0.0.1")
                    .Build()
            );
            Assert.Contains("User cannot be empty.", ex.Message);
        }
    }
}

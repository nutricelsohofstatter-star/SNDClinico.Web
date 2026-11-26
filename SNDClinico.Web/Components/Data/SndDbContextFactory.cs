
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace SNDClinico.Web.Data;

public class SndDbContextFactory
    : IDesignTimeDbContextFactory<SndDbContext>
{
    public SndDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable(
                "SND_DESIGN_CONNECTION");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var databaseUrl =
                Environment.GetEnvironmentVariable("DATABASE_URL");

            if (!string.IsNullOrWhiteSpace(databaseUrl))
            {
                connectionString = ConverterDatabaseUrl(databaseUrl);
            }
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Conexão do PostgreSQL não configurada para migrações. " +
                "Configure SND_DESIGN_CONNECTION ou DATABASE_URL " +
                "com os dados do PostgreSQL do Render.");
        }

        var options = new DbContextOptionsBuilder<SndDbContext>();

        options.UseNpgsql(connectionString);

        return new SndDbContext(options.Options);
    }

    private static string ConverterDatabaseUrl(string databaseUrl)
    {
        if (!Uri.TryCreate(
            databaseUrl,
            UriKind.Absolute,
            out var uri))
        {
            throw new InvalidOperationException(
                "A variável DATABASE_URL não contém uma URL PostgreSQL válida.");
        }

        var usuarioSenha = uri.UserInfo.Split(':', 2);

        if (usuarioSenha.Length != 2)
        {
            throw new InvalidOperationException(
                "A URL PostgreSQL não contém usuário e senha válidos.");
        }

        var connection = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.IsDefaultPort ? 5432 : uri.Port,
            Database = Uri.UnescapeDataString(
                uri.AbsolutePath.Trim('/')),
            Username = Uri.UnescapeDataString(usuarioSenha[0]),
            Password = Uri.UnescapeDataString(usuarioSenha[1]),
            SslMode = SslMode.Require,
            Pooling = true,
            Timeout = 15,
            CommandTimeout = 30
        };

        return connection.ConnectionString;
    }
}

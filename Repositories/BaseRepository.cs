using System.Data;
using instagram.Settings;
using Npgsql;


namespace instagram.Repositories;

public class BaseRepository
{
    private readonly IConfiguration _configuration;
    public BaseRepository(IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        _configuration = configuration;
    }


    public NpgsqlConnection NewConnection => new NpgsqlConnection(_configuration
        .GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString);
}
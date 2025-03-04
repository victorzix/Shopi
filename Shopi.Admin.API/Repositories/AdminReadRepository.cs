using System.Data;
using Dapper;
using Npgsql;
using Shopi.Admin.API.Interfaces;
using Shopi.Admin.API.Models;
using Shopi.Admin.API.Queries;

namespace Shopi.Admin.API.Repositories;

public class AdminReadRepository : IAdminReadRepository
{
    private readonly IDbConnection _dbConnection;

    public AdminReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
    
    public async Task<AppAdmin?> FilterAdmin(FilterAdminQuery query)
    {
        const string sql =
            "SELECT * FROM \"AppAdmin\" WHERE \"Email\" = @Email OR \"Id\" = @Id OR \"UserId\" = @UserId";
        return await _dbConnection.QueryFirstOrDefaultAsync<AppAdmin>(sql,
            new { query.Email, query.Id, UserId = query.Id });
    }
}
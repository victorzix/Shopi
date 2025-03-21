using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shopi.Admin.Domain.Entities;
using Shopi.Admin.Domain.Interfaces;
using Shopi.Admin.Domain.Queries;

namespace Shopi.Admin.Infrastructure.Repositories;

public class AdminReadRepository : IAdminReadRepository
{
    private readonly IDbConnection _dbConnection;

    public AdminReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
    
    public async Task<AppAdmin?> FilterAdmin(QueryAdmin query)
    {
        const string sql =
            "SELECT * FROM \"AppAdmin\" WHERE \"Email\" = @Email OR \"Id\" = @Id OR \"UserId\" = @UserId";
        return await _dbConnection.QueryFirstOrDefaultAsync<AppAdmin>(sql,
            new { query.Email, query.Id, UserId = query.Id });
    }
}
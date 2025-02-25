using System.Data;
using Dapper;
using Npgsql;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.Repository;

public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<AppCustomer?> FilterClient(FilterCustomerQuery query)
    {
        const string sql =
            "SELECT * FROM \"AppCustomer\" WHERE \"Email\" = @Email OR \"Document\" = @Document OR \"Id\" = @Id OR \"UserId\" = @UserId";
        return await _dbConnection.QueryFirstOrDefaultAsync<AppCustomer>(sql,
            new { query.Email, query.Document, query.Id, UserId = query.Id });
    }
}
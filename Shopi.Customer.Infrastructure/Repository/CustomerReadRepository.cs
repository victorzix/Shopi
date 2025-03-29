using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Infrastructure.Repository;

public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<AppCustomer?> FilterClient(QueryCustomer query)
    {
        const string sql =
            "SELECT * FROM \"AppCustomer\" WHERE \"IsActive\" = TRUE AND \"Email\" = @Email OR \"Document\" = @Document OR \"Id\" = @Id OR \"UserId\" = @UserId";
        return await _dbConnection.QueryFirstOrDefaultAsync<AppCustomer>(sql,
            new { query.Email, query.Document, query.Id, UserId = query.Id });
    }
}
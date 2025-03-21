using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Infrastructure.Repository;

public class AddressReadRepository : IAddressReadRepository
{
    private readonly IDbConnection _dbConnection;

    public AddressReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<IEnumerable<Address?>> List(QueryAddresses query)
    {
        const string sql = "SELECT * FROM \"Addresses\" WHERE \"CustomerId\" = @CustomerId  ";
        return await _dbConnection.QueryAsync<Address>(sql,
            new { query.CustomerId });
    }

    public async Task<Address?> Get(QueryAddress query)
    {
        const string sql = "SELECT * FROM \"Addresses\" WHERE \"CustomerId\" = @CustomerId AND \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Address>(sql, new { query.CustomerId, query.Id });
    }
}
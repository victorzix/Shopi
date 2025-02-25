using System.Data;
using Dapper;
using Npgsql;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.Repository;

public class AddressReadRepository : IAddressReadRepository
{
    private readonly IDbConnection _dbConnection;

    public AddressReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<IEnumerable<Address?>> List(ListAddressesQuery query)
    {
        const string sql = "SELECT * FROM \"Addresses\" WHERE \"CustomerId\" = @CustomerId  ";
        return await _dbConnection.QueryAsync<Address>(sql,
            new { query.CustomerId });
    }

    public async Task<Address?> Get(GetAddressQuery query)
    {
        const string sql = "SELECT * FROM \"Addresses\" WHERE \"CustomerId\" = @CustomerId AND \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Address>(sql, new { query.CustomerId, query.Id });
    }
}
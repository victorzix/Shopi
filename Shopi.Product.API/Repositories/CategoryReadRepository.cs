using System.Data;
using Dapper;
using Npgsql;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Repositories;

public class CategoryReadRepository : ICategoryReadRepository
{
    private readonly IDbConnection _dbConnection;

    public CategoryReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        ;
    }

    public async Task<Category?> Get(Guid id)
    {
        const string sql = "SELECT * FROM \"Categories\" WHERE \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
    }

    public async Task<IReadOnlyCollection<Category>> FilterCategories(FilterCategoriesQuery query)
    {
        const string sql = """
                           SELECT * 
                           FROM "Categories" c
                           WHERE
                                (@Name IS NULL OR c."Name" ILIKE '%' || @Name || '%')
                                AND (@ParentId iS NULL OR c."ParentId" = @ParentId)
                                AND (@Visible IS NULL OR c."Visible" = @Visible)
                           """;
        var parameters = new
        {
            Name = string.IsNullOrEmpty(query.Name) ? null : "%" + query.Name + "%",
            ParentId = query.ParentId,
            Visible = query.Visible,
        };

        return (await _dbConnection.QueryAsync<Category>(sql, parameters)).ToList();
    }
}
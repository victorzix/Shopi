using System.Data;
using Dapper;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Repositories;

public class CategoryReadRepository : ICategoryReadRepository
{
    private readonly IDbConnection _dbConnection;

    public CategoryReadRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Category?> Get(Guid id)
    {
        const string sql = "SELECT * FROM \"Categories\" WHERE \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
    }

    public async Task<IReadOnlyCollection<Category>> List()
    {
        const string sql = "SELECT * FROM \"Categories\"";
        return (await _dbConnection.QueryAsync<Category>(sql)).ToList();
    }

    public async Task<IReadOnlyCollection<Category>> FilterProducts(FilterCategoriesQuery query)
    {
        const string sql = """
                           SELECT * 
                           FROM "Categories" c
                           WHERE
                                (@Name IS nULL OR c."Name" ILIKE '%' || @Name || '%')
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

    public async Task<IReadOnlyCollection<Category>> GetByParentId(Guid parentId)
    {
        const string sql = "SELECT * FROM \"Categories\" WHERE \"ParentId\" = @Parent";
        return (await _dbConnection.QueryAsync<Category>(sql)).ToList();
    }
}
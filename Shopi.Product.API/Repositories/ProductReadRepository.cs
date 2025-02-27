using System.Data;
using Dapper;
using Npgsql;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Repositories;

public class ProductReadRepository : IProductReadRepository
{
    private readonly IDbConnection _dbConnection;

    public ProductReadRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));;
    }

    public async Task<AppProduct?> Get(Guid id)
    {
        var query = "SELECT * FROM \"AppProducts\" WHERE \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<AppProduct>(query, new { Id = id });
    }

    public async Task<IReadOnlyCollection<AppProduct>> FilterProducts(FilterProductsQuery query)
    {
        const string sql = """
                                   SELECT DISTINCT p.* 
                                   FROM AppProducts p
                                   LEFT JOIN AppProductCategories pc ON p.Id = pc.ProductId
                                   LEFT JOIN Reviews r ON p.Id = r.AppProductId
                                   WHERE 
                                       (@CategoryIds IS NULL OR pc.CategoryId IN @CategoryIds)
                                       AND (@Name IS NULL OR p.Name ILIKE '%' || @Name || '%')
                                       AND (@MinPrice IS NULL OR p.Price >= @MinPrice)
                                       AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice)
                                       AND (@Visible IS NULL OR p.Visible = @Visible)
                                       AND (@Manufacturer IS NULL OR p.Manufacturer IN @Manufacturer)
                                   GROUP BY p.Id
                                   ORDER BY 
                                       CASE WHEN @NameOrder = 'name asc' THEN p.Name END ASC,
                                       CASE WHEN @NameOrder = 'name desc' THEN p.Name END DESC,
                                       CASE WHEN @PriceOrder = 'price asc' THEN p.Price END ASC,
                                       CASE WHEN @PriceOrder = 'price desc' THEN p.Price END DESC,
                                       CASE WHEN @ReviewOrder = 'review asc' THEN COALESCE(AVG(r.Rating), 0) END ASC,
                                       CASE WHEN @ReviewOrder = 'review desc' THEN COALESCE(AVG(r.Rating), 0) END DESC
                                 LIMIT @Limit OFFSET @Offset
                           """;


        var parameters = new
        {
            CategoryIds = query.CategoryIds?.Count > 0 ? query.CategoryIds : null,
            Name = string.IsNullOrWhiteSpace(query.Name) ? null : "%" + query.Name + "%",
            MinPrice = query.MinPrice,
            MaxPrice = query.MaxPrice,
            Visible = query.Visible,
            Manufacturer = string.IsNullOrEmpty(query.Manufacturer) ? null : query.Manufacturer,
            NameOrder = query.NameOrder,
            PriceOrder = query.PriceOrder,
            ReviewOrder = query.ReviewOrder,
            Limit = query.PaginationFilter.Limit,
            Offset = (query.PaginationFilter.Page - 1) * query.PaginationFilter.Limit
        };

        return (await _dbConnection.QueryAsync<AppProduct>(sql, parameters)).ToList();
    }
}
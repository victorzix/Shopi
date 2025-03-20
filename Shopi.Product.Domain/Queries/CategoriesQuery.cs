namespace Shopi.Product.Domain.Queries;

public class CategoriesQuery
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool? Visible { get; set; }
    public string? NameOrder { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }

    public CategoriesQuery()
    {
    }

    public CategoriesQuery(string? name, Guid? parentId, bool? visible, string? nameOrder, int limit, int offset)
    {
        Name = name;
        ParentId = parentId;
        Visible = visible;
        NameOrder = nameOrder;
        Limit = limit;
        Offset = offset;
    }
}
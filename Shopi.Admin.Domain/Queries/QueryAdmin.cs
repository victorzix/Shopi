namespace Shopi.Admin.Domain.Queries;

public class QueryAdmin
{
    public string? Email { get; set; }
    public Guid? Id { get; set; }

    public QueryAdmin() { }

    public QueryAdmin(string? email, Guid? id)
    {
        Email = email;
        Id = id;
    }
}
using Shopi.Admin.API.Models;
using Shopi.Admin.API.Queries;

namespace Shopi.Admin.API.Interfaces;

public interface IAdminReadRepository
{
    Task<AppAdmin?> FilterAdmin(FilterAdminQuery query);
}
using SUPMS.Infrastructure.Models;

namespace SUPMS.Infrastructure.Interface.Business
{
    public interface IRolesManager
    {
        Roles GetRoles(int ComanyID, int RoleId);
    }
}

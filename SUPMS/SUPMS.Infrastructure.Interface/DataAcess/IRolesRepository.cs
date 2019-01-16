using SUPMS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Interface.DataAcess
{
    public interface IRolesRepository
    {
        Roles GetRoles(int ComanyID, int RoleId);
    }
}

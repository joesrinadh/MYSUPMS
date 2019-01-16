using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUPMS.Infrastructure.Interface.Business;
using SUPMS.Infrastructure.Interface.DataAcess;
using SUPMS.Infrastructure.Models;

namespace SUPMS.Business
{
    public class RolesManager : BaseManager, IRolesManager
    {
        private IRolesRepository _rolesRepository;

        public RolesManager(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }
        /// <summary>
        /// Get roles
        /// </summary>
        /// <param name="ComanyID"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public Roles GetRoles(int ComanyID, int RoleId)
        {
            Roles roles = new Roles();
            roles = _rolesRepository.GetRoles(ComanyID, RoleId);
            return roles;
        }
    }
}

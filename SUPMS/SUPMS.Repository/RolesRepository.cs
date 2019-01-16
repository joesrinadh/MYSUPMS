using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUPMS.Infrastructure.Interface.DataAcess;
using SUPMS.Infrastructure.Models;
using SUPMS.Infrastructure.Utilities;
using Domain = SUPMS.Infrastructure.Models;
using SUPMS.Repository.Extensions;

namespace SUPMS.Repository
{
    public class RolesRepository:RepositoryBase,IRolesRepository
    {
        private string _connectionString;
        public RolesRepository(SUPMSEntities dbContext) : base(dbContext)
        {
            _connectionString = ConnectionHelper.GetConnectionString("SUPMSDBConnection");
        }
        public Domain.Roles GetRoles(int ComanyID, int RoleId)
        {
            Domain.Roles rolesDomainModel = new Domain.Roles();
            if (ComanyID > 0 && RoleId > 0)
            {
                ROLE rolesDataModel = SUPMSDbContext.ROLES.FirstOrDefault(a => a.COMPANYID == ComanyID && a.ROLEID == RoleId);
                if (rolesDataModel != null)
                {
                    rolesDomainModel = rolesDataModel.ToDomainModel();
                }
            }
            return rolesDomainModel;
        }
    }
}

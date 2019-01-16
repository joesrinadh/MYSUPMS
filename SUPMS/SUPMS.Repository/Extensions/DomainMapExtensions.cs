using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = SUPMS.Infrastructure.Models;

namespace SUPMS.Repository.Extensions
{
    internal static class DomainMapExtensions
    {
        internal static Domain.Roles ToDomainModel(this Repository.ROLE roleDataModel)
        {
            if (roleDataModel == null)
                return null;

            Domain.Roles roles = new Domain.Roles();
            roles.RoleId = roleDataModel.ROLEID;
            roles.Name = roleDataModel.NAME;
            roles.Description = roleDataModel.DESCRIPTION;
            roles.CompanyId = roleDataModel.COMPANYID;
            roles.IsSystemRole = roleDataModel.ISSYSTEMROLE;
            roles.FeaturesXML = roleDataModel.FEATURESXML;
            roles.CreatedBy = roleDataModel.CREATEDBY;
            roles.CreatedDate = roleDataModel.CREATEDDATE;
            roles.ModifiedBy = roleDataModel.MODIFIEDBY;
            roles.ModifiedDate = roleDataModel.MODIFIEDDATE;

            return roles;
        }
    }
}

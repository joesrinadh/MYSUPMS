using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPMS.Repository
{
    public class RepositoryBase
    {
        protected SUPMSEntities SUPMSDbContext { private set; get; }
        public RepositoryBase(SUPMSEntities dbContext)
        {
            SUPMSDbContext = dbContext;
        }

    }
}
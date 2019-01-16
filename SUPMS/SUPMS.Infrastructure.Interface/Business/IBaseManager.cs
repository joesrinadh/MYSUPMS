using SUPMS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Interface.Business
{
    public interface IBaseManager
    {
        Context BusinessContext { get; set; }
    }
}

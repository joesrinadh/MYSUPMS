using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUPMS.Infrastructure.Interface.Business;
using Microsoft.Practices.Unity;
using SUPMS.Infrastructure.Models;
using Unity;
using Unity.Attributes;

namespace SUPMS.Business
{
    public class BaseManager : IBaseManager, IDisposable
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public Context BusinessContext { get; set; } = Context.New;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Dispose(bool disposing)
        {
            if (disposing && BusinessContext != null)
            {
                BusinessContext = null; ;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~BaseManager()
        {
            Dispose(false);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Models
{
    public class ResultDTO
    {
        public ActionType ActionName { get; set; }
        public Exception ErrorMessage { get; set; }
        public bool IsPageRefresh { get; set; }
        public string Message { get; set; }
        public Response ResultType { get; set; }
        public object ReturnObject { get; set; }
        public long ReturnObjId { get; set; }
    }
}

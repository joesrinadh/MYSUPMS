using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Utilities
{
    public class JSTreeElement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasChildren { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public decimal ClosingAmount { get; set; }
        public List<JSTreeElement> children { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Models
{
    public class HTMLGridClass
    {
        public string GridData
        {
            get;
            set;
        }
        public string Action
        {
            get;
            set;
        }
        public string Controller
        {
            get;
            set;
        }
        public int FeatureID
        {
            get;
            set;
        }
        public int ComanyID
        {
            get;
            set;
        }
        public int RowCount
        {
            get;
            set;
        }
        public int PageSize
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public int IsNewEnable
        {
            get;
            set;
        }
        public int IsAssignEnable
        {
            get;
            set;
        }
        public int IsCloseEnable
        {
            get;
            set;
        }
        public int IsAproveEnable
        {
            get;
            set;
        }
        public int IsRejectEnable
        {
            get;
            set;
        }
        public int IsRenewalEnable
        {
            get;
            set;
        }
        public int IsTerminateEnable
        {
            get;
            set;
        }
        public int IsLedgerEnable
        {
            get;
            set;
        }
        public int IsActivateEnable
        {
            get;
            set;
        }
        public int IsDeActivateEnable
        {
            get;
            set;
        }
        public int IsActivated { get; set; }
    }
}

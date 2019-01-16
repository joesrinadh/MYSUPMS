using SUPMS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Featureid"></param>
        /// <param name="ContactType"></param>
        /// <returns></returns>
        //public ActionResult CommonGrid(int Featureid, int? ContactType)
        //{
        //    try
        //    {
        //        HTMLGridClass objClass = new HTMLGridClass();
        //        int isDelete = 0, isedit = 0, isnewassign = 0;
        //        if (UserSession != null)
        //        {
        //            objClass.IsNewEnable = 0;
        //            objClass.IsAssignEnable = 0;
        //            objClass.IsCloseEnable = 0;
        //            objClass.IsAproveEnable = 0;
        //            if (IsActionAssigned(UserSession.RoleXML, "New", Featureid.ToString(), 1) == 1)
        //            {
        //                isnewassign = 1;
        //                objClass.IsNewEnable = 1;
        //            }
        //            else
        //                isnewassign = 0;
        //            if (IsActionAssigned(UserSession.RoleXML, "Edit", Featureid.ToString(), 2) == 1)
        //            {
        //                isedit = 1;
        //            }
        //            else
        //                isedit = 0;
        //            if (IsActionAssigned(UserSession.RoleXML, "Delete", Featureid.ToString(), 3) == 1)
        //            {
        //                isDelete = 1;
        //            }
        //            else
        //                isDelete = 0;
        //            if (IsActionAssigned(UserSession._RoleXML, "Assign", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsAssignEnable = 1;
        //            }
        //            else
        //                objClass.IsAssignEnable = 0;

        //            if (IsActionAssigned(UserSession.RoleXML, "Close", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsCloseEnable = 1;
        //            }
        //            else
        //                objClass.IsCloseEnable = 0;

        //            if (IsActionAssigned(UserSession.RoleXML, "Approve", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsAproveEnable = 1;
        //            }
        //            else
        //                objClass.IsAproveEnable = 0;


        //            if (IsActionAssigned(UserSession.RoleXML, "Renewal", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsRenewalEnable = 1;
        //            }
        //            else
        //                objClass.IsRenewalEnable = 0;
        //            if (IsActionAssigned(UserSession.RoleXML, "Terminate", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsTerminateEnable = 1;
        //            }
        //            else
        //                objClass.IsTerminateEnable = 0;
        //            if (IsActionAssigned(UserSession.oleXML, "Ledger", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsLedgerEnable = 1;
        //            }
        //            else
        //                objClass.IsLedgerEnable = 0;


        //            if (IsActionAssigned(UserSession.RoleXML, "Activate", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsActivateEnable = 1;
        //            }
        //            else
        //                objClass.IsActivateEnable = 0;
        //            if (IsActionAssigned(UserSession.RoleXML, "DeActivate", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsDeActivateEnable = 1;
        //            }
        //            else
        //                objClass.IsDeActivateEnable = 0;
        //            if (IsActionAssigned(UserSession.RoleXML, "Reject", Featureid.ToString(), 3) == 1)
        //            {
        //                objClass.IsRejectEnable = 1;
        //            }
        //            else
        //                objClass.IsRejectEnable = 0;

        //            FillGridDropdowns(UserSession.ComanyID, Featureid);
        //            objClass.FeatureID = Featureid;
        //            objClass.ComanyID = UserSession.ComanyID;
        //            string[] arr = GetGridData(Featureid, 1, 20, "", "", isDelete, isedit, ContactType, 0, DefaultViewSeqno);
        //            if (arr.Length > 0 && arr[0].ToString() != "")
        //                objClass.GridData = arr[0];
        //            else
        //                objClass.GridData = "";


        //            if (arr.Length > 1 && arr[1].ToString() != "")
        //                objClass.RowCount = Convert.ToInt32(arr[1]);
        //            else
        //                objClass.RowCount = 0;

        //            objClass.PageSize = 1;
        //            objClass.Title = Featureid == 100 ? "Properties" : "";
        //            string strAction = "";
        //            string strController = "";
        //            if (ReturnActions(Featureid, ref strAction, ref strController))
        //            {
        //                objClass.Action = strAction;
        //                objClass.Controller = strController;
        //            }

        //            return View("CommonGrid", objClass);
        //        }

        //        return View("CommonGrid", null);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
    }
}
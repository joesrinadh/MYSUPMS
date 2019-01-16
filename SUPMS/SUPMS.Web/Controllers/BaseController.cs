using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using SUPMS.Infrastructure.AsyncLogger;
using SUPMS.Infrastructure.Utilities;
using SUPMS.Infrastructure.Models;
using SUPMS.Infrastructure.Interface.Business;
namespace SUPMS.Web.Controllers
{
    public class BaseController : Controller
    {
        private IRolesManager _rolesManager;
        /// <summary>
        /// SessionList
        /// </summary>
        public Dictionary<string, object> SessionList
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SessionList"] != null)
                {
                    return (Dictionary<string, object>)System.Web.HttpContext.Current.Session["SessionList"];
                }
                else
                {
                    return new Dictionary<string, object>();
                }
            }
            set
            {

                System.Web.HttpContext.Current.Session["SessionList"] = value;
            }
        }
        /// <summary>
        /// User Session
        /// </summary>
        public SessionManager UserSession
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SUPMSSession"] != null)
                {
                    return (SessionManager)System.Web.HttpContext.Current.Session["SUPMSSession"];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
       /// 
       /// </summary>
        public BaseController([Dependency] IRolesManager rolesManager)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - BaseController.BaseController" + " Constructor Begins ", LogMessageType.Informational);
                _rolesManager = rolesManager;
                if (UserSession == null || UserSession.HeaderLogoPath == "")
                {
                    RedirectToAction("Index", "Login");
                }
                else
                { 
                    try
                    {
                        ViewBag.HeaderLogoPath = UserSession.HeaderLogoPath;
                        ViewBag.BaseProfilePic = UserSession.ProfilePicPath;
                    }
                    catch (Exception ex)
                    {
                        AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - BaseController.BaseController" + " Exception :  " + ex.StackTrace.ToString(), LogMessageType.Error);

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Login");
            }
        }
        bool bArea;
        bool loopBreadCrumb;
        string FinalBreadCrumbForPage;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            bArea = false;
            loopBreadCrumb = true;
            base.OnActionExecuting(ctx);
            if (UserSession == null && ctx.ActionDescriptor.ControllerDescriptor.ControllerName == "common" && ctx.ActionDescriptor.ActionName == "TempAction")
            {
                SessionManager userDetails = new SessionManager();
                userDetails.UserId = Convert.ToInt32(2);
                userDetails.UserType = "A";
                userDetails.ComanyID = Convert.ToInt32(1);
                userDetails.RoleId = 1;
                userDetails.LanguageId = 1;
                Session["ComanyID"] = userDetails.ComanyID;
                Session["USERTYPE"] = userDetails.UserType;

                Session["UserDetails"] = userDetails;
                System.Web.HttpContext.Current.Session["SUPMSSession"] = userDetails;

                ctx.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        Controller = "Login",
                        Action = "Index"
                    }));
            }
            if (UserSession == null)
            {
                ctx.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        Controller = "Login",
                        Action = "Index"
                    }));
            }
            else
            {
                string actionName = ctx.ActionDescriptor.ActionName;
                string controllerName = ctx.ActionDescriptor.ControllerDescriptor.ControllerName;
                bArea = ctx.RouteData.DataTokens.ContainsKey("AREA");
                string featureId = "";
                string areaName = "";
                if (bArea)
                    areaName = ctx.RouteData.DataTokens.FirstOrDefault(s => s.Key.ToUpper() == "AREA").Value.ToString();
                bool bContains = ctx.ActionParameters.ContainsKey("FEATUREID");
                if (bContains)
                {
                    featureId = ctx.ActionParameters.FirstOrDefault(s => s.Key.ToUpper() == "FEATUREID").Value.ToString();
                }
                bool bContainsId = ctx.ActionParameters.ContainsKey("Id");
                if (bContainsId)
                {
                    if (ctx.ActionParameters.FirstOrDefault(s => s.Key.ToUpper() == "ID").Value == null)
                    {
                        ViewBag.EntityId = "";
                    }
                    else
                    {
                        string EntityId = ctx.ActionParameters.FirstOrDefault(s => s.Key.ToUpper() == "ID").Value.ToString();
                        if (string.IsNullOrEmpty(EntityId))
                        {
                            ViewBag.EntityId = EntityId;
                        }
                        else
                            ViewBag.EntityId = "";
                    }
                }
                ViewBag.ReturnUrl = "";
                if (ViewBag.IsSingle != "1")
                {
                    ViewBag.IsSingle = "0";
                }
                bool bContainsreturnUrl = false;
                if (ctx.RequestContext.HttpContext.Request.QueryString["returnUrl"] != null)
                {
                    if (!string.IsNullOrEmpty(ctx.RequestContext.HttpContext.Request.QueryString["returnUrl"].ToString()))
                    {
                        bContainsreturnUrl = true;
                    }

                }
                if (bContainsreturnUrl)
                {
                    string returnUrl = ctx.RequestContext.HttpContext.Request.QueryString["returnUrl"].ToString();
                    if (!returnUrl.Contains("~"))
                    {
                        returnUrl = returnUrl.Insert(0, "~");
                    }
                    ViewBag.ReturnUrl = returnUrl;
                }
                GetMenu(actionName, controllerName, areaName, featureId);
            }
        }
        public int DefaultViewSeqno = -1;
        /// <summary>
        /// To Get Menu for Roles
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public string GetMenu(string actionName, string controllerName, string areaName, string featureId)
        {
            if (UserSession != null)
            {
                string strFeatureXML = "";
                Roles roles = _rolesManager.GetRoles(UserSession.ComanyID, UserSession.RoleId);

                if (roles != null)
                {
                        strFeatureXML = roles.FeaturesXML.ToString();
                }
                StringBuilder sb = new StringBuilder();
                StringBuilder TopMenus = new StringBuilder();
                string ReleaseMode = System.Configuration.ConfigurationManager.AppSettings["VirtualPath"];
                string AppPath = string.Empty;
                if (ReleaseMode == "false")
                    AppPath = "/";//System.Web.HttpContext.Current.Request.ApplicationPath;
                else
                    AppPath = System.Web.HttpContext.Current.Request.ApplicationPath + "/";

                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/MainMenu.xml")))
                {
                    string _MenuPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/MainMenu.xml");
                    XmlDocument Xdoc = new XmlDocument();
                    Xdoc.Load(_MenuPath);
                    XmlNodeList xNode = Xdoc.SelectNodes("/MENUITEMS/MENUITEM");
                    sb.Append("");
                    string strMenuId = "HeaderTab";
                    int headercnt = 1;

                    foreach (XmlNode item in xNode)
                    {
                        if (item != null)
                        {
                            strMenuId = "HeaderTab" + headercnt;
                            sb.Append(ReturnHeaderTab(strFeatureXML, item, AppPath, strMenuId, headercnt, actionName, controllerName, areaName, featureId, TopMenus));
                            headercnt++;
                        }
                    }
                }
                ViewBag.MainMenu = sb.ToString();
                return sb.ToString();
            }
            else
                return "";
        }

        string ReturnHeaderTab(string strFeatureXML, XmlNode xNode, string AppPath, string headerid, int headercnt, string actionName, string controllerName, string areaName, string FeatureId, StringBuilder TopMenus)
        {
            try
            {
                string str = "";

                string strbreadcrumb = "<li class='nav-item'><a class='nav-link' href='/Home/" + ReturnDashboardLink() + "' ><i class='icon-speedometer'></i>Home</a>";
                if (CacheManager.Instance == null)
                {

                    if (IsActionAssigned(strFeatureXML, "ISALLOW", (xNode.Attributes["FEATUREID"] != null && xNode.Attributes["FEATUREID"].Value != "" ?
                        xNode.Attributes["FEATUREID"].Value : "0"), 2) == 1)
                    {


                        if (xNode.ChildNodes.Count > 0)
                        {
                            str = "<li class='nav-item nav-dropdown'>";
                            str += "<a class='nav-link nav-dropdown-toggle' href='" + (xNode.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                (AppPath + xNode.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(xNode.Attributes["FEATUREID"].Value, xNode.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + xNode.Attributes["ICON"].Value + "'></i>";
                            str += xNode.Attributes["TEXT"].Value + " </a>";
                            for (int i = 0; i < xNode.ChildNodes.Count; i++)
                            {
                                str += "<ul class='nav-dropdown-items'>";
                                foreach (XmlNode item in xNode.ChildNodes[i])
                                {
                                    if (IsActionAssigned(strFeatureXML, "ISALLOW", (item.Attributes["FEATUREID"] != null && item.Attributes["FEATUREID"].Value != "" ?
                        item.Attributes["FEATUREID"].Value : "0"), 2) == 1)
                                    {
                                        str += "<li class='nav-item'>";
                                        str += "<a class='nav-link' href='" + (item.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                            (AppPath + item.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(item.Attributes["FEATUREID"].Value, item.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + item.Attributes["ICON"].Value + "'></i>";
                                        str += GetMenuKeys(item.Attributes["TEXT"].Value) + " </a></li>";
                                    }
                                }
                                str += "</ul>";
                            }
                        }
                        else
                        {
                            str = "<li class='nav-item'>";
                            str += "<a class='nav-link' href='" + (xNode.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                (AppPath + xNode.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(xNode.Attributes["FEATUREID"].Value, xNode.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + xNode.Attributes["ICON"].Value + "'></i>";
                            str += xNode.Attributes["TEXT"].Value + " </a>";
                        }
                        str += " </li>";
                    }
                }
                else
                {
                    if (IsActionAssigned(strFeatureXML, "ISALLOW", (xNode.Attributes["FEATUREID"] != null && xNode.Attributes["FEATUREID"].Value != "" ?
                        xNode.Attributes["FEATUREID"].Value : "0"), 2) == 1)
                    {
                        string strInnerhtml = "";
                        string strMainHtml = "";
                        int iCount = 0;
                        bool InnerCurrent = false;
                        bool OuterCurrent = false;
                        string strInnerBreadCrublink = "";
                        if (actionName.ToUpper().Contains("CommonGrid"))
                        {
                            if (xNode.Attributes["CONTROLLER"].Value.ToUpper() == areaName.ToUpper() + "/" + controllerName.ToUpper() && xNode.Attributes["ACTION"].Value.ToUpper() == actionName.ToUpper() + "?FEATUREID=" + FeatureId.ToUpper())
                            {
                                OuterCurrent = true;
                                ViewBag.Title = GetMenuKeys(xNode.Attributes["TEXT"].Value);

                            }
                        }
                        else
                        {
                            if (xNode.Attributes["CONTROLLER"].Value.ToUpper() == areaName.ToUpper() + "/" + controllerName.ToUpper() && xNode.Attributes["ACTION"].Value.ToUpper() == actionName.ToUpper())
                            {
                                OuterCurrent = true;
                                TempData["EditController"] = controllerName;
                                TempData["EditArea"] = areaName;
                                TempData["EditAction"] = actionName;
                                TempData["EditFeatureId"] = xNode.Attributes["FEATUREID"].Value;
                            }
                        }

                        string iconClass = xNode.Attributes["ICON"].Value;
                        bool hasChildrens = false;
                        if (xNode.ChildNodes.Count > 0)
                        {
                            hasChildrens = true;
                            str = "<li class='nav-item nav-dropdown'>";
                            str += "<a  class='nav-link nav-dropdown-toggle' href='" + (xNode.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                (AppPath + xNode.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(xNode.Attributes["FEATUREID"].Value, xNode.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + iconClass + "'></i>";
                            strMainHtml += "</a>";
                            iCount = xNode.ChildNodes.Count;
                            for (int i = 0; i < xNode.ChildNodes.Count; i++)
                            {

                                strInnerhtml += "<ul class='nav-dropdown-items'>";
                                foreach (XmlNode item in xNode.ChildNodes[i])
                                {
                                    bool HasInnerCurrent = false;
                                    if (IsActionAssigned(strFeatureXML, "ISALLOW", (item.Attributes["FEATUREID"] != null && item.Attributes["FEATUREID"].Value != "" ?
                            item.Attributes["FEATUREID"].Value : "0"), 2) == 1)
                                    {
                                        if (actionName.Contains("CommonGrid"))
                                        {
                                            if (item.Attributes["CONTROLLER"].Value.ToUpper() == (bArea ? areaName.ToUpper() + "/" : "") + controllerName.ToUpper() && item.Attributes["ACTION"].Value.ToUpper() == actionName.ToUpper() + "?FEATUREID=" + FeatureId.ToUpper())
                                            {
                                                InnerCurrent = true;
                                                HasInnerCurrent = true;
                                                ViewBag.Title = GetMenuKeys(item.Attributes["TEXT"].Value);

                                            }

                                        }
                                        else
                                        {
                                            if (item.Attributes["CONTROLLER"].Value.ToUpper() == areaName.ToUpper() + "/" + controllerName.ToUpper() && item.Attributes["ACTION"].Value.ToUpper() == actionName.ToUpper())
                                            {
                                                if (!HasInnerCurrent)
                                                {
                                                    InnerCurrent = true;
                                                    HasInnerCurrent = true;
                                                    ViewBag.Title = GetMenuKeys(item.Attributes["TEXT"].Value);
                                                    TempData["EditController"] = item.Attributes["EDITCONTROLLER"].Value;
                                                    TempData["EditArea"] = item.Attributes["EDITAREA"].Value;
                                                    TempData["EditAction"] = item.Attributes["EDITACTION"].Value;
                                                    TempData["EditFeatureId"] = item.Attributes["FEATUREID"].Value;
                                                }
                                            }
                                            else if (item.Attributes["EDITAREA"].Value.ToUpper() == areaName.ToUpper() && item.Attributes["EDITCONTROLLER"].Value.ToUpper() == controllerName.ToUpper() && item.Attributes["EDITACTION"].Value.ToUpper() == actionName.ToUpper())
                                            {
                                                if (!HasInnerCurrent)
                                                {
                                                    InnerCurrent = true;
                                                    HasInnerCurrent = true;
                                                    ViewBag.Title = GetMenuKeys(item.Attributes["TEXT"].Value);
                                                    TempData["EditController"] = item.Attributes["EDITCONTROLLER"].Value;
                                                    TempData["EditArea"] = item.Attributes["EDITAREA"].Value;
                                                    TempData["EditAction"] = item.Attributes["EDITACTION"].Value;
                                                    TempData["EditFeatureId"] = item.Attributes["FEATUREID"].Value;
                                                }
                                            }

                                        }
                                        string iconInnerClass = item.Attributes["ICON"].Value;
                                        string strClass = "";
                                        if (HasInnerCurrent == true)
                                        {
                                            strClass = "active";
                                            strInnerBreadCrublink = "<li class='nav-item'><a class='nav-link' href='" + (item.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                                (AppPath + item.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(item.Attributes["FEATUREID"].Value, item.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + iconInnerClass + "'></i>" + GetMenuKeys(item.Attributes["TEXT"].Value) + "</a></li>";
                                        }
                                        strInnerhtml += "<li class='nav-item'>";
                                        strInnerhtml += "<a class='nav-link' href='" + (item.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                            (AppPath + item.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(item.Attributes["FEATUREID"].Value, item.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + iconInnerClass + "'></i>";
                                        strInnerhtml += GetMenuKeys(item.Attributes["TEXT"].Value) + "</a></li>";
                                        strClass = "";
                                    }
                                }
                                strInnerhtml += "</ul>";
                            }
                        }
                        else
                        {
                            str = "<li class='nav-item'>";
                            str += "<a  class='nav-link' href='" + (xNode.Attributes["CONTROLLER"].Value.ToString() != "" ?
                                (AppPath + xNode.Attributes["CONTROLLER"].Value + ("/") + ReturnHomeAction(xNode.Attributes["FEATUREID"].Value, xNode.Attributes["ACTION"].Value)) : "javascript::") + "'><i class='" + iconClass + "'></i>";
                            strMainHtml += "</a>";
                            hasChildrens = false;
                        }
                        str += xNode.Attributes["TEXT"].Value + "";

                        str += strMainHtml;

                        str += strInnerhtml;

                        str += "</li>";

                    }
                }
                return str;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string ReturnDashboardLink()
        {
            if (UserSession.UserType == "C" && UserSession.ContactType == 54)
                return "TDashboard";
            else if (UserSession.UserType == "C" && UserSession.ContactType == 55)
                return "VDashboard";
            else if (UserSession.UserType == "C" && UserSession.ContactType == 53)
                return "OWDashboard";
            else if (UserSession.UserType == "C" && UserSession.ContactType == 56)
                return "OTDashboard";
            else if (UserSession.UserType == "C" && UserSession.ContactType == 57)
                return "Dashboard";
            return "DocumentCommonGrid";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FeatureID"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        string ReturnHomeAction(string FeatureID, string Action)
        {
            if (FeatureID == "10")
            {
                SessionManager objVeritySession = (SessionManager)System.Web.HttpContext.Current.Session["SUPMSSession"];

                if (objVeritySession != null)
                {
                    if (UserSession.UserType == "C" && UserSession.ContactType == 54)
                        return "TDashboard";
                    else if (UserSession.UserType == "C" && UserSession.ContactType == 55)
                        return "VDashboard";
                    else if (UserSession.UserType == "C" && UserSession.ContactType == 53)
                        return "OWDashboard";
                    else if (UserSession.UserType == "C" && UserSession.ContactType == 56)
                        return "OTDashboard";
                    else if (UserSession.UserType == "C" && UserSession.ContactType == 57)
                        return "Dashboard";
                    else
                        return Action;

                }
            }
            return Action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuText"></param>
        /// <param name="ReturnTextIfEmpty"></param>
        /// <returns></returns>
        public string GetMenuKeys(string menuText, bool ReturnTextIfEmpty = true)
        {
            string strText = "";
            if (CacheManager.Instance != null)
            {
                string trimmedtext = menuText.Trim().Replace(" ", string.Empty);
                Object objText = CacheManager.Instance.GetResource("Menu." + trimmedtext);
                if (objText != null)
                {
                    strText = objText.ToString();
                }
            }
            if (ReturnTextIfEmpty && string.IsNullOrEmpty(strText))
            {
                return menuText;
            }
            return strText;

        }
        /// <summary>
        /// IsActionAssigned
        /// </summary>
        /// <param name="strFeatureXML"></param>
        /// <param name="_Key"></param>
        /// <param name="FeatureID"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public int IsActionAssigned(string strFeatureXML, string _Key, string FeatureID, int Index)
        {

            if (!string.IsNullOrEmpty(strFeatureXML))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(strFeatureXML);
                if (xDoc != null)
                {
                    XmlNodeList xNodeList = xDoc.SelectNodes("/SUPMSResponsibilities/Object");

                    foreach (XmlNode item in xNodeList)
                    {
                        if (item.ChildNodes[1].Name == "ID" && item.ChildNodes[1].InnerText == FeatureID)
                        {
                            for (int i = 0; i < item.ChildNodes.Count; i++)
                            {
                                if (item.ChildNodes[i].Name.ToUpper() == _Key.ToUpper())
                                {
                                    if (item.ChildNodes[2].InnerText == "1")
                                        return 1;
                                    else
                                        return 0;
                                }
                            }

                            for (int i = 0; i < item.ChildNodes[3].ChildNodes.Count; i++) //3 index for actions
                            {
                                if (item.ChildNodes[3].ChildNodes[i].Name.ToUpper() == _Key.ToUpper())
                                {
                                    if (item.ChildNodes[3].ChildNodes[i].InnerText == "1")
                                        return 1;
                                    else
                                        return 0;
                                }
                            }
                        }
                    }

                }

            }
            return 0;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="CompanyWBS"></param>
        ///// <param name="FID"></param>
        //public void FillGridDropdowns(int CompanyWBS, int FID)
        //{
        //    switch (FID)
        //    {
        //        case 301:
        //            List<SelectListItem> filter = new List<SelectListItem>();
        //            filter = FillDropDown(Modules.TUsers, 0, false);
        //            var filterNames = new SelectList(filter, "Value", "Text");
        //            ViewData["FitlerView"] = filterNames;
        //            break;
        //        default:
        //            break;
        //    }


        //    //here we are getting viewnames from featuregrid
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    ArrayList obj = new ArrayList();
        //    obj.Add(FID);
        //    obj.Add(UserSession.ComanyID);
        //    DataSet dsetViewNames = VerityHelper.GetViewNamesByFID(obj);
        //    if (dsetViewNames != null && dsetViewNames.Tables.Count > 0)
        //    {
        //        for (int i = 0; i < dsetViewNames.Tables[0].Rows.Count; i++)
        //        {
        //            if (i == 0)
        //                DefaultViewSeqno = Convert.ToInt32(dsetViewNames.Tables[0].Rows[i]["ViewSeqno"]);
        //            items.Add(new SelectListItem { Text = dsetViewNames.Tables[0].Rows[i]["ViewName"].ToString(), Value = dsetViewNames.Tables[0].Rows[i]["ViewSeqno"].ToString() });
        //        }
        //    }
        //    var ViewNames = new SelectList(items, "Value", "Text");
        //    ViewData["ViewNames"] = ViewNames;

        //    obj = new ArrayList();
        //    obj.Add(DefaultViewSeqno);//SEQNO
        //    obj.Add(FID);
        //    obj.Add(0);
        //    obj.Add(10);
        //    obj.Add(""); obj.Add("");
        //    obj.Add(CompanyWBS);
        //    obj.Add(UserSession.ROLEID);
        //    obj.Add(0);
        //    obj.Add(0);
        //    obj.Add("");
        //    obj.Add("");
        //    obj.Add(0);
        //    DataSet dset = VerityHelper.GetViewDetailByFeature(obj);

        //    List<SelectListItem> items2 = new List<SelectListItem>() { new SelectListItem() { Text = null, Value = null } };
        //    items2.Add(new SelectListItem { Text = "", Value = "" });
        //    var ViewNames2 = new SelectList(items2, "Value", "Text");
        //    ViewData["SelectedColumns"] = new SelectList(ViewNames2, "Value", "Text");
        //    if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
        //    {
        //        List<SelectListItem> tenants = new List<SelectListItem>();
        //        tenants.Add(new SelectListItem() { Text = "Main", Value = "1" });
        //        ViewData["Tenant"] = new SelectList(tenants, "Value", "Text");
        //        items = new List<SelectListItem>();
        //        DataView db = dset.Tables[0].DefaultView;
        //        //db.RowFilter = "ViewSeqno=" + DefaultViewSeqno;
        //        if (db.Count > 0)
        //        {
        //            string[] arrAliasColumns = null;
        //            string[] arrTablesColumns = null;
        //            string[] arrColumnsType = null;
        //            arrAliasColumns = db[0]["COLUMNS"].ToString().Split('~');
        //            arrTablesColumns = db[0]["SELECTEDCOLUMNS"].ToString().Split('~');
        //            arrColumnsType = db[0]["COLUMNTYPE"].ToString().Split('~');
        //            if (arrAliasColumns != null && arrAliasColumns.Length > 0)
        //            {
        //                for (int i = 1; i < arrAliasColumns.Length; i++)
        //                {
        //                    items.Add(new SelectListItem
        //                    {
        //                        Text = GetLabelkeys(arrAliasColumns[i].ToString()),
        //                        Value = arrTablesColumns[i] != null ? arrTablesColumns[i].ToString() : ""
        //                    });

        //                }
        //                var SelectedColumns = new SelectList(items, "Value", "Text");
        //                ViewData["SelectedColumns"] = SelectedColumns;
        //            }
        //        }

        //    }
        //    else
        //    {
        //        List<SelectListItem> tenants = new List<SelectListItem>();
        //        tenants.Add(new SelectListItem() { Text = "Main", Value = "1" });
        //        ViewData["Tenant"] = new SelectList(tenants, "Value", "Text");
        //    }
        //    ViewData["DefaultViewSeqno"] = DefaultViewSeqno;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="objModule"></param>
        ///// <param name="ParentId"></param>
        ///// <param name="insertSelect"></param>
        ///// <returns></returns>
        //public List<SelectListItem> FillDropDown(Modules objModule, int ParentId = 0, bool insertSelect = true)
        //{
        //    List<SelectListItem> dropdownlist = new List<SelectListItem>();
        //    int iId = (int)objModule;
        //    switch (objModule)
        //    {
        //        case Modules.TUsers:
        //            dropdownlist = new List<SelectListItem>() { };
        //            DataSet dset = VerityHelper.GetAllUsers();
        //            if (dset != null && dset.Tables.Count > 0)
        //            {
        //                if (dset.Tables[0].Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
        //                    {
        //                        dropdownlist.Add(new SelectListItem()
        //                        {
        //                            Text = ReturnString("FIRSTNAME", dset, i) + ' ' + ReturnString("LASTNAME", dset, i),
        //                            Value = ReturnString("USERID", dset, i),
        //                            Selected = false
        //                        });
        //                    }
        //                }
        //            }
        //            break;
        //        case Modules.TLanguage:
        //            dropdownlist = new List<SelectListItem>() { };
        //            dropdownlist = GetLanguagesList();
        //            break;
        //        case Modules.TaskStatus:
        //            dropdownlist = new List<SelectListItem>() { };
        //            dropdownlist = GetAllStatusesBasedOnLookup((int)Modules.TaskStatus, UserSession.USERID, UserSession.ComanyID);
        //            break;

        //        case Modules.TRoles:
        //            dropdownlist = new List<SelectListItem>() { };
        //            dropdownlist = GetRolesList();
        //            break;
        //        case Modules.TUSERGROUPS:
        //            dropdownlist = new List<SelectListItem>() { };
        //            dropdownlist = GetUserGrops();
        //            break;
        //        case Modules.EventTypes:
        //            dropdownlist = new List<SelectListItem>() { };
        //            dropdownlist = GetEventTypes();
        //            break;
        //        default:
        //            break;
        //    }
        //    if (insertSelect)
        //    {
        //        dropdownlist.Insert(0, new SelectListItem { Text = "Select..", Value = "" });
        //    }
        //    return dropdownlist;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="FeatureID"></param>
        ///// <param name="StartRowIndex"></param>
        ///// <param name="Maximumrows"></param>
        ///// <param name="FilterColumn"></param>
        ///// <param name="FilterCondition"></param>
        ///// <param name="isDelete"></param>
        ///// <param name="isEdit"></param>
        ///// <param name="ContactType"></param>
        ///// <param name="fitlerId"></param>
        ///// <param name="ViewSeqno"></param>
        ///// <param name="SortBy"></param>
        ///// <param name="SortOrder"></param>
        ///// <returns></returns>
        //public string[] GetGridData(int FeatureID, int StartRowIndex, int Maximumrows, string FilterColumn, string FilterCondition, int isDelete,
        //   int isEdit, int? ContactType, int? fitlerId, int ViewSeqno, string SortBy = "", string SortOrder = "")
        //{
        //    try
        //    {
        //        if (!fitlerId.HasValue)
        //        {
        //            fitlerId = 0;
        //        }
        //        string[] arr = new string[2];
        //        StringBuilder SbData = new StringBuilder();
        //        ArrayList objList = new ArrayList();
        //        if (ViewSeqno == 0)
        //        {
        //            ViewSeqno = DefaultViewSeqno;
        //        }
        //        objList.Add(ViewSeqno);
        //        objList.Add(FeatureID);
        //        int num = (StartRowIndex - 1) * Maximumrows + 1;
        //        objList.Add(num); objList.Add(Maximumrows);
        //        objList.Add(FilterColumn);
        //        objList.Add(FilterCondition);
        //        objList.Add(UserSession.ComanyID);
        //        objList.Add(UserSession.USERID);
        //        objList.Add(fitlerId);
        //        objList.Add(ContactType == null ? 0 : ContactType);
        //        objList.Add(SortBy);
        //        objList.Add(SortOrder);
        //        objList.Add(0);
        //        //DataSet Dset = VerityHelper.GetFeatureDataByFeatureID(objList);
        //        DataSet Dset = VerityHelper.GetViewDetailByFeature(objList);

        //        string[] arrAliasColumns = null;
        //        string[] arrColumns = null;
        //        string[] arrColumnType = null;
        //        string strortOrder = string.IsNullOrEmpty(SortOrder) ? "asc" : SortOrder;
        //        SbData.Append("<table width='100%' class='table table-bordered dataTable table-striped small-12 flip-content' id='tblFeatureID_" + FeatureID + "'>");
        //        if (Dset != null && Dset.Tables.Count > 0)
        //        {
        //            TempData["FeatureId"] = FeatureID;
        //            TempData["ContactType"] = ContactType;
        //            SbData.Append("<thead class='flip-content'>");
        //            SbData.Append("<tr>");
        //            if (Dset != null && Dset.Tables.Count > 0 && Dset.Tables[0].Rows.Count > 0)
        //            {
        //                arrAliasColumns = Dset.Tables[0].Rows[0][0].ToString().Split('~');
        //                arrColumns = Dset.Tables[0].Rows[0][1].ToString().Split('~');
        //                arrColumnType = Dset.Tables[0].Rows[0][2].ToString().Split('~');
        //            }
        //            if (arrAliasColumns != null && arrAliasColumns.Length > 0)
        //            {
        //                int i = 0;
        //                if (SortBy == arrColumns[0].ToString())
        //                {
        //                    string sortClass = "sorting_" + strortOrder;
        //                    SbData.Append("<th class='" + sortClass + "' aria-controls='tblFeatureID_" + FeatureID + "' aria-column='" + arrColumns[0].ToString().Replace(System.Environment.NewLine, "") + "'>").Append("S.No").Append("</th>");
        //                }
        //                else
        //                {
        //                    SbData.Append("<th class='sorting' aria-controls='tblFeatureID_" + FeatureID + "'  aria-column='" + arrColumns[0].ToString().Replace(System.Environment.NewLine, "") + "'>").Append("S.No").Append("</th>");
        //                }
        //                for (i = 1; i < arrAliasColumns.Length; i++)
        //                    if (SortBy == arrColumns[i].ToString())
        //                    {

        //                        string sortClass = "sorting_" + strortOrder;
        //                        SbData.Append("<th class='" + sortClass + "' aria-controls='tblFeatureID_" + FeatureID + "'  aria-column='" + arrColumns[i].ToString().Replace(System.Environment.NewLine, "") + "'>").Append(GetLabelkeys(arrAliasColumns[i].ToString())).Append("</th>");
        //                    }
        //                    else
        //                    {
        //                        SbData.Append("<th class='sorting' aria-controls='tblFeatureID_" + FeatureID + "' aria-column='" + arrColumns[i].ToString().Replace(System.Environment.NewLine, "") + "'>").Append(GetLabelkeys(arrAliasColumns[i].ToString())).Append("</th>");
        //                    }
        //                //SbData.Append("<th>").Append(GetLabelkeys(arrAliasColumns[i].ToString())).Append("</th>");

        //                SbData.Append("<th class='sorting_disabled'>").Append("Edit/Delete").Append("</th></tr></thead>");
        //                i = 0;
        //                if (Dset != null && Dset.Tables.Count > 1 && Dset.Tables[1].Rows.Count == 0)
        //                {

        //                    FillEmptyRows(SbData, ref num, Maximumrows, arrAliasColumns, ref i);
        //                    arr[0] = SbData.ToString();
        //                    arr[1] = "0";
        //                    return arr;
        //                }

        //                if (Dset.Tables.Count > 1)
        //                {
        //                    for (; i < Dset.Tables[1].Rows.Count; i++, num++)
        //                    {

        //                        SbData.Append("<tr id='" + (i + 1) + "' onclick=RowSelection(this,'" + FeatureID + "','" + Dset.Tables[1].Rows[i]["Seqno"].ToString() + "') Seqno='" + Dset.Tables[1].Rows[i]["Seqno"].ToString() + "'>");
        //                        SbData.Append("<td>").Append(num).Append("</td>");
        //                        for (int j = 1; j < arrAliasColumns.Length; j++)
        //                        {
        //                            if (arrColumnType[j].ToUpper() == "DATE")
        //                            {
        //                                SbData.Append("<td>").Append(Dset.Tables[1].Rows[i][j].ToString().ToSafeDateStringExt("dd MMM yyyy")).Append("</td>");
        //                            }
        //                            else if (FeatureID == 111 && j == 1)
        //                            {
        //                                SbData.Append("<td>").Append("<a href='javascript:' onclick='UpdateTopTaskNotificaions(" + Dset.Tables[1].Rows[i]["Seqno"] + ")'>" + Dset.Tables[1].Rows[i][j].ToString() + "</a>").Append("</td>");//UpdateTopTaskNotificaions(2)
        //                            }
        //                            else
        //                            {
        //                                SbData.Append("<td>").Append(Dset.Tables[1].Rows[i][j].ToString()).Append("</td>");
        //                            }

        //                        }

        //                        SbData.Append("<td><div class='actions'>");

        //                        if (FeatureID == 500)
        //                            SbData.Append("<a href='javascript::' data-id='" + Dset.Tables[1].Rows[i]["Seqno"].ToString() + "' onclick=OpenLegder('" + Dset.Tables[1].Rows[i]["Seqno"].ToString() + "','L') title='Ledger'><i class='icon-wallet'></i></a> ");
        //                        if (isEdit == 1)
        //                            SbData.Append("<a href='javascript::' onclick=CallEditfunction('" + Dset.Tables[1].Rows[i]["Seqno"].ToString() + "') class='btn btn-circle btn-icon-only blue'  title='Edit'><i class='fa fa-edit'></i></a>");
        //                        if (isDelete == 1)
        //                            SbData.Append("<a href='javascript::' data-id='" + Dset.Tables[1].Rows[i]["Seqno"].ToString() + "'  class='btn btn-circle btn-icon-only blue' onclick='CallDeleteRowByFeature(this)' title='Delete'><i class='fa fa-trash'></i></a> ");

        //                        SbData.Append("</div></td>");
        //                        SbData.Append("</tr>");
        //                    }
        //                }
        //                FillEmptyRows(SbData, ref num, Maximumrows, arrAliasColumns, ref i);
        //            }
        //        }
        //        SbData.Append("</table>");
        //        arr[0] = SbData.ToString().Replace(System.Environment.NewLine, "");
        //        if (Dset != null && Dset.Tables.Count > 0 && Dset.Tables.Count > 1 && Dset.Tables[2].Columns.Contains("TotalRecords"))
        //            arr[1] = Dset.Tables[2].Rows[0][0].ToString();
        //        else
        //            arr[1] = "0";
        //        return arr;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="SbData"></param>
        ///// <param name="num"></param>
        ///// <param name="arrAliasColumns"></param>
        ///// <param name="i"></param>
        //public void FillEmptyRows(StringBuilder SbData, ref int num, int noofrows, string[] arrAliasColumns, ref int i)
        //{
        //    if (i < noofrows)
        //    {
        //        for (; i < noofrows; i++, num++)
        //        {
        //            SbData.Append("<tr Seqno='0'>");
        //            SbData.Append("<td>").Append(num).Append("</td>");
        //            for (int j = 1; j < arrAliasColumns.Length; j++)
        //                SbData.Append("<td>").Append("&nbsp;").Append("</td>");
        //            SbData.Append("<td>&nbsp;</td>");
        //            SbData.Append("</tr>");
        //        }
        //    }
        //}


    }
}
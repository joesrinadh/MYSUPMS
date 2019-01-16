using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPMS.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            DestroySessionAndCookies();
            string UserId = System.Web.HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(UserId) || UserId == "0")
            {
                TUSERS objUser = new TUSERS();
                return View(objUser);
            }
            else
            {
                TUSERS objUser = new TUSERS();
                return View(objUser);
            }
        }
        /// <summary>
        /// 


        //
        private void GetPageDetails()
        {
            System.Collections.ArrayList objlist = HomeController.HeaderLogo(1);
            if (objlist != null && objlist.Count > 0)
            {
                ViewBag.HeaderLogopath = objlist[0].ToString();
                ViewBag.HeaderLogoWidth = objlist[1].ToString();
                ViewBag.HeaderLogoHeight = objlist[2].ToString();
            }
        }
        [HttpPost]
        public ActionResult Index(TUSERS model, FormCollection formcoll)
        {
            bool rememberMe = Convert.ToBoolean(formcoll["rememberMe"] == null ? "false" : string.IsNullOrEmpty(formcoll["rememberMe"].ToString()) ? "false" : formcoll["rememberMe"].ToString());
            AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginController.Login" + " - " + "Authenticating  Login Details  with Parameter  LoginViewModel as :model", LogMessageType.Informational);
            if (!ModelState.IsValid)
                return View();
            CommonUtilities _userService = new CommonUtilities();
            TUSERS obj1 = new TUSERS();
            var obj = _userService.AuthenticateUser(model.USERNAME, model.PASSWORD);

            if (obj != null)
            {
                var authTicket = new FormsAuthenticationTicket(1, obj.USERID.ToString(),  //user id
                        DateTime.Now, DateTime.Now.AddMinutes(20),// expiry
                        rememberMe,
                        model.USERID.ToString(),
                        "/"
                        );
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginViewModel as model" + " - " + JsonConvert.SerializeObject(model), LogMessageType.Informational);
                SessionManager userDetails = new SessionManager();
                //userDetails.TENANTID = Convert.ToInt32(obj.TENANTID);
                userDetails.ComanyID = obj.COMPANYID;
                userDetails.USERNAME = obj.USERNAME; userDetails.EmailAddress = obj.EMAILADDRESS;
                userDetails.USERTYPE = obj.USERTYPE; userDetails.MobileNo = obj.MOBILE;
                userDetails.USERID = obj.USERID; //userDetails.CUSTOMERID = obj.CUSTOMERID;
                userDetails.ROLEID = obj.ROLEID;
                userDetails.PROFILEPICPATH = obj.PROFILEPICPATH;
                userDetails.FIRSTNAME = obj.FIRSTNAME;
                userDetails.LASTNAME = obj.LASTNAME;
                //userDetails.LanguageId = obj.LANGUAGE.Value;
                userDetails.LanguageId = 1;
                userDetails._UserLanguage = "en-US";
                //userDetails.vFormatter = _userService.GetUserFormatter();
                System.Collections.ArrayList objlist = HomeController.HeaderLogo(Convert.ToInt32(1));//obj.TENANTID
                if (objlist != null && objlist.Count > 0)
                {
                    userDetails.HEADERLOGOFILEPATH = objlist[0].ToString();
                    userDetails.HEADERLOGOWIDTH = objlist[1].ToString();
                    userDetails.HEADERLOGOHEIGHT = objlist[2].ToString();
                }
                //LocalizationBL objlocalization = new LocalizationBL();
                //IList<LOCALESTRINGRESOURCE> objResourceKeys = objlocalization.GetAllResources(obj.LANGUAGE.Value);

                //IList<LOCALESTRINGRESOURCE> objMobileNo = objResourceKeys.ToList().FindAll(s => s.RESOURCENAME.StartsWith("LABEL.MOBILE"));
                //Dictionary<string, string> objResourcedic = new Dictionary<string, string>();
                //var objKeys = (from s in objResourceKeys select new { Key = s.RESOURCENAME.Trim().ToUpper(), Value = s.RESOURCEVALUE }).ToList();
                //foreach (var item in objKeys)
                //{
                //    if (item.Key != null)
                //    {
                //        try
                //        {
                //            if (objResourcedic.Count(s => s.Key == item.Key) == 0)
                //            {
                //                objResourcedic.Add(item.Key.Trim(), item.Value.Trim());
                //            }
                //            else
                //            {
                //                //objResourceKeys.FirstOrDefault(s =>s.Key == item.Key)
                //                //{
                //                //}
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }

                //}
                //CacheManager.Instance.AddResourceList(objResourcedic);
                DataSet dset = VerityHelper.GetRolesList(obj.COMPANYID, obj.ROLEID);

                if (dset != null && dset.Tables.Count > 0 && dset.Tables[1].Rows.Count > 0)
                {
                    if (dset != null && dset.Tables.Count > 1 && dset.Tables[2].Rows.Count > 0)
                    {
                        userDetails._RoleXML = dset.Tables[2].Rows[0]["FeaturesXML"].ToString();

                    }
                }
                string ReleaseMode = System.Configuration.ConfigurationManager.AppSettings["VirtualPath"];
                if (ReleaseMode == "false")
                    userDetails.AppPath = "";//System.Web.HttpContext.Current.Request.ApplicationPath;
                else
                    userDetails.AppPath = System.Web.HttpContext.Current.Request.ApplicationPath;

                int orgId = Convert.ToInt32(obj.COMPANYID);
                Session["TENANTID"] = userDetails.ComanyID;
                Session["USERNAME"] = userDetails.USERNAME;
                Session["USERTYPE"] = userDetails.USERTYPE;
                Session["USERID"] = userDetails.USERID;
                Session["UserDetails"] = userDetails;
                System.Web.HttpContext.Current.Session["DMSSession"] = userDetails;

                //encrypt the ticket and add it to a cookie
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);

                if (obj.USERTYPE == "A")
                {
                    return Redirect("~/Home/NDashboard");
                }
                if (obj.USERTYPE == "U")
                {
                    return Redirect("~/Home/NDashboard");
                }
                else
                {
                    GetPageDetails();
                    ModelState.AddModelError(string.Empty, "");
                    return View();
                }
            }
            else
            {
                GetPageDetails();
                obj1 = (TUSERS)model;
                ModelState.AddModelError("Error", "Login data is incorrect!");
                Session.RemoveAll();
                // AuthenticateUser(false);
                return View(obj1);
            }
        }

        public ActionResult SignOut()
        {
            //Clearing cookies and sessions
            DestroySessionAndCookies();
            return RedirectToAction("Index");
        }
        //Clearing cookies and sessions
        private void DestroySessionAndCookies()
        {
            //Session["DMSSession"] = null;
            FormsAuthentication.SignOut();
            Session.Clear();  // This may not be needed -- but can't hurt
            Session.Abandon();
            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
        }
    }
}
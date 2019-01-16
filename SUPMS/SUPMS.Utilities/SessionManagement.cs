using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SUPMS.Infrastructure.Utilities
{
    public class SessionManagement
    {

        /// <summary>
        /// 
        /// </summary>
        public SessionManager UserSession
        {

            get
            {
                if (System.Web.HttpContext.Current.Session["REMSSession"] != null)
                {
                    return (SessionManager)_httpContext.Session["REMSSession"];
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
        public readonly HttpContext _httpContext;
        /// <summary>
        /// 
        /// </summary>
        public SessionManagement()
        {
            _httpContext = System.Web.HttpContext.Current;
        }


        /// <summary>
        /// 
        /// </summary>
        public class CommonClass
        {
            public int Entity
            {
                get;
                set;
            }
            public string EntityId
            {
                get;
                set;
            }
            //public string GalleryGrid
            //{
            //    get;
            //    set;
            //}
            public string NotesGrid
            {
                get;
                set;
            }
            public string AttachmentsGrid
            {
                get;
                set;
            }
            public string CustomFields
            {
                get;
                set;
            }
            public string ListOfCustomFields
            {
                get;
                set;
            }
            public string LookUpsGrid
            {
                get;
                set;
            }
            //public string AutoCharges
            //{
            //    get;
            //    set;
            //}
        }
    }
}

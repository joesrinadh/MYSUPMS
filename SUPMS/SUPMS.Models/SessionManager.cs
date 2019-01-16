using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Models
{
    public class SessionManager
    {
        public string AppPath = string.Empty;

        private int _UserId;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private int _RoleId;

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private string _EmailAddress;
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        private string _MobileNo;
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private int _ComanyID;
        public int ComanyID
        {
            get { return _ComanyID; }
            set { _ComanyID = value; }
        }

        private string _UserType;
        public string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        private string _UserImage;
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        public int ContactType
        {
            get;
            set;
        }
        public int LanguageId
        {
            get;
            set;
        }

        public string ProfilePicPath
        {
            get;
            set;
        }
        public string HeaderLogoPath
        {
            get;
            set;
        }
        public string RoleXML;        
        public VFormatter _vFormatter = new VFormatter();
        public VFormatter vFormatter
        {
            get { return _vFormatter; }
            set { _vFormatter = value; }
        }
    }

    #region Declarations

    public class VFormatter
    {
        public VFormatter()
        {
            DateFormate = "dd-MM-yyyy";
            decimalFormate = "0.000";
            CurrenySymbol = "Rs";
            SpaceAfterCurrency = true;
            ShowInFront = false;
            jDateFormate = "dd-MM-yyyy";
        }
        public string MDateFormate
        {
            get
            {
                return "{0:" + DateFormate + "}";
            }
        }

        public string MCurrencyFormate
        {
            get
            {
                return "{0:" + decimalFormate + "}";
            }
        }
        public string jDateFormate { get; set; }
        public string DateFormate { get; set; }
        public string decimalFormate { get; set; }
        public string CurrenySymbol { get; set; }
        public bool SpaceAfterCurrency { get; set; }
        public bool ShowInFront { get; set; }
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Utilities
{
    public class SessionManager
    {
        public string AppPath = string.Empty;
        private string _TenantName;

        public string TENANTNAME
        {
            get { return _TenantName; }
            set { _TenantName = value; }
        }

        private int _UserId;

        public int USERID
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int BUID = -1;
        private int _RoleId;

        public int ROLEID
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
        public string FIRSTNAME
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _LastName;
        public string LASTNAME
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _UserName;
        public string USERNAME
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private int _TenantId;
        public int ComanyID
        {
            get { return _TenantId; }
            set { _TenantId = value; }
        }

        private string _UserType;
        public string USERTYPE
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        private string _UserImage;
        public string USERIMAGE
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        public int? CUSTOMERID
        {
            get;
            set;
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

        public string PROFILEPICPATH
        {
            get;
            set;
        }
        public string HEADERLOGOFILEPATH
        {
            get;
            set;
        }
        public string HEADERLOGOWIDTH
        {
            get;
            set;
        }
        public string HEADERLOGOHEIGHT
        {
            get;
            set;
        }
        public List<ExKeyValuePair> TENANTSBUILDINGLIST
        {
            get;
            set;
        }
        public string _RoleXML;
        public string _UserLanguage;

        public ThemeSettings themeSetting { get; set; }
        public VFormatter _vFormatter = new VFormatter();
        public VFormatter vFormatter {
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
        public string DateFormate{ get; set; }
        public string decimalFormate{get;set;}
        public string CurrenySymbol{get;set;}
        public bool SpaceAfterCurrency { get; set; }
        public bool ShowInFront { get; set; }
    }
    public class ExKeyValuePair
    {
        public string BuildingId
        {
            get;
            set;
        }
        public string UnitId
        {
            get;
            set;
        }
        public string AssociationId
        {
            get;
            set;
        }
        public string BuildingName
        {
            get;
            set;
        }
        public string UnitName
        {
            get;
            set;
        }
        public string AssociationName
        {
            get;
            set;
        }
        public string Porfolio
        {
            get;
            set;
        }
        public ExKeyValuePair(string BId, string BName, string UId, string UName, string AId, string AName, string P)
        {
            this.AssociationId = AId;
            this.AssociationName = AName;
            this.BuildingId = BId;
            this.BuildingName = BName;
            this.UnitId = UId;
            this.UnitName = UId;
            this.Porfolio = P;
        }
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
        public string GalleryGrid
        {
            get;
            set;
        }
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
        public string AutoCharges
        {
            get;
            set;
        }
    }
    public class Field
    {
        public int Seqno
        {
            get;
            set;
        }
        public int Entity
        {
            get;
            set;
        }
        public string FieldName
        {
            get;
            set;
        }
        public string SYSTEMCOLUMN
        {
            get;
            set;
        }
        public string DataType
        {
            get;
            set;
        }
        public string DefaultValue
        {
            get;
            set;
        }
        public string ProbableValues
        {
            get;
            set;
        }
        public string Mandatory
        {
            get;
            set;
        }
        public string ReadOnly
        {
            get;
            set;
        }
        public string PlaceUnder
        {
            get;
            set;
        }
        public string Visible
        {
            get;
            set;
        }
    }
    #endregion
}

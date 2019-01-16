using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using VERITY.DMS.MODEL;

namespace SUPMS.Infrastructure.Utilities
{
    public class CommonUtilities
    {
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ProperMS");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();

            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown 
        /// when the crypted string is null or empty.</exception>
        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// gets UserName,Password from Users
        /// </summary>
        /// <param name="USERNAME"></param>
        /// <param name="PASSWORD"></param>
        /// <returns></returns>
        public TUSERS AuthenticateUser(string USERNAME, string PASSWORD)
        {
            //UserRepository objRepository = new UserRepository();
            // var objUser = objRepository.FindBy(x => x.USERNAME == USERNAME && x.ISSTATUS == 1).SingleOrDefault();
            ArrayList objlist = new ArrayList();
            objlist.Add(USERNAME);
            objlist.Add(1);
            DataSet dset = VerityHelper.GetUserDetails(objlist);

            if (dset != null && dset.Tables.Count > 0 ? dset.Tables[0].Rows.Count > 0 ? true : false : false)
            {
                if (CommonUtilities.Decrypt(dset.Tables[0].Rows[0]["PASSWORD"].ToString()) == PASSWORD)
                {
                    //preparing TUSER Object and return dataset
                    return PrepareTUSERObject(dset);
                }
                else
                    return null;
            }
            else
                return null;

        }

        /// <summary>
        /// preparing TUSERS Object
        /// </summary>
        /// <param name="dset"></param>
        /// <returns>TUSERS object</returns>
        public static TUSERS PrepareTUSERObject(DataSet dset)
        {
            TUSERS objUser = new TUSERS();
            if (dset != null && dset.Tables.Count > 0)
            {
                if (dset.Tables[0].Rows.Count > 0)
                {
                    objUser.PASSWORD = dset.Tables[0].Rows[0]["PASSWORD"].ToString();
                    objUser.USERNAME = dset.Tables[0].Rows[0]["USERNAME"].ToString();
                    objUser.FIRSTNAME = dset.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                    objUser.LASTNAME = dset.Tables[0].Rows[0]["LASTNAME"].ToString();
                    objUser.USERID = Convert.ToInt32(dset.Tables[0].Rows[0]["USERID"]);
                    objUser.EMAILADDRESS = dset.Tables[0].Rows[0]["EMAILADDRESS"].ToString();
                    objUser.STATUS = Convert.ToInt32(dset.Tables[0].Rows[0]["STATUS"]);
                    objUser.ROLEID = Convert.ToInt32(dset.Tables[0].Rows[0]["ROLEID"]);
                    objUser.USERTYPE = dset.Tables[0].Rows[0]["USERTYPE"].ToString();
                    objUser.COMPANYID = Convert.ToInt32(dset.Tables[0].Rows[0]["COMPANYID"]);
                    objUser.CREATEDBY = Convert.ToInt32(dset.Tables[0].Rows[0]["CREATEDBY"]);
                    objUser.CREATEDDATE = Convert.ToDateTime(dset.Tables[0].Rows[0]["CREATEDDATE"]);
                    objUser.LANGUAGE = Convert.ToInt32(Convert.IsDBNull(dset.Tables[0].Rows[0]["language"]) ? "0" : dset.Tables[0].Rows[0]["language"]);
                    objUser.MOBILE = dset.Tables[0].Rows[0]["MOBILE"].ToString();
                    objUser.PROFILEPICPATH = dset.Tables[0].Rows[0]["PROFILEPICPATH"] == DBNull.Value ? "" : dset.Tables[0].Rows[0]["PROFILEPICPATH"].ToString();
                }
                else
                    return null;
            }
            else
                return null;
            return objUser;
        }
    }
   
}

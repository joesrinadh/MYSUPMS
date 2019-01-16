using SUPMS.Infrastructure.AsyncLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Utilities
{
   public class Notification
    {
        static List<StaticKeywords> lstKeyWords = new List<StaticKeywords>();
        public class StaticKeywords
        {
            public string _Key = string.Empty;
            public string _Value = string.Empty;
            public StaticKeywords(string _k, string _v)
            {
                this._Key = _k;
                this._Value = _v;
            }
        }
        #region SMS Configuration
        public class SMSAPI
        {
            private string strSMSUserID = string.Empty;
            private string strSMSPassword = string.Empty;
            private string strSenderGSMID = string.Empty;
            private string strSMSAPI = string.Empty;

            private WebProxy objProxy1 = null;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="notifSeqno"></param>
            /// <param name="bFlg"></param>
            /// <param name="Type"></param>
            /// <param name="strTo"></param>
            /// <param name="strCC"></param>
            /// <param name="strBody"></param>
            /// <param name="strSubject"></param>
            /// <param name="UserID"></param>
            /// <param name="FeatureID"></param>
            /// <param name="TemplateType"></param>
            /// <param name="Seqno"></param>
            /// <param name="Msg"></param>
            private static void TrackMail(long notifSeqno, bool bFlg, int Type, string strTo, string strCC, string strBody, string strSubject, int UserID, int FeatureID, int TemplateType, int Seqno, string Msg)
            {
                try
                {
                    AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - DMS_Notification.InsertNotifStatus" + " ,  Before Save  ", LogMessageType.Informational);

                    DMS_Notification.NotifClass objClass = new DMS_Notification.NotifClass();
                    objClass._FeatureId = FeatureID;
                    objClass._featureSeqno = Seqno;
                    objClass._TemplateType = TemplateType;
                    objClass._Type = Type;
                    objClass._CreatedBy = UserID;
                    objClass._ToAddress = strTo;
                    objClass._BccAddress = strCC;
                    objClass._Subject = strSubject;
                    objClass._Body = strBody;
                    objClass.Exception = Msg;
                    objClass.NotificationSent = bFlg;
                    objClass._NotifSeqno = notifSeqno;
                    DMS_Notification.InsertNotifStatus(objClass);
                }
                catch (Exception ex)
                {
                    AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - DMS_Notification.TrackMail" + " , SPName : InsertNotifStatus  " + ex.Message.ToString(), LogMessageType.Error);

                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Message"></param>
            /// <param name="Mobile_Number"></param>
            /// <param name="iLanguageId"></param>
            /// <returns></returns>
            public string SMSInitiate(string Message, string[] Mobile_Number, long NotifSeqno, int UserID, int FeatureID, int TemplateType, int Seqno, int iLanguageId = 0)
            {
                ArrayList objList = new ArrayList();
                objList.Add(167);//default for oman
                DataSet dset = VerityHelper.GetSmsDetailsByCountry(objList);
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
                {
                    string strTo = string.Empty;
                    for (int i = 0; i < Mobile_Number.Length; i++)
                    {
                        strTo = strTo + Mobile_Number[i] + ",";
                    }
                    try
                    {
                        TrackMail(NotifSeqno, false, 2, strTo.Trim(','), "", Message, "", UserID, FeatureID, TemplateType, Seqno, "");
                        this.strSMSUserID = DMS_Notification.ReturnString("SMSUSERNAME", dset, 0);
                        this.strSMSPassword = DMS_Notification.ReturnString("SMSPASSWORD", dset, 0);
                        this.strSMSAPI = DMS_Notification.ReturnString("SMSAPI", dset, 0);
                        this.strSenderGSMID = DMS_Notification.ReturnString("SMSSENDERID", dset, 0);
                        SMSResponse objResponse = SendSMS(strSMSUserID, strSMSPassword, Message, Mobile_Number, iLanguageId);
                        TrackMail(NotifSeqno, true, 3, strTo.Trim(','), "", Message, "", UserID, FeatureID, TemplateType, Seqno, objResponse.Description);
                    }
                    catch (Exception ex)
                    {
                        TrackMail(NotifSeqno, false, 3, strTo.Trim(','), "", Message, "", UserID, FeatureID, TemplateType, Seqno, ex.StackTrace.ToString());

                    }
                }
                return "";
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Message"></param>
            /// <param name="Mobile_Number"></param>
            /// <param name="NotifSeqno"></param>
            /// <param name="UserID"></param>
            /// <param name="FeatureID"></param>
            /// <param name="TemplateType"></param>
            /// <param name="Seqno"></param>
            /// <param name="iLanguageId"></param>
            /// <returns></returns>
            public string SMSInitiateWithoutFormat(string Message, string[] Mobile_Number, int UserID, int FeatureID, int TemplateType, int Seqno, int iLanguageId = 0)
            {
                ArrayList objList = new ArrayList();
                objList.Add(167);//default for oman
                DataSet dset = VerityHelper.GetSmsDetailsByCountry(objList);
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
                {
                    string strTo = string.Empty;
                    for (int i = 0; i < Mobile_Number.Length; i++)
                    {
                        strTo = strTo + Mobile_Number[i] + ",";
                    }
                    try
                    {

                        this.strSMSUserID = DMS_Notification.ReturnString("SMSUSERNAME", dset, 0);
                        this.strSMSPassword = DMS_Notification.ReturnString("SMSPASSWORD", dset, 0);
                        this.strSMSAPI = DMS_Notification.ReturnString("SMSAPI", dset, 0);
                        this.strSenderGSMID = DMS_Notification.ReturnString("SMSSENDERID", dset, 0);
                        SMSResponse objResponse = SendSMS(strSMSUserID, strSMSPassword, Message, Mobile_Number, iLanguageId);
                        return "Sucess";
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }
                }
                return "";
            }

            /// <summary>
            /// Method which can be used by the client applications to deliver messages using iBulk SMS service
            /// </summary>
            /// <param name="UserID">User ID to access the service, provided by Infocomm to the client</param>
            /// <param name="password">Password to access the service, provided by Infocomm to the client.</param>
            /// <param name="Message">Text message to be sent. Message length description is given in next section.</param>
            /// <param name="Mobile_Number">Array of mobile numbers. At least one mobile must be provided. All numbers must start with 968. Please do not start number with + or 00.</param>
            /// <param name="iLanguageId">Allowed values are 0 for English and 64 for Arabic. Default 0</param>
            /// <returns>Response </returns>
            public SMSResponse SendSMS(string UserID, string password, string Message, string[] Mobile_Number, int iLanguageId = 0)
            {
                //BulkSMSService.BulkSMSSoapClient objClient = new BulkSMSService.BulkSMSSoapClient();
                //BulkSMSService.ArrayOfString objReceipts = new BulkSMSService.ArrayOfString();
                //objReceipts.AddRange(Mobile_Number);
                //int iresult = objClient.PushMessage(UserID, password, Message, iLanguageId, DateTime.Now, objReceipts, 1);
                SMSResponse objResponse = new SMSResponse();
                //objResponse.Code = iresult;
                //GetResponseMessage(iresult, ref objResponse);
                return objResponse;
            }

            private void GetResponseMessage(int iresult, ref SMSResponse objResponse)
            {
                switch (iresult)
                {
                    case 1:
                        objResponse.Description = "Message Pushed";
                        break;
                    case 2:
                        objResponse.Description = "Company Not Exits. Please check the company";
                        break;
                    case 3:
                        objResponse.Description = "User or Password is wrong";
                        break;
                    case 4:
                        objResponse.Description = "Credit is Low";
                        break;
                    case 5:
                        objResponse.Description = "Message is blank";
                        break;
                    case 6:
                        objResponse.Description = "Message Length Exceeded";
                        break;
                    case 7:
                        objResponse.Description = "Account is Inactive";
                        break;
                    case 8:
                        objResponse.Description = "No Recipient found, array length is zero";
                        break;
                    case 9:
                        objResponse.Description = "One or more mobile numbers are of invalid length";
                        break;
                    case 10:
                        objResponse.Description = "Invalid Language";
                        break;
                    case 11:
                        objResponse.Description = "Un Known Error";
                        break;
                    case 12:
                        objResponse.Description = "Account is Blocked by administrator, concurrent failure of login.";
                        break;
                    case 13:
                        objResponse.Description = "Account Expired";
                        break;
                    case 14:
                        objResponse.Description = "Credit Expired";
                        break;
                    case 18:
                        objResponse.Description = "Web Service User Id not configured with Infocomm.";
                        break;
                    case 20:
                        objResponse.Description = "Client IP Has been Blocked, Please contact to Administrator.";
                        break;

                }
            }
        }

        public class SMSResponse
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }
        #endregion
        #region Email Configuration
        /// <summary>
        /// 
        /// </summary> 
        public class Mail
        {


            private string strText = string.Empty;
            private string strEmaiPassword = string.Empty;
            private string strEmailSmtp = string.Empty;
            private string strEmailUserNm = string.Empty;
            private string strEmailPort = string.Empty;
            private string strEmailNewLine = string.Empty;
            private string strSMSNewLine = string.Empty;
            private bool strEnableSSL;
            private string strMailTo = string.Empty;
            private string strMailcc = string.Empty;
            private string strSubject = string.Empty;
            private string EmailEnable = string.Empty;
            private string strPath = string.Empty;
            private string Sender = string.Empty;

            public static string DefaultLogo()
            {
                return ReturnApplicationPath("newlogo.jpg");
                // return AppDomain.CurrentDomain.BaseDirectory + "Content/images/newlogo.jpg";
            }
            public static string ReturnApplicationPath(string Image)
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("DefaultPageUrl"))
                {
                    return System.Configuration.ConfigurationManager.AppSettings["DefaultPageUrl"].ToString() + "/Images/" + Image;
                }
                return "";
            }

            #region Emails for Approval/Reject Files
            //TO RETRIEVE DATA FROM DATATABLE
            public static string ReturnString(int tableNo, string col, DataSet dset, int row)
            {
                if (dset.Tables[tableNo].Columns.Contains(col))
                    return dset.Tables[tableNo].Rows[row][col].ToString();
                return "";
            }
          
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Body"></param>
            /// <param name="Subject"></param>
            /// <param name="oMailMessage"></param>
            /// <param name="uid"></param>
            /// <param name="TemplateType"></param>
            /// <param name="Seqno"></param>
            /// <returns></returns>
            public static string DefaultHTMLFormatForBasicEmails(string Body, string Subject, MailMessage oMailMessage)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>  <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml'>  <head>      <meta http-equiv='X-UA-Compatible' content='IE=edge' />      <meta name='viewport' content='width=device-width, initial-scale=1' />      <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />      <meta name='apple-mobile-web-app-capable' content='yes' />      <meta name='apple-mobile-web-app-status-bar-style' content='black' />      <meta name='format-detection' content='telephone=no' />      <title>dms template</title>      <style type='text/css'>          /* Resets */          .ReadMsgBody {              width: 100%;              background-color: #ebebeb;          }            .ExternalClass {              width: 100%;              background-color: #ebebeb;          }                .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {                  line-height: 100%;              }            body {              -webkit-text-size-adjust: none;              -ms-text-size-adjust: none;          }            body {              margin: 0;              padding: 0;          }            .yshortcuts a {              border-bottom: none !important;          }            .rnb-del-min-width {              min-width: 0 !important;          }            /* Image width by default for 3 columns */          img[class='rnb-col-3-img'] {              max-width: 170px;          }            /* Image width by default for 2 columns */          img[class='rnb-col-2-img'] {              max-width: 264px;          }            /* Image width by default for 2 columns aside small size */          img[class='rnb-col-2-img-side-xs'] {              max-width: 180px;          }            /* Image width by default for 2 columns aside big size */          img[class='rnb-col-2-img-side-xl'] {              max-width: 350px;          }            /* Image width by default for 1 column */          img[class='rnb-col-1-img'] {              max-width: 550px;          }            /* Image width by default for header */          img[class='rnb-header-img'] {              max-width: 590px;          }            @media screen and (max-width: 480px) {              td[class='rnb-container-padding'] {                  padding-left: 10px !important;                  padding-right: 10px !important;              }                /* force container nav to (horizontal) blocks */              td[class='rnb-force-nav'] {                  display: block;              }          }            @media only screen and (max-width : 600px) {                /* center the address &amp; social icons */              .rnb-text-center {                  text-align: center !important;              }                /* force container columns to (horizontal) blocks */              td[class='rnb-force-col'] {                  display: block;                  padding-right: 0 !important;                  padding-left: 0 !important;              }                table[class='rnb-container'] {                  width: 100% !important;              }                table[class='rnb-btn-col-content'] {                  width: 100% !important;              }                table[class='rnb-col-3'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-bottom: 10px;                  padding-bottom: 10px;                  border-bottom: 1px solid #eee;              }                table[class='rnb-last-col-3'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;              }                table[class='rnb-col-2'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-bottom: 10px;                  padding-bottom: 10px;                  border-bottom: 1px solid #eee;              }                table[class='rnb-col-2-noborder-onright'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-bottom: 10px;                  padding-bottom: 10px;              }                table[class='rnb-col-2-noborder-onleft'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-top: 10px;                  padding-top: 10px;              }                table[class='rnb-last-col-2'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;              }                table[class='rnb-col-1'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;              }                img[class='rnb-col-3-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-2-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-2-img-side-xs'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-2-img-side-xl'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-1-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-header-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                td[class='rnb-mbl-float-none'] {                  float: inherit !important;              }          }      </style>      <!--[if gte mso 11]><style type='text/css'>table{border-spacing: 0;	}table td {border-collapse: separate;}</style><![endif]-->      <!--[if !mso]><!-->      <style type='text/css'>          table {              border-spacing: 0;          }                table td {                  border-collapse: collapse;              }      </style>      <!--<![endif]-->  </head>  <body>        <table border='0' width='100%' cellpadding='0' cellspacing='0' class='main-template' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>              <tbody>              <tr style='display: none !important; font-size: 1px;'>                  <td></td>                  <td></td>              </tr>                <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>  ");


                sb.Append(" &nbsp; </td>              </tr>              <tr>     <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                  <table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;' name='Layout_9' id='Layout_9'>                          <tbody>                              <tr>                                  <td class='rnb-del-min-width' valign='top' align='center' style='min-width: 590px;'>                                        <table width='590' class='rnb-container' cellpadding='0' border='0' bgcolor='#D1D1D1' align='center' cellspacing='0' style='background-color: #D1D1D1;'>                                          <tbody>                                              <tr>                                                  <td valign='top' align='center'>                                                              <img border='0' hspace='0' vspace='0' width='590' class='rnb-header-img' alt='' style='display: block; border-radius: 0px; width: 590px; max-width: 600px !important;' src='" + ReturnApplicationPath("Email-head.png") + "'>                                                  </td>                                              </tr>                                          </tbody>                                      </table>  ");
                sb.Append("</td>                              </tr>                          </tbody>                      </table>                                                                                        </td>              </tr>              <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                                                                          <table width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='background-color: #D1D1D1;' name='Layout_10' id='Layout_10'>                            <tbody>                              <tr>                                    <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                          <table border='0' width='590' cellpadding='0' cellspacing='0' class='rnb-container' bgcolor='#ffffff' style='height: 0px; border-radius: 0px; border-collapse: separate; padding-left: 20px; padding-right: 20px; background-color: rgb(255, 255, 255);'>                                          <tbody>                                              <tr>                                                    <td class='rnb-container-padding' bgcolor='#ffffff' style='background-color: #ffffff; font-size: px; font-family: ; color: ;'>                                                        <table border='0' cellpadding='0' cellspacing='0' class='rnb-columns-container' align='center' style='margin: auto;'>                                                          <tbody>                                                              <tr>                                                                      <td class='rnb-force-col' align='center'>                                                                          <table border='0' cellspacing='0' cellpadding='0' align='center' class='rnb-col-1'>                                                                            <tbody>                                                                              <tr>                                                                                  <td height='10'></td>                                                                              </tr>                                                                                  <tr>                                                                                    <td style='font-size: 18px; font-family: Arial,Helvetica,sans-serif; color: #999; font-weight: normal; text-align: center;'>                                                                                          <span style='color: #999; font-weight: normal;'><span style='color: #FF0000'>" + Subject + "</span></span>                                                                                  </td>                                                                              </tr>                                                                              <tr>                                                                                  <td height='10'></td>                                                                              </tr>                                                                              </tbody>                                                                      </table>                                                                      </td>                                                                      </tr>                                                          </tbody>                                                      </table>                                                    </td>                                              </tr>                                            </tbody>                                      </table>                                    </td>                              </tr>                            </tbody>                      </table>                                </td>              </tr>              <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                                                                                    <table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;' name='Layout_11'>                          <tbody>                              <tr>                                    <td class='rnb-del-min-width' align='center' valign='top' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;'>                                        <table width='590' border='0' cellpadding='0' cellspacing='0' class='rnb-container' bgcolor='#ffffff' style='border-radius: 0px; border-collapse: separate; padding-left: 20px; padding-right: 20px; background-color: rgb(255, 255, 255);'>                                            <tbody>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px;'>&nbsp;</td>                                              </tr>                                              <tr>                                                    <td valign='top' class='rnb-container-padding' bgcolor='#ffffff' style='background-color: #ffffff;' align='left'>                                                        <table width='100%' border='0' cellpadding='0' cellspacing='0' class='rnb-columns-container'>                                                          <tbody>                                                              <tr>                                                                        <td class='rnb-force-col' valign='top' style='padding-right: 0px;'>                                                                                <table border='0' valign='top' cellspacing='0' cellpadding='0' width='100%' align='left' class='rnb-col-1'>                                                                              <tbody>                                                                              <tr>                                                                                    <td style='font-size: 13px; font-family: Arial,Helvetica,sans-serif, sans-serif; color: #555;'>");

                sb.Append("" + Body + "");

                sb.Append("</td>  </tr>  </tbody> </table> </td>  </tr>  </tbody>   </table>   </td>    </tr><tr><td height='20' style='font-size: 1px; line-height: 1px; border-bottom: 0px;'>&nbsp;</td>                                              </tr>                                          </tbody>                                      </table>                                    </td>                              </tr>                          </tbody>                      </table>                      </td>              </tr>              <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                                                                                    <table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;' name='Layout_8'>                          <tbody>                              <tr>                                    <td class='rnb-del-min-width' align='center' valign='top' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;'>                                        <table width='590' border='0' cellpadding='0' cellspacing='0' class='rnb-container' bgcolor='#ffffff' style='border-radius: 0px; border-collapse: separate; padding-left: 20px; padding-right: 20px; background-color: rgb(255, 255, 255);'>                                            <tbody>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px;'>&nbsp;</td>                                              </tr>                                              <tr>                                                    <td valign='top' class='rnb-container-padding' bgcolor='#ffffff' style='background-color: #ffffff;' align='left'>                                                        <table width='100%' border='0' cellpadding='0' cellspacing='0' class='rnb-columns-container'>                                                          <tbody>                                                              <tr>                                                                        <td class='rnb-force-col' valign='top' style='padding-right: 0px;'>                                                                                <table border='0' valign='top' cellspacing='0' cellpadding='0' width='100%' align='left' class='rnb-col-1'>                                                                              <tbody>                                                                              <tr>                                                                                    <td style='font-size: 13px; font-family: Arial,Helvetica,sans-serif, sans-serif; color: #555;'>                                                                                      <div style='text-align: right;'><span style='color: #B22222;'></span></div>                                                                                  </td>                                                                              </tr>                                                                              </tbody>                                                                      </table>                                                                      </td>                                                              </tr>                                                          </tbody>                                                      </table>                                                    </td>                                              </tr>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px; border-bottom: 0px;'>&nbsp;</td>                                              </tr>                                          </tbody>                                      </table>                                    </td>                              </tr>                          </tbody>                      </table>                      </td>              </tr>     <tr>  	  		  		<td align='center' valign='top' bgcolor='#D1D1D1' style='background-color:#D1D1D1;'>  		   			  			<table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' style='min-width:590px; background-color:#D1D1D1;' name='Layout_968' id='Layout_968'>  				<tbody><tr>  					  					<td class='rnb-del-min-width' valign='top' align='center' bgcolor='#D1D1D1' style='min-width:590px; background-color:#D1D1D1;'>  						  						<table width='100%' cellpadding='0' border='0' height='30' cellspacing='0' bgcolor='#D1D1D1' style='background-color:#D1D1D1;'>  							<tbody><tr>  								  								<td valign='top' height='30'>  									<img width='20' height='30' style='display:block; max-height:30px; max-width:20px;' alt='' src='http://img.mailinblue.com/new_images/rnb/rnb_space.gif'>  								</td>  							</tr>  						</tbody></table>  					</td>  				</tr>  			</tbody></table>   		</td>  	</tr>         </tbody>      </table> </body>  </html>  ");


                return sb.ToString();
            }
            public static string ReturnString(string col, DataSet dset, int row)
            {
                if (dset.Tables[0].Columns.Contains(col))
                    return dset.Tables[0].Rows[row][col].ToString();
                return "";
            }
      
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Userid"></param>
            /// <returns></returns>
            public static string ApprovedUser(int Userid)
            {
                string strUserNm = string.Empty;
                ArrayList objlist = new ArrayList();
                objlist.Add(Userid);
                DataSet dset = VerityHelper.GetUserDetailsById(objlist);
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
                {
                    strUserNm = ReturnString("FIRSTNAME", dset, 0) + " " + ReturnString("LASTNAME", dset, 0);
                    strUserNm = strUserNm + "(" + ReturnString(1, "GROUPNAME", dset, 0) + ")";
                }

                return strUserNm;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dset"></param>
            /// <param name="UserId"></param>
            /// <param name="Body"></param>
            public static void CheckApprovedUser(DataSet dset, int UserId, string Body)
            {
                if (dset.Tables[0].Columns.Contains("APPROVEDUSERNAME") && Body.Contains("APPROVEDUSERNAME"))
                {
                    foreach (DataRow item in dset.Tables[0].Rows)
                    {
                        item["APPROVEDUSERNAME"] = ApprovedUser(UserId);
                        dset.Tables[0].AcceptChanges();
                    }


                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="col"></param>
            /// <param name="dset"></param>
            /// <param name="row"></param>
            /// <returns></returns>
            public static string ReturnString(DataSet dset, string col, int row)
            {
                if (dset.Tables[0].Columns.Contains(col))
                    return dset.Tables[0].Rows[row][col].ToString();
                return "";
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="res"></param>
            /// <param name="templatetype"></param>
            /// <param name="featureid"></param>
            /// <param name="formattype"></param>
            /// <param name="userid"></param>
            /// <returns></returns>
            public static int SendEmailForApprovalUsers(int res, int templatetype, int featureid, int formattype, int userid)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginController.SendEmailForApprovalUsers -- request for mail send received ", LogMessageType.Informational);
                DataSet dataSet = GetTemplateByFeature(templatetype, featureid, formattype);
                string ToAddress = string.Empty, BccAddress = string.Empty, Subject = string.Empty, Body = string.Empty;

                if (dataSet != null ? dataSet.Tables.Count > 0 ? dataSet.Tables[0].Rows.Count > 0 : false : false)
                {
                    AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginController.SendEmailForApprovalUsers -- request for mail send received and loop entered for templated mail", LogMessageType.Informational);
                    try
                    {
                        if (2 == 2) //email or fileworkflowstatus follow status
                        {
                            DataSet dsetData = GetFeatureDataBySeqno(templatetype, featureid, res);
                            SmtpClient client = new SmtpClient();
                            NetworkCredential credentials = new NetworkCredential();
                            MailMessage mailMessage = new MailMessage();
                            if (dsetData != null ? dsetData.Tables.Count > 0 ? dsetData.Tables[1].Rows.Count > 0 : false : false)
                            {
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                                client.EnableSsl = ReturnString(1, "SMTPUSESSL", dsetData, 0) == "True" || ReturnString(1, "SMTPUSESSL", dsetData, 0) == "1" ? true : false;

                                client.Host = ReturnString(1, "SMTPHOST", dsetData, 0);
                                client.Port = Convert.ToInt32(ReturnString(1, "SMTPPORT", dsetData, 0));


                                client.UseDefaultCredentials = false;
                                credentials.UserName = ReturnString(1, "SMTPAUTHENTICATEUSER", dsetData, 0);
                                credentials.Password = ReturnString(1, "SMTPAUTHENTICATEPASSWORD", dsetData, 0);
                                client.Credentials = credentials;
                                mailMessage.From = new MailAddress(ReturnString(1, "SMTPAUTHENTICATEUSER", dsetData, 0), ReturnString(1, "SENDER", dsetData, 0));
                            }
                            else
                            {
                                return -300;//when user email settings has not done
                            }

                            string ToEmailAddress = string.Empty;


                            for (int i = 0; i < dsetData.Tables[0].Rows.Count; i++)
                            {

                                ToEmailAddress += ReturnString("APPROVALUSEREMAIL", dsetData, i) + ",";
                            }
                            ToAddress = ToEmailAddress.Trim(',');
                            BccAddress = ReturnString("BccAddress", dataSet, 0);
                            Subject = ReturnString("Subject", dataSet, 0);
                            Body = ReturnString("Format", dataSet, 0);
                            ToAddress = Returnstring(dsetData, ToAddress);
                            BccAddress = Returnstring(dsetData, BccAddress);
                            Subject = Returnstring(dsetData, Subject);

                            CheckApprovedUser(dsetData, userid, Body);
                            //CheckPasswordField(dsetData, Body);
                            //CheckReferredLinkField(dsetData, Body, Seqno);
                            //CheckPaymentLinkField(dsetData, Body, Seqno);
                            Body = DefaultHTMLFormatForBasicEmails(Returnstring(dsetData, Body), Subject, mailMessage);



                            Mail.AddMailCredentials(mailMessage, 1, ToAddress);
                            Mail.AddMailCredentials(mailMessage, 3, BccAddress);
                            mailMessage.Subject = Subject;
                            mailMessage.IsBodyHtml = true;
                            mailMessage.Body = Body;
                            try
                            {
                                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginController.SendEmailForApprovalUsers -- mail sending to " + ToAddress + " with bcc " + BccAddress + " with body " + Body + " mailmessage " + mailMessage, LogMessageType.Informational);
                                client.Send(mailMessage);
                            }
                            catch (Exception ex)
                            {
                                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - LoginController.SendEmailForApprovalUsers " + ex.StackTrace.ToString(), LogMessageType.Error);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - LoginController.SendEmailForApprovalUsers " + ex.StackTrace.ToString(), LogMessageType.Error);
                        return -200;//other type of error
                    }
                }
                else
                {
                    AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginController.SendEmailForApprovalUsers -- request for mail send received and loop entered for non-templated mail", LogMessageType.Informational);
                    DataSet dsetData = GetFeatureDataBySeqno(templatetype, featureid, res);
                    SmtpClient client = new SmtpClient();
                    NetworkCredential credentials = new NetworkCredential();
                    MailMessage mailMessage = new MailMessage();
                    if (dsetData != null ? dsetData.Tables.Count > 0 ? dsetData.Tables[1].Rows.Count > 0 : false : false)
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.EnableSsl = ReturnString(1, "SMTPUSESSL", dsetData, 0) == "True" || ReturnString(1, "SMTPUSESSL", dsetData, 0) == "1" ? true : false;
                        client.Host = ReturnString(1, "SMTPHOST", dsetData, 0);
                        client.Port = Convert.ToInt32(ReturnString(1, "SMTPPORT", dsetData, 0));

                        client.UseDefaultCredentials = false;
                        credentials.UserName = ReturnString(1, "SMTPAUTHENTICATEUSER", dsetData, 0);
                        credentials.Password = ReturnString(1, "SMTPAUTHENTICATEPASSWORD", dsetData, 0);
                        client.Credentials = credentials;
                        mailMessage.From = new MailAddress(ReturnString(1, "SMTPAUTHENTICATEUSER", dsetData, 0), ReturnString(1, "SENDER", dsetData, 0));
                        string ToEmailAddress = string.Empty;


                            for (int i = 0; i < dsetData.Tables[0].Rows.Count; i++)
                            {

                                ToEmailAddress += ReturnString("APPROVALUSEREMAIL", dsetData, i) + ",";
                            }
                            //ToAddress = ReturnString("ToAddress", dataSet, 0);
                            //BccAddress = ReturnString("BccAddress", dataSet, 0);
                            //Subject = ReturnString("Subject", dataSet, 0);

                            ToAddress = ToEmailAddress;// Returnstring(dsetData, ToAddress, i);
                            // BccAddress = Returnstring(dsetData, BccAddress, i);
                            DataView dv = new DataView(dsetData.Tables[0]);
                            dv.RowFilter = "APPROVALUSERID=" + userid;
                            Subject = "File name with " + ReturnString(0, "FILENAME", dsetData, 0) + " has approved by " + dv[0]["APPROVALUSERNAME"];// Returnstring(dsetData, Subject, i);
                            //CheckPasswordField(dsetData, Body);
                            //CheckReferredLinkField(dsetData, Body, Seqno);
                            //CheckPaymentLinkField(dsetData, Body, Seqno);
                            Body = "Dear " + ReturnString(0, "APPROVALUSERNAME", dsetData, 0) + ",\n" + "Approval " +
                                ReturnString(0, "APPROVALORDER", dsetData, 0) +
                                " (" + ReturnString(0, "GROUPNAME", dsetData, 0) + ") " + "\n" + Subject + "\n"
                                + "\n Thanks & Regards\n   VDocs";
                            Body = Returnstring(dsetData, Body);

                            Mail.AddMailCredentials(mailMessage, 1, ToAddress);
                            Mail.AddMailCredentials(mailMessage, 3, BccAddress);
                            mailMessage.Subject = Subject;
                            mailMessage.IsBodyHtml = true;
                            mailMessage.Body = Body;
                            try
                            {
                                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - LoginController.SendEmailForApprovalUsers -- mail sending to " + ToAddress + " with bcc " + BccAddress + " with body " + Body + " mailmessage " + mailMessage, LogMessageType.Informational);
                                client.Send(mailMessage);

                            }
                            catch (Exception ex)
                            {
                                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - LoginController.SendEmailForApprovalUsers " + ex.StackTrace.ToString(), LogMessageType.Error);
                                string Exp = ex.StackTrace.ToString();
                            }
                         
                    }
                }
                return 100;
            }

            #endregion

            /// <summary>
            /// 
            /// </summary>
            /// <param name="Body"></param>
            /// <param name="Subject"></param>
            /// <param name="oMailMessage"></param>
            /// <param name="FeatureId"></param>
            /// <param name="TemplateType"></param>
            /// <returns></returns>
            string DefaultHTMLFormatForBasicEmails(string Body, string Subject, MailMessage oMailMessage, int FeatureId, int TemplateType, int Seqno)
            {
                string ServerId = "";
                if (ConfigurationManager.AppSettings["PaymentServer"] != null)
                {
                    ServerId = ConfigurationManager.AppSettings["PaymentServer"].ToString();
                }
                else
                {
                    ServerId = "";
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>  <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml'>  <head>      <meta http-equiv='X-UA-Compatible' content='IE=edge' />      <meta name='viewport' content='width=device-width, initial-scale=1' />      <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />      <meta name='apple-mobile-web-app-capable' content='yes' />      <meta name='apple-mobile-web-app-status-bar-style' content='black' />      <meta name='format-detection' content='telephone=no' />      <title>dms template</title>      <style type='text/css'>          /* Resets */          .ReadMsgBody {              width: 100%;              background-color: #ebebeb;          }            .ExternalClass {              width: 100%;              background-color: #ebebeb;          }                .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {                  line-height: 100%;              }            body {              -webkit-text-size-adjust: none;              -ms-text-size-adjust: none;          }            body {              margin: 0;              padding: 0;          }            .yshortcuts a {              border-bottom: none !important;          }            .rnb-del-min-width {              min-width: 0 !important;          }            /* Image width by default for 3 columns */          img[class='rnb-col-3-img'] {              max-width: 170px;          }            /* Image width by default for 2 columns */          img[class='rnb-col-2-img'] {              max-width: 264px;          }            /* Image width by default for 2 columns aside small size */          img[class='rnb-col-2-img-side-xs'] {              max-width: 180px;          }            /* Image width by default for 2 columns aside big size */          img[class='rnb-col-2-img-side-xl'] {              max-width: 350px;          }            /* Image width by default for 1 column */          img[class='rnb-col-1-img'] {              max-width: 550px;          }            /* Image width by default for header */          img[class='rnb-header-img'] {              max-width: 590px;          }            @media screen and (max-width: 480px) {              td[class='rnb-container-padding'] {                  padding-left: 10px !important;                  padding-right: 10px !important;              }                /* force container nav to (horizontal) blocks */              td[class='rnb-force-nav'] {                  display: block;              }          }            @media only screen and (max-width : 600px) {                /* center the address &amp; social icons */              .rnb-text-center {                  text-align: center !important;              }                /* force container columns to (horizontal) blocks */              td[class='rnb-force-col'] {                  display: block;                  padding-right: 0 !important;                  padding-left: 0 !important;              }                table[class='rnb-container'] {                  width: 100% !important;              }                table[class='rnb-btn-col-content'] {                  width: 100% !important;              }                table[class='rnb-col-3'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-bottom: 10px;                  padding-bottom: 10px;                  border-bottom: 1px solid #eee;              }                table[class='rnb-last-col-3'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;              }                table[class='rnb-col-2'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-bottom: 10px;                  padding-bottom: 10px;                  border-bottom: 1px solid #eee;              }                table[class='rnb-col-2-noborder-onright'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-bottom: 10px;                  padding-bottom: 10px;              }                table[class='rnb-col-2-noborder-onleft'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;                  /* change left/right padding and margins to top/bottom ones */                  margin-top: 10px;                  padding-top: 10px;              }                table[class='rnb-last-col-2'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;              }                table[class='rnb-col-1'] {                  /* unset table align='left/right' */                  float: none !important;                  width: 100% !important;              }                img[class='rnb-col-3-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-2-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-2-img-side-xs'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-2-img-side-xl'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-col-1-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                img[class='rnb-header-img'] {                  /**max-width:none !important;**/                  width: 100% !important;              }                td[class='rnb-mbl-float-none'] {                  float: inherit !important;              }          }      </style>      <!--[if gte mso 11]><style type='text/css'>table{border-spacing: 0;	}table td {border-collapse: separate;}</style><![endif]-->      <!--[if !mso]><!-->      <style type='text/css'>          table {              border-spacing: 0;          }                table td {                  border-collapse: collapse;              }      </style>      <!--<![endif]-->  </head>  <body>        <table border='0' width='100%' cellpadding='0' cellspacing='0' class='main-template' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>              <tbody>              <tr style='display: none !important; font-size: 1px;'>                  <td></td>                  <td></td>              </tr>                <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>  ");


                sb.Append(" &nbsp; </td>              </tr>              <tr>     <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                  <table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;' name='Layout_9' id='Layout_9'>                          <tbody>                              <tr>                                  <td class='rnb-del-min-width' valign='top' align='center' style='min-width: 590px;'>                                        <table width='590' class='rnb-container' cellpadding='0' border='0' bgcolor='#D1D1D1' align='center' cellspacing='0' style='background-color: #D1D1D1;'>                                          <tbody>                                              <tr>                                                  <td valign='top' align='center'>                                                              <img border='0' hspace='0' vspace='0' width='590' class='rnb-header-img' alt='' style='display: block; border-radius: 0px; width: 590px; max-width: 600px !important;' src='" + ReturnApplicationPath("Email-head.png") + "'>                                                  </td>                                              </tr>                                          </tbody>                                      </table>  ");
                sb.Append("</td>                              </tr>                          </tbody>                      </table>                                                                                        </td>              </tr>              <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                                                                          <table width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='background-color: #D1D1D1;' name='Layout_10' id='Layout_10'>                            <tbody>                              <tr>                                    <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                          <table border='0' width='590' cellpadding='0' cellspacing='0' class='rnb-container' bgcolor='#ffffff' style='height: 0px; border-radius: 0px; border-collapse: separate; padding-left: 20px; padding-right: 20px; background-color: rgb(255, 255, 255);'>                                          <tbody>                                              <tr>                                                    <td class='rnb-container-padding' bgcolor='#ffffff' style='background-color: #ffffff; font-size: px; font-family: ; color: ;'>                                                        <table border='0' cellpadding='0' cellspacing='0' class='rnb-columns-container' align='center' style='margin: auto;'>                                                          <tbody>                                                              <tr>                                                                      <td class='rnb-force-col' align='center'>                                                                          <table border='0' cellspacing='0' cellpadding='0' align='center' class='rnb-col-1'>                                                                            <tbody>                                                                              <tr>                                                                                  <td height='10'></td>                                                                              </tr>                                                                                  <tr>                                                                                    <td style='font-size: 18px; font-family: Arial,Helvetica,sans-serif; color: #999; font-weight: normal; text-align: center;'>                                                                                          <span style='color: #999; font-weight: normal;'><span style='color: #FF0000'>" + Subject + "</span></span>                                                                                  </td>                                                                              </tr>                                                                              <tr>                                                                                  <td height='10'></td>                                                                              </tr>                                                                              </tbody>                                                                      </table>                                                                      </td>                                                                      </tr>                                                          </tbody>                                                      </table>                                                    </td>                                              </tr>                                            </tbody>                                      </table>                                    </td>                              </tr>                            </tbody>                      </table>                                </td>              </tr>              <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                                                                                    <table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;' name='Layout_11'>                          <tbody>                              <tr>                                    <td class='rnb-del-min-width' align='center' valign='top' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;'>                                        <table width='590' border='0' cellpadding='0' cellspacing='0' class='rnb-container' bgcolor='#ffffff' style='border-radius: 0px; border-collapse: separate; padding-left: 20px; padding-right: 20px; background-color: rgb(255, 255, 255);'>                                            <tbody>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px;'>&nbsp;</td>                                              </tr>                                              <tr>                                                    <td valign='top' class='rnb-container-padding' bgcolor='#ffffff' style='background-color: #ffffff;' align='left'>                                                        <table width='100%' border='0' cellpadding='0' cellspacing='0' class='rnb-columns-container'>                                                          <tbody>                                                              <tr>                                                                        <td class='rnb-force-col' valign='top' style='padding-right: 0px;'>                                                                                <table border='0' valign='top' cellspacing='0' cellpadding='0' width='100%' align='left' class='rnb-col-1'>                                                                              <tbody>                                                                              <tr>                                                                                    <td style='font-size: 13px; font-family: Arial,Helvetica,sans-serif, sans-serif; color: #555;'>");
                sb.Append("" + Body + "</td>  </tr>  </tbody> </table> </td>  </tr>  </tbody>   </table>   </td>    </tr>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px; border-bottom: 0px;'>&nbsp;</td>                                              </tr>                                          </tbody>                                      </table>                                    </td>                              </tr>                          </tbody>                      </table>                      </td>              </tr>              <tr>                      <td align='center' valign='top' bgcolor='#D1D1D1' style='background-color: #D1D1D1;'>                                                                                                                    <table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;' name='Layout_8'>                          <tbody>                              <tr>                                    <td class='rnb-del-min-width' align='center' valign='top' bgcolor='#D1D1D1' style='min-width: 590px; background-color: #D1D1D1;'>                                        <table width='590' border='0' cellpadding='0' cellspacing='0' class='rnb-container' bgcolor='#ffffff' style='border-radius: 0px; border-collapse: separate; padding-left: 20px; padding-right: 20px; background-color: rgb(255, 255, 255);'>                                            <tbody>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px;'>&nbsp;</td>                                              </tr>                                              <tr>                                                    <td valign='top' class='rnb-container-padding' bgcolor='#ffffff' style='background-color: #ffffff;' align='left'>                                                        <table width='100%' border='0' cellpadding='0' cellspacing='0' class='rnb-columns-container'>                                                          <tbody>                                                              <tr>                                                                        <td class='rnb-force-col' valign='top' style='padding-right: 0px;'>                                                                                <table border='0' valign='top' cellspacing='0' cellpadding='0' width='100%' align='left' class='rnb-col-1'>                                                                              <tbody>                                                                              <tr>                                                                                    <td style='font-size: 13px; font-family: Arial,Helvetica,sans-serif, sans-serif; color: #555;'>                                                                                      <div style='text-align: right;'><span style='color: #B22222;'></span></div>                                                                                  </td>                                                                              </tr>                                                                              </tbody>                                                                      </table>                                                                      </td>                                                              </tr>                                                          </tbody>                                                      </table>                                                    </td>                                              </tr>                                              <tr>                                                  <td height='20' style='font-size: 1px; line-height: 1px; border-bottom: 0px;'>&nbsp;</td>                                              </tr>                                          </tbody>                                      </table>                                    </td>                              </tr>                          </tbody>                      </table>                      </td>              </tr>     <tr>  	  		  		<td align='center' valign='top' bgcolor='#D1D1D1' style='background-color:#D1D1D1;'>  		   			  			<table class='rnb-del-min-width' width='100%' cellpadding='0' border='0' cellspacing='0' style='min-width:590px; background-color:#D1D1D1;' name='Layout_968' id='Layout_968'>  				<tbody><tr>  					  					<td class='rnb-del-min-width' valign='top' align='center' bgcolor='#D1D1D1' style='min-width:590px; background-color:#D1D1D1;'>  						  						<table width='100%' cellpadding='0' border='0' height='30' cellspacing='0' bgcolor='#D1D1D1' style='background-color:#D1D1D1;'>  							<tbody><tr>  								  								<td valign='top' height='30'>  									<img width='20' height='30' style='display:block; max-height:30px; max-width:20px;' alt='' src='http://img.mailinblue.com/new_images/rnb/rnb_space.gif'>  								</td>  							</tr>  						</tbody></table>  					</td>  				</tr>  			</tbody></table>   		</td>  	</tr>         </tbody>      </table> </body>  </html>  ");


                if (FeatureId == 400 && TemplateType == 12)
                {
                    if (Body.Contains("PaymentLink"))
                    {
                        string paymentpath = "http://payments.foodontips.com/Home.aspx?Id=" + Seqno + "&Server=" + ServerId;
                        string sBody = "<br/><a href='" + paymentpath + "' ";
                        sBody += "<img alt='pay now' src='" + ReturnApplicationPath("email-paynow-btn.png") + "' /> </a>";
                        Body = Body.Replace("PaymentLink", sBody);
                    }
                }

                //if (FeatureId == 300 && TemplateType == 2)
                //{
                //    sb.Append("<a href='" + (System.Configuration.ConfigurationManager.AppSettings["LoginPageUrl"] != "" ? System.Configuration.ConfigurationManager.AppSettings["LoginPageUrl"].ToString() : "#") + "' style='text-decoration: none; color: #ffffff;font-weight: normal;width: 120px;min-height: 40px;background-color: #cf1f26;display: inline-block;font-size: 16px;vertical-align: middle;-webkit-border-radius: 30px;  line-height: 41px;'>");
                //    sb.Append("Click here to login</a>");
                //}
                //if (FeatureId == 300 && TemplateType == 1)
                //{
                //    string VerificationLink = (System.Configuration.ConfigurationManager.AppSettings["RestaurantVerifiedUrl"] != "" ? System.Configuration.ConfigurationManager.AppSettings["RestaurantVerifiedUrl"].ToString() + "?Id=" + Seqno : "#");

                //    sb.Append("<a href='" + VerificationLink + "' style='text-decoration: none; color: #ffffff;font-weight: normal;width: 120px;min-height: 40px;background-color: #cf1f26;display: inline-block;font-size: 16px;vertical-align: middle;-webkit-border-radius: 30px;  line-height: 41px;'>");
                //    sb.Append("Click here to verify your email address</a>");
                //}
                //else if (FeatureId == 400 && TemplateType == 4)
                //{
                //    sb.Append("<a href='" + (System.Configuration.ConfigurationManager.AppSettings["DefaultPageUrl"] != "" ? System.Configuration.ConfigurationManager.AppSettings["DefaultPageUrl"].ToString() : "#") + "' ");
                //    sb.Append("<img alt='pay now' src='" + ReturnApplicationPath("email-paynow-btn.png") + "' /> </a>");
                //}

                //sb.Append("<table width='100%' bgcolor='#d1d1d1' cellpadding='0' cellspacing='0' border='0'>  	<tbody>  		<tr>  			<td>  				<div class='innerbg'>  				</div>  				<table width='600' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' bgcolor='#ebebeb'>  					<tbody>  						<tr>  							<td width='100%'>  								<table width='600' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth'>  									<tbody>  										<!-- Spacing -->    										<tr>  											<td width='100%' height='21'>  											</td>  										</tr>  										<!-- Spacing -->    										<tr>  											<td align='center' valign='middle' style='font-family: Helvetica, arial, sans-serif; font-size: 13px;color: #ffffff'>  												<p style='text-align: right;'>  													<span style='color: rgb(0, 0, 0);'>Don't want to receive email Updates? <span style='text-decoration: none; color: rgb(255, 0, 0);'>Unsubscribe here</span></span>  												</p>  											</td>  										</tr>  										<!-- Spacing -->    										<tr>  											<td width='100%' height='20'>  											</td>  										</tr>  										<!-- Spacing -->    									</tbody>  								</table>  							</td>  						</tr>  					</tbody>  				</table>  			</td>  		</tr>  	</tbody>  </table>");


                return sb.ToString();
            }

            #region Order Format
            string ReturnTollFreeNo(int OrderId, int FeatureId)
            {
                string TollFreeNo = "1800-123-1234";
                //ArrayList objList = new ArrayList();
                //objList.Add(FeatureId);
                //objList.Add(OrderId);
                //DataSet dsetCountryDetails = VerityHelper.GetCountryDetailsByOrderID(objList);
                //if (dsetCountryDetails != null && dsetCountryDetails.Tables.Count > 0 && dsetCountryDetails.Tables[0].Rows.Count > 0)
                //    return TollFreeNo = REMS_Notification.ReturnString("TollFreeNumber", dsetCountryDetails, 0);

                return TollFreeNo;
            }
            string ReturnAMPMTime(string _Time)
            {
                try
                {
                    TimeSpan timespan = new TimeSpan(Convert.ToDateTime(_Time).Ticks);
                    DateTime time = DateTime.Today.Add(timespan);
                    return time.ToString("hh:mm tt"); // It will give "03:00 AM"
                }
                catch (Exception)
                {
                    return _Time;
                }
            }



            #endregion
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strTo"></param>
            /// <param name="strCC"></param>
            /// <param name="strBody"></param>
            /// <param name="strSubject"></param>
            /// <param name="UserID"></param>
            /// <param name="FeatureID"></param>
            /// <param name="TemplateType"></param>
            /// <param name="Seqno"></param>
            public void SendMail(string strTo, string strCC, string strSubject, string strBody, int UserID, int FeatureID, int TemplateType, int Seqno, long NotifSeqno, DataSet dsetFeatureData)
            {
                try
                {
                    DataSet dset = DMS_Notification.GetFeatureDataBySeqno(TemplateType, FeatureID, Seqno);
                    if (dset != null && dset.Tables.Count > 1 && dset.Tables[1].Rows.Count > 0)
                    {

                        this.strEmailPort = DMS_Notification.ReturnString("SmtpPort", dset, 0, 1);
                        this.strEmailSmtp = DMS_Notification.ReturnString("SmtpHost", dset, 0, 1);
                        this.strEmailUserNm = DMS_Notification.ReturnString("SmtpAuthenticateUser", dset, 0, 1);
                        this.strEmaiPassword = DMS_Notification.ReturnString("SmtpAuthenticatePassword", dset, 0, 1);
                        this.strEnableSSL = Convert.ToBoolean(DMS_Notification.ReturnString("SmtpUseSSL", dset, 0, 1));
                        this.Sender = DMS_Notification.ReturnString("Sender", dset, 0, 1);
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = this.strEnableSSL;
                        smtpClient.Host = this.strEmailSmtp;
                        smtpClient.Port = int.Parse(this.strEmailPort);
                        NetworkCredential credentials = new NetworkCredential(this.strEmailUserNm, this.strEmaiPassword);
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = credentials;
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(this.strEmailUserNm, this.Sender);
                        Mail.AddMailCredentials(mailMessage, 1, strTo.Trim(' '));
                        Mail.AddMailCredentials(mailMessage, 3, strCC.Trim(' '));
                        mailMessage.Subject = strSubject;
                        mailMessage.IsBodyHtml = true;
                        //|| TemplateType == 6 || TemplateType == 3 removed from buildorderformat on nov 26 by hafeez,for dinein we dont want to show item details

                        {
                            mailMessage.Body = DefaultHTMLFormatForBasicEmails(strBody, strSubject, mailMessage, FeatureID, TemplateType, Seqno);
                        }

                        try
                        {


                            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - Mail Data : " + " - " + " Info=" + "Mail Data : To Address :" + strTo + ", CC :" + strCC + ", Subject :" + strSubject + ", Body :" + strBody + "", LogMessageType.Informational);

                            TrackMail(NotifSeqno, false, 2, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, "");
                            smtpClient.Send(mailMessage);
                            TrackMail(NotifSeqno, true, 3, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, "Sucessfully Message Sent");

                        }
                        catch (Exception ex)
                        {
                            TrackMail(NotifSeqno, false, 3, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, ex.ToString());

                        }
                    }
                }
                catch (Exception ex2)
                {
                    TrackMail(NotifSeqno, false, 3, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, ex2.ToString());

                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strTo"></param>
            /// <param name="strCC"></param>
            /// <param name="strSubject"></param>
            /// <param name="strBody"></param>
            /// <param name="UserID"></param>
            /// <param name="FeatureID"></param>
            /// <param name="TemplateType"></param>
            /// <param name="Seqno"></param>
            public string SendMailWithoutFormat(string strTo, string strCC, string strSubject, string strBody, int UserID, int FeatureID, int TemplateType, int Seqno)
            {
                try
                {
                    DataSet dset = DMS_Notification.GetFeatureDataBySeqno(TemplateType, FeatureID, Seqno);
                    if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
                    {

                        this.strEmailPort = DMS_Notification.ReturnString("SmtpPort", dset, 0, 0);
                        this.strEmailSmtp = DMS_Notification.ReturnString("SmtpHost", dset, 0, 0);
                        this.strEmailUserNm = DMS_Notification.ReturnString("SmtpAuthenticateUser", dset, 0, 0);
                        this.strEmaiPassword = DMS_Notification.ReturnString("SmtpAuthenticatePassword", dset, 0, 0);
                        this.strEnableSSL = Convert.ToBoolean(DMS_Notification.ReturnString("SmtpUseSSL", dset, 0, 0));
                        this.Sender = DMS_Notification.ReturnString("Sender", dset, 0);
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = this.strEnableSSL;
                        smtpClient.Host = this.strEmailSmtp;
                        smtpClient.Port = int.Parse(this.strEmailPort);
                        NetworkCredential credentials = new NetworkCredential(this.strEmailUserNm, this.strEmaiPassword);
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = credentials;
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(this.strEmailUserNm, this.Sender);
                        Mail.AddMailCredentials(mailMessage, 1, strTo.Trim(' '));
                        Mail.AddMailCredentials(mailMessage, 3, strCC.Trim(' '));
                        mailMessage.Subject = strSubject;
                        mailMessage.IsBodyHtml = true;
                        //|| TemplateType == 6 || TemplateType == 3 removed from buildorderformat on nov 26 by hafeez,for dinein we dont want to show item details

                        {
                            mailMessage.Body = DefaultHTMLFormatForBasicEmails(strBody, strSubject, mailMessage, FeatureID, TemplateType, Seqno);

                        }

                        try
                        {


                            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - Mail Data : " + " - " + " Info=" + "Mail Data : To Address :" + strTo + ", CC :" + strCC + ", Subject :" + strSubject + ", Body :" + strBody + "", LogMessageType.Informational);

                            //TrackMail(NotifSeqno, false, 2, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, "");
                            smtpClient.Send(mailMessage);
                            return "Sent";
                            //TrackMail(NotifSeqno, true, 3, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, "Sucessfully Message Sent"); 
                        }
                        catch (Exception ex)
                        {
                            return ex.ToString();
                            //  TrackMail(NotifSeqno, false, 3, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, ex.ToString());

                        }
                    }
                    return "";
                }
                catch (Exception ex2)
                {
                    return ex2.ToString();
                    // TrackMail(NotifSeqno, false, 3, strTo, strCC, strBody, strSubject, UserID, FeatureID, TemplateType, Seqno, ex2.ToString()); 
                }
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="bFlg"></param>
            /// <param name="Type"></param>
            /// <param name="strTo"></param>
            /// <param name="strCC"></param>
            /// <param name="strBody"></param>
            /// <param name="strSubject"></param>
            /// <param name="UserID"></param>
            /// <param name="FeatureID"></param>
            /// <param name="TemplateType"></param>
            /// <param name="Seqno"></param>
            /// <param name="Msg"></param>
            private static void TrackMail(long notifSeqno, bool bFlg, int Type, string strTo, string strCC, string strBody, string strSubject, int UserID, int FeatureID, int TemplateType, int Seqno, string Msg)
            {
                try
                {
                    AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - DMS_Notification.InsertNotifStatus" + " ,  Before Save  ", LogMessageType.Informational);

                    DMS_Notification.NotifClass objClass = new DMS_Notification.NotifClass();
                    objClass._FeatureId = FeatureID;
                    objClass._featureSeqno = Seqno;
                    objClass._TemplateType = TemplateType;
                    objClass._Type = Type;
                    objClass._CreatedBy = UserID;
                    objClass._ToAddress = strTo;
                    objClass._BccAddress = strCC;
                    objClass._Subject = strSubject;
                    objClass._Body = strBody;
                    objClass.Exception = Msg;
                    objClass.NotificationSent = bFlg;
                    objClass._NotifSeqno = notifSeqno;
                    DMS_Notification.InsertNotifStatus(objClass);
                }
                catch (Exception ex)
                {
                    AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - DMS_Notification.TrackMail" + " , SPName : InsertNotifStatus  " + ex.Message.ToString(), LogMessageType.Error);

                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="flg"></param>
            /// <param name="Data"></param>
            public static void AddMailCredentials(MailMessage msg, int flg, string Data)
            {
                if (Data != "")
                {
                    string[] array = Data.Trim(',').Split(new char[]
				{
					','
				});
                    switch (flg)
                    {
                        case 1:
                            for (int i = 0; i < array.Length; i++)
                            {
                                msg.To.Add(new MailAddress(array[i]));
                            }
                            return;
                        case 2:
                            for (int j = 0; j < array.Length; j++)
                            {
                                msg.CC.Add(new MailAddress(array[j]));
                            }
                            return;
                        case 3:
                            for (int k = 0; k < array.Length; k++)
                            {
                                msg.Bcc.Add(new MailAddress(array[k]));
                            }
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        #endregion
       public class NotifClass
       {
           #region Varibales
           public long _NotifSeqno = 0;
           public int _FeatureId = 0;
           public int _Type = 0;
           public int _featureSeqno = 0;
           public int _TemplateType = 0;
           public string _Message = "";
           public string _ToAddress = "";
           public string _BccAddress = "";
           public string _Subject = "";
           public string _Body = "";
           public int _CreatedBy = 1;
           public bool NotificationSent = false;
           public string Exception = "";
           public int NotificationType = 1;
       }
       public static int FeatureID = 0;
       public static int TemplateType = 0;
       public static int Seqno = 0; public static int CustomerID = 0;
       public static long NotifSeqno = 0;
            #endregion
       public static string SendNotification(int _TemplateType, int _FeatureID, string Msg, int _Seqno, int UserId)
       {
           NotifClass objClass = new NotifClass();
           objClass._TemplateType = _TemplateType;
           objClass._FeatureId = _FeatureID;
           objClass._featureSeqno = _Seqno;
           objClass._Type = 1;
           objClass._Message = Msg;
           objClass._CreatedBy = UserId;
           NotifSeqno =InsertNotifStatus(objClass);

           TemplateType = _TemplateType;
           FeatureID = _FeatureID;
           Seqno = _Seqno;
           if (TemplateType != 0)
           {

               SendEMAIL(0, _FeatureID, _Seqno, GetFeatureDataBySeqno(TemplateType, _FeatureID, _Seqno), "English", UserId, TemplateType, NotifSeqno, 2);// EMAIL
               //SendEMAIL(0, _FeatureID, _Seqno, GetFeatureDataBySeqno(TemplateType, _FeatureID, _Seqno), "English", UserId, TemplateType, NotifSeqno, 1); // SMS
           }
           return "";
       }
        public static long InsertNotifStatus(NotifClass objClass)
        {            
            try
            {
                ArrayList objList = new ArrayList();
                objList.Add(objClass._NotifSeqno);
                objList.Add(objClass._Type);
                objList.Add(objClass._FeatureId);
                objList.Add(objClass._featureSeqno);
                objList.Add(objClass._TemplateType);
                objList.Add(objClass._Message);
                objList.Add(objClass._ToAddress);
                objList.Add(objClass._BccAddress);
                objList.Add(objClass._Subject);
                objList.Add(objClass._Body);
                objList.Add(objClass._CreatedBy);
                objList.Add(objClass.NotificationSent);
                objList.Add(objClass.Exception);
                objList.Add(objClass.NotificationType);
                long NotifSeqno = VerityHelper.InsertNotifStatus(objList);
                return NotifSeqno;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iFlg"></param>
        /// <param name="iFeature"></param>
        /// <param name="SeqNo"></param>
        /// <param name="dsetData"></param>
        /// <param name="strLanguage"></param>
        /// <param name="UserID"></param>
        public static void SendEMAIL(int iFlg, int iFeature, int SeqNo, DataSet dsetData, string strLanguage, int UserID, int TemplateType, long NotifSeqno, int FormatType)
        {
            DataSet dataSet = GetTemplateByFeature(TemplateType, FeatureID, FormatType);
            string ToAddress = string.Empty, BccAddress = string.Empty, Subject = string.Empty, Body = string.Empty;

            if (!(dsetData == null )? dsetData.Tables.Count > 0 ? dsetData.Tables[0].Rows.Count == 0:false:false)
                return;
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                try
                {
                    if (FormatType == 2) //email
                    {

                        ToAddress = ReturnString("ToAddress", dataSet, 0);
                        BccAddress = ReturnString("BccAddress", dataSet, 0);
                        Subject = ReturnString("Subject", dataSet, 0);
                        Body = ReturnString("Format", dataSet, 0);
                        ToAddress = Returnstring(dsetData, ToAddress);
                        BccAddress = Returnstring(dsetData, BccAddress);
                        Subject = Returnstring(dsetData, Subject);
                        CheckPasswordField(dsetData, Body);
                        CheckReferredLinkField(dsetData, Body, Seqno);
                        CheckPaymentLinkField(dsetData, Body, Seqno);
                        Body = Returnstring(dsetData, Body);
                    }
                    else if (FormatType == 1) //sms
                    {
                        ToAddress = ReturnString("ToAddress", dataSet, 0);
                        Body = ReturnString("Format", dataSet, 0);
                        ToAddress = Returnstring(dsetData, ToAddress);
                        CheckPasswordField(dsetData, Body);
                        CheckReferredLinkField(dsetData, Body, Seqno);
                        CheckPaymentLinkField(dsetData, Body, Seqno);
                        Body = Returnstring(dsetData, Body);
                    }
                    #region CheckForKeywords
                    try
                    {
                        for (int i = 0; i < lstKeyWords.Count; i++)
                        {
                            if (Body.Contains(lstKeyWords[i]._Key))
                            {
                                Body = Body.Replace(lstKeyWords[i]._Key, lstKeyWords[i]._Value);
                            }
                            if (Subject.Contains(lstKeyWords[i]._Key))
                            {
                                Subject = Subject.Replace(lstKeyWords[i]._Key, lstKeyWords[i]._Value);
                            }
                        }
                    }
                    catch (Exception)
                    {


                    }
                    #endregion

                    Mail mail = null;
                    SMSAPI objsms = null;
                    try
                    {
                        mail = new Mail();
                        objsms = new SMSAPI();

                    }
                    catch (Exception ex)
                    {

                    }
                    if (FormatType == 2)
                    {
                        mail.SendMail(ToAddress, BccAddress, Subject, Body, UserID, iFeature, TemplateType, Seqno, NotifSeqno, dsetData);
                    }
                    else if (FormatType == 1)
                    {
                        string[] arrOfString = null;
                        if (ToAddress.Contains(","))
                            arrOfString = ToAddress.Split(',');
                        if (ToAddress.Contains(";"))
                            arrOfString = ToAddress.Split(',');
                        if (ToAddress.Contains("~"))
                            arrOfString = ToAddress.Split(',');
                        else
                            arrOfString = ToAddress.Split(',');
                        objsms.SMSInitiate(Body, arrOfString, NotifSeqno, UserID, FeatureID, TemplateType, Seqno, 0);
                    }
                }
                catch (Exception ex3)
                {
                    DMS_Notification.NotifClass objClass = new DMS_Notification.NotifClass();
                    objClass._FeatureId = FeatureID;
                    objClass._featureSeqno = Seqno;
                    objClass._TemplateType = TemplateType;
                    objClass._Type = 2;
                    objClass._CreatedBy = UserID;
                    objClass._ToAddress = ToAddress;
                    objClass._BccAddress = BccAddress;
                    objClass._Subject = Subject;
                    objClass._Body = Body;
                    objClass.Exception = ex3.Message;
                    objClass._NotifSeqno = NotifSeqno;
                    DMS_Notification.InsertNotifStatus(objClass);

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemplateType"></param>
        /// <param name="FeatureID"></param>
        /// <returns></returns>
        public static DataSet GetTemplateByFeature(int TemplateType, int FeatureID, int FormatType)
        {
            DataSet dset = new DataSet();
            ArrayList objList = new ArrayList();
            objList.Add(TemplateType); objList.Add(FeatureID); objList.Add(FormatType);
            //For  template types
            dset = VerityHelper.GetEmailTemplateByTemplateType(objList);
            if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                return dset;
            }
            else
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - DMS_Notification.GetTemplateByFeature" + " ,  No Template define", LogMessageType.Informational);

            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="dset"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string ReturnString(string col, DataSet dset, int row)
        {
            if (dset.Tables[0].Columns.Contains(col))
                return dset.Tables[0].Rows[row][col].ToString();
            return "";
        }
        private static string Returnstring(DataSet dsetData, string strFrom)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            if (strFrom.Contains("#"))
            {
                string text = "";
                if (strFrom != "")
                {
                    for (int i = 0; i < strFrom.Length; i++)
                    {
                        if (strFrom[i].ToString() == "#")
                        {
                            if (strFrom[i].ToString() == "#")
                            {
                                num++;
                            }
                        }
                        else if (strFrom[i].ToString() == "%")
                        {
                            if (strFrom[i].ToString() == "%")
                            {
                                num++;
                            }
                            if (num == 1)
                            {
                                stringBuilder.Append(ReturnFieldData(text.Replace('#', ' ').Replace('>', ' '), dsetData));
                                text = "";
                            }
                            if (num == 1)
                            {
                                num = 0;
                            }
                        }
                        int num2;
                        if (num == 1)
                        {
                            text += strFrom[i];
                            num2 = 1;
                        }
                        else
                        {
                            num2 = 0;
                        }
                        if (num2 == 0)
                        {
                            if (strFrom[i].ToString() == "\n")
                            {
                                stringBuilder.Append("<br>");
                            }
                            stringBuilder.Append(strFrom[i]);
                        }
                        if (i == strFrom.Length - 1 || strFrom[i + 1].ToString() == "%")
                        {
                            num = 0;
                        }
                    }
                }
            }
            else
            {
                stringBuilder.Append(strFrom);
            }

            return stringBuilder.ToString().Replace('%', ' ');
        }
        public static string ReturnString(string col, DataSet dset, int row, int table)
        {
            if (dset.Tables[table].Columns.Contains(col))
                return dset.Tables[table].Rows[row][col].ToString();
            return "";
        }
        public static void CheckPasswordField(DataSet dset, string Body)
        {
            if (dset.Tables[0].Columns.Contains("PASSWORD") && Body.Contains("PASSWORD"))
            {
                DataRow dr = dset.Tables[0].Rows[0];
                dr["PASSWORD"] = CommonUtilities.Decrypt(dr["PASSWORD"].ToString());
                dset.Tables[0].AcceptChanges();
            }
        }
        public static void CheckReferredLinkField(DataSet dset, string Body, int Seqno)
        {
            if (dset.Tables[0].Columns.Contains("ReferralLink") && Body.Contains("ReferralLink"))
            {
                DataRow dr = dset.Tables[0].Rows[0];
                dr["ReferralLink"] = "ReferralSignUpLink";
                dset.Tables[0].AcceptChanges();
            }
        }
        public static void CheckPaymentLinkField(DataSet dset, string Body, int Seqno)
        {
            if (dset.Tables[0].Columns.Contains("PaymentLink") && Body.Contains("PaymentLink"))
            {
                DataRow dr = dset.Tables[0].Rows[0];
                dr["PaymentLink"] = "PaymentLink";
                dset.Tables[0].AcceptChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ReturnFieldData(string Field, DataSet d)
        {
            string text = string.Empty;

            if (d.Tables[0].Columns[Field.Trim(new char[]
			{
				' '
			})].ColumnName == Field.Trim(new char[]
			{
				' '
			}))
            {
                return d.Tables[0].Rows[0][Field.Trim(new char[]
				{
					' '
				})].ToString();
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemplateType"></param>
        /// <param name="FeatureID"></param>
        /// <returns></returns>
        public static DataSet GetFeatureDataBySeqno(int TemplateType, int FeatureID, int Seqno)
        {
            DataSet dset = new DataSet();
            ArrayList objList = new ArrayList();
            objList.Add(TemplateType); objList.Add(FeatureID); objList.Add(Seqno);
            dset = VerityHelper.GetFeatureDataBySeqno(objList);
            if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                return dset;
            }
            else
            {
                DMS_Notification.NotifClass objClass = new DMS_Notification.NotifClass();
                objClass._FeatureId = FeatureID;
                objClass._featureSeqno = Seqno;
                objClass._TemplateType = TemplateType;
                objClass._Type = 2;
                objClass.Exception = " Records not found with FeatureID" + FeatureID + " And SequenceNo" + Seqno;
                objClass._NotifSeqno = NotifSeqno;
                DMS_Notification.InsertNotifStatus(objClass);
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - DMS_Notification.GetTemplateByFeature" + " ,  No Template define", LogMessageType.Informational);

            }
            return dset;
        }
    }
}

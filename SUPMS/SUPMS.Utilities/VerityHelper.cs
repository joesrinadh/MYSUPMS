using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Web;
using Framework.AsyncLogger;
using VERITY.DMS.MODEL;
namespace SUPMS.Infrastructure.Utilities
{

    /// <summary>
    /// 
    /// </summary>
    public partial class VerityHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetDocumentsOfAllSeqnosAndRespectiveVersions(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetDocumentsOfAllSeqnosAndRespectiveVersions" + " , SPName : SP_GETFILESANDRESPECTIVEVERSIONSPATHS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETFILESANDRESPECTIVEVERSIONSPATHS", objList.ToArray());

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetDocumentsOfAllSeqnosAndRespectiveVersions" + " , SPName : SP_GETFILESANDRESPECTIVEVERSIONSPATHS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int CopyVersion(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CopyVersion" + " , SPName : SP_COPYVERSIONOFDOCUMENT  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_COPYVERSIONOFDOCUMENT", objList.ToArray());

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CopyVersion" + " , SPName : SP_COPYVERSIONOFDOCUMENT  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet ChangeExpiredFilesStatus(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.ChangeExpiredFilesStatus" + " , SPName : SP_EXPIREDFILESSTATUSCHANGES  " + ConvertObjectToString(objList), LogMessageType.Informational);

               return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_EXPIREDFILESSTATUSCHANGES", objList.ToArray());
                
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.ChangeExpiredFilesStatus" + " , SPName : SP_EXPIREDFILESSTATUSCHANGES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveCustomFields(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveCustomFields" + " , SPName : SP_SAVECUSTOMFIELDS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVECUSTOMFIELDS", objList.ToArray());
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0 && dset.Tables[0].Columns.Contains("Seqno"))
                    return Convert.ToInt32(dset.Tables[0].Rows[0][0]);
                else
                    return -1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveCustomFields" + " , SPName : SP_SAVECUSTOMFIELDS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static DataSet GetListOfPendingNotifications()
        {

            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetListOfPendingNotifications" + " , SPName : SP_GETPENDINGNOTIFICATION  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETPENDINGNOTIFICATION");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetListOfPendingNotifications" + " , SPName : SP_GETPENDINGNOTIFICATION  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet Getfiledetailsbyseqno(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.Getfiledetailsbyseqno" + " , SPName : SP_GETNEXTLAZYLOADUSERSRESULTS  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETFILEDETAILSBYSEQNO", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.Getfiledetailsbyseqno" + " , SPName : SP_GETNEXTLAZYLOADUSERSRESULTS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet CheckWorkflowProcess(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CheckWorkflowProcess" + " , SPName : SP_GETNEXTLAZYLOADUSERSRESULTS  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETNEXTLAZYLOADUSERSROLESGROUPSRESULTS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CheckWorkflowProcess" + " , SPName : SP_GETNEXTLAZYLOADUSERSRESULTS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetNextUsersRolesGroups(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetNextUsers" + " , SPName : SP_GETNEXTLAZYLOADUSERSRESULTS  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETNEXTLAZYLOADUSERSROLESGROUPSRESULTS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetNextUsers" + " , SPName : SP_GETNEXTLAZYLOADUSERSRESULTS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// get shareid before share link
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetLastShareIdOnSocialMedia(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetTFilesShareDeatailsOrInsertNewR" + " , SPName : SP_GETSHAREDOCUMENTNEXTID  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETSHAREDOCUMENTNEXTID", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetTFilesShareDeatailsOrInsertNewR" + " , SPName : SP_GETSHAREDOCUMENTNEXTID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetTFilesShareDeatailsOrInsertNewR(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetTFilesShareDeatailsOrInsertNewR" + " , SPName : SP_GETTFILESHAREID  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETTFILESHAREID", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetTFilesShareDeatailsOrInsertNewR" + " , SPName : SP_GETTFILESHAREID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// get file path by seqno and version of file
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetFilePathBySeqNoAndVersion(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetFilePathBySeqNoAndVersion" + " , SPName : SP_GETDETAILSBYSHAREDID  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETDETAILSBYSHAREDID", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetFilePathBySeqNoAndVersion" + " , SPName : SP_GETDETAILSBYSHAREDID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetDocuemtsAndRespectiveDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetDocuemtsAndRespectiveDetails" + " , SPName : SP_GETFILESANDRESPECTIVEVERSIONS  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETFILESANDRESPECTIVEVERSIONS", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetDocuemtsAndRespectiveDetails" + " , SPName : SP_GETFILESANDRESPECTIVEVERSIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SendFilesAndFoldersEmail(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SendFilesAndFoldersEmail" + " , SPName : SP_SAVEFILESSHAREDETAILS  ", LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_SAVEFILESSHAREDETAILS", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SendFilesAndFoldersEmail" + " , SPName : SP_SAVEFILESSHAREDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// SAVE FILE(S) METADATA FROM TDOCYUMENTS GRID 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveFilesMetadata(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveFilesMetadata" + " , SPName : SP_SAVEFILESMETADATA  ", LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_SAVEFILESMETADATA", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveFilesMetadata" + " , SPName : SP_SAVEFILESMETADATA  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllEventsDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAllEventsDetails" + " , SPName : SP_GETALLEVENTS  ", LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETALLEVENTS", objList.ToArray());
                return dset;
            }

            catch (Exception ex)
            {
                DataSet dset = new DataSet();
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAllEventsDetails" + " , SPName : SP_GETALLEVENTS  " + ex.Message.ToString(), LogMessageType.Error);
                return dset;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static int CloseEvent(ArrayList objlist)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.CloseEvent" + " , SPName : SP_ClOSEEVENT" + ConvertObjectToString(objlist), LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_ClOSEEVENT", objlist.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.CloseEvent" + " , SPName : SP_ClOSEEVENT  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// GetTING Event Details BASED ON Id
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static DataSet GetEventDetailsById(ArrayList objlist)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetEventDetailsById" + " , SPName : SP_GETEVENTDETAILSBYID" + ConvertObjectToString(objlist), LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETEVENTDETAILSBYID", objlist.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetEventDetailsById" + " , SPName : SP_GETEVENTDETAILSBYID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetUserEvents(ArrayList objList)
        {
            AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserEvents" + " , SPName : SP_GETUSEREVENTS  ", LogMessageType.Informational);
            try
            {
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETUSEREVENTS", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserEvents" + " , SPName : SP_GETUSEREVENTS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// SAVE EVENT
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static int SaveEvent(ArrayList objlist)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.SaveEvent" + " , SPName : SP_SAVEEVENT  " + ConvertObjectToString(objlist), LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVEEVENT", objlist.ToArray());
                if (dset != null ? dset.Tables.Count > 0 ? dset.Tables[0].Rows.Count > 0 : false : false)
                {
                    return Convert.ToInt32(dset.Tables[0].Rows[0][0]);
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.SaveEvent" + " , SPName : SP_SAVEEVENT  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet AccessUploadFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.AccessUploadFiles" + " , SPName : SP_GETACCESSUPLOADFILES  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETACCESSUPLOADFILES", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.AccessUploadFiles" + " , SPName : SP_GETACCESSUPLOADFILES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int UpdateNotificationsReadStatus(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.UpdateNotificationsReadStatus" + " , SPName : SP_UPDATENOTIFICATIONSREADSTATUS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_UPDATENOTIFICATIONSREADSTATUS", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.UpdateNotificationsReadStatus" + " , SPName : SP_UPDATENOTIFICATIONSREADSTATUS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetAdminTaskNotifications(ArrayList objList)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GETTASKNOTIFICATIONS" + " , SPName : SP_GETTASKNOTIFICATIONS  " + ConvertObjectToString(objList), LogMessageType.Informational);

            try
            {
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETTASKNOTIFICATIONS", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GETTASKNOTIFICATIONS" + " , SPName : SP_GETTASKNOTIFICATIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetAdminNotifications(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAdminNotifications" + " , SPName : SP_GETADMINNOTIFICATIONS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETADMINNOTIFICATIONS", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAdminNotifications" + " , SPName : SP_GETADMINNOTIFICATIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetUserNotifications(ArrayList objList)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetUserNotifications" + " , SPName : SP_GETUSERNOTIFICATIONS  " + ConvertObjectToString(objList), LogMessageType.Informational);

            try
            {
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETUSERNOTIFICATIONS", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetUserNotifications" + " , SPName : SP_GETUSERNOTIFICATIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int ApproveworkflowFile(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.ApproveworkflowFile" + " , SPName : SP_APPROVEWORKFLOWFILEFOLDER" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_APPROVEWORKFLOWFILEFOLDER", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.ApproveworkflowFile" + " , SPName : SP_APPROVEWORKFLOWFILEFOLDER  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetWorkFlowPendingFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetWorkFlowPendingFiles" + " , SPName : SP_GETWORKFLOWPENDINGFILES" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETWORKFLOWPENDINGFILES", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetWorkFlowPendingFiles" + " , SPName : SP_GETWORKFLOWPENDINGFILES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetWorkflowApproval(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetWorkflowApproval" + " , SPName : SP_GETWORKFLOWAPPROVAL" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETWORKFLOWAPPROVAL", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetWorkflowApproval" + " , SPName : SP_GETWORKFLOWAPPROVAL  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetDepartmentWorkflowSettings(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetDepartmentWorkflowSettings" + " , SPName : SP_GETSAVEDDEPARTMENTWFLOWVALUES" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETSAVEDDEPARTMENTWFLOWVALUES", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetDepartmentWorkflowSettings" + " , SPName : SP_GETSAVEDDEPARTMENTWFLOWVALUES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// SAVE WORKFLOW FILES STATUS CHENGE USERS LIST SAVE
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveWorkflowApproval(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveWorkflowApproval" + " , SPName : SP_SAVEWORKFLOWAPPROVAL" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_SAVEWORKFLOWAPPROVAL", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveWorkflowApproval" + " , SPName : SP_SAVEWORKFLOWAPPROVAL  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// get file permissions details for specific userseqnos ex:userseqnos are comma seperated
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetFilePermissionsDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetFilePermissionsDetails" + " , SPName : SP_GETPERMISSIONS" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETPERMISSIONS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetFilePermissionsDetails" + " , SPName : SP_GETPERMISSIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// SETTING PERMISSIONS FOR FILE OR FOLDER
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SavePermissions(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SavePermissions" + " , SPName : SP_SAVEPERMISSIONS" + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_SAVEPERMISSIONS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SavePermissions" + " , SPName : SP_SAVEPERMISSIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// DASH BOARD DOCUMENTS DATA
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDashboardDocumnetGrid(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetDashboardDocumnetGrid" + " , SPName : SP_GETDATAFORDASHBOARD  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETDATAFORDASHBOARD", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetDashboardDocumnetGrid" + " , SPName : SP_GETDATAFORDASHBOARD  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int UpdateCheckoutWithDrawPositions(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.UpdateCheckoutWithDrawPositions" + " , SPName : SP_UPDATECHECKOUTWITHDRAWPOSITIONS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_UPDATECHECKOUTWITHDRAWPOSITIONS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.UpdateCheckoutWithDrawPositions" + " , SPName : SP_UPDATECHECKOUTWITHDRAWPOSITIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet ViewHistoryOfFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.ViewHistoryOfFiles" + " , SPName : SP_GETHISTORYOFFILES  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETHISTORYOFFILES", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.ViewHistoryOfFiles" + " , SPName : SP_GETHISTORYOFFILES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int RestoreFilesToOlderVersion(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.RestoreFilesToOlderVersion" + " , SPName : SP_RESTORETOPREVIOUSVERSION  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_RESTORETOPREVIOUSVERSION", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.RestoreFilesToOlderVersion" + " , SPName : SP_RESTORETOPREVIOUSVERSION  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int CheckInFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CheckInFiles" + " , SPName : SP_CHECKINFILES  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_CHECKINFILES", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CheckInFiles" + " , SPName : SP_CHECKINFILES  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// Final step to check out files in TCHECKOUT TABLE 
        /// </summary>
        /// <returns></returns>
        public static int CheckOutFilesWithDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CheckOutFilesWithDetails" + " , SPName : SP_INSERTCHECKOUTFILESINTB  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_INSERTCHECKOUTFILESINTB", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CheckOutFilesWithDetails" + " , SPName : SP_INSERTCHECKOUTFILESINTB  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetChekoutDetailsOfFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetChekoutDetailsOfFiles" + " , SPName : SP_GETCHECKOUTFILESDETAILS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETCHECKOUTFILESDETAILS", objList.ToArray());

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetChekoutDetailsOfFiles" + " , SPName : SP_GETCHECKOUTFILESDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int AssignFeature(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.AssignFeature" + " , SPName : SP_ASSIGNFEATURE  " + ConvertObjectToString(objList), LogMessageType.Informational);

                SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_ASSIGNFEATURE", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.AssignFeature" + " , SPName : SP_ASSIGNFEATURE  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataSet GetAssignedList(ArrayList obj)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAssignedList" + " , SPName : SP_GETASSIGNEDLIST  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETASSIGNEDLIST", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAssignedList" + " , SPName : SP_GETASSIGNEDLIST  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetAssignedHistoryList(ArrayList objList)
        {

            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAssignedHistoryList" + " , SPName : SP_CLOSEFEATURE  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETASSIGNHISTORY", objList.ToArray());

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAssignedHistoryList" + " , SPName : SP_CLOSEFEATURE  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int CloseTask(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CloseTask" + " , SPName : SP_CLOSEFEATURE  " + ConvertObjectToString(objList), LogMessageType.Informational);

                SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_CLOSEFEATURE", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CloseTask" + " , SPName : SP_CLOSEFEATURE  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="objList"></param>
        ///// <returns></returns>
        //public static DataSet GetTaskById(ArrayList objList)
        //{
        //    try
        //    {
        //        AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetTaskById" + " , SPName : SP_GETTASKBYID  " + ConvertObjectToString(objList), LogMessageType.Informational);

        //        return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETTASKBYID", objList.ToArray());
        //    }

        //    catch (Exception ex)
        //    {
        //        AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetTaskById" + " , SPName : SP_GETTASKBYID  " + ex.Message.ToString(), LogMessageType.Error);
        //        return null;
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveTask(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveTask" + " , SPName : SP_SAVETASK  " + ConvertObjectToString(objList), LogMessageType.Informational);

                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVETASK", objList.ToArray());
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0 && dset.Tables[0].Columns.Contains("Seqno"))
                    return Convert.ToInt32(dset.Tables[0].Rows[0][0]);
                else
                    return -1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveTask" + " , SPName : SP_SAVETASK  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyWBS"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static int DeleteRowFromList(int FId, int seqno)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.DeleteRowFromList" + " - " + "Calling DeleteRowFromList with  Parameter as :FId:" + FId + " & seqno: " + seqno, LogMessageType.Informational);
            try
            {

                ArrayList obj = new ArrayList();
                obj.Add(FId); obj.Add(seqno);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_DELETEFEATUREBYID", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0 && dset.Tables[0].Columns.Contains("RETURNVALUE"))
                    return Convert.ToInt32(dset.Tables[0].Rows[0][0]);
                else
                    return -1;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetAttachmentsBuildingFolderName(ArrayList objList)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAttachmentsBuildingFolderName" + " , SPName : SP_GETATTACHMENTSBUILDINGFOLDERNAME  " + ConvertObjectToString(objList), LogMessageType.Informational);
            try
            {

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETATTACHMENTSBUILDINGFOLDERNAME", objList.ToArray());

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAttachmentsBuildingFolderName" + " , SPName : SP_GETATTACHMENTSBUILDINGFOLDERNAME  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveAttachments(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveAttachments" + " , SPName : SP_SAVEATTACHMENTS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVEATTACHMENTS", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveAttachments" + " , SPName : SP_SAVEATTACHMENTS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static DataSet GETCOMMONFEATUREBYENTITY(ArrayList objlist)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GETCOMMONFEATUREBYENTITY" + " , SPName : SP_GETCOMMONFEATUREBYENTITY  " + ConvertObjectToString(objlist), LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETCOMMONFEATUREBYENTITY", objlist.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GETCOMMONFEATUREBYENTITY" + " , SPName : SP_GETCOMMONFEATUREBYENTITY  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetTaskDetailsById(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetTaskDetailsById" + " , SPName :  SP_GETTASKDETAILSBYID  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETTASKDETAILSBYID", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetTaskDetailsById" + " , SPName :  SP_GETTASKDETAILSBYID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// Save the VoucherConfigurator
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveVoucherConfigurator(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveVoucherConfigurator" + " , SPName :  SP_SAVEAUTOSEQUENCE  " + ConvertObjectToString(objList), LogMessageType.Informational);

                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVEAUTOSEQUENCE", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveVoucherConfigurator" + " , SPName :  SP_SAVEAUTOSEQUENCE  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param featurid="obj"></param>
        /// <returns></returns>
        public static DataSet GETAUTOSEQUENCEBYFEATURE(string obj)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SP_GETAUTOSEQUENCEBYFEATURE" + " - " + obj, LogMessageType.Informational);
            try
            {

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETAUTOSEQUENCEBYFEATURE", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SP_GETAUTOSEQUENCEBYFEATURE" + " - " + ex.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// status messages based on lookuptypeid
        /// </summary>
        /// <param name="lookupTypeId"></param>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static DataSet GetAllStatusesBasedOnLookup(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAllStatusesBasedOnLookup" + " , SPName : SP_GETALLSTATUSESBYLOOKUPTYPEID  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETALLSTATUSESBYLOOKUPTYPEID", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAllStatusesBasedOnLookup" + " , SPName : SP_GETALLSTATUSESBYLOOKUPTYPEID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetNextVoucherNo(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetNextVoucherNo" + " , SPName : SP_GETNEXTVOUCHERNO  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETNEXTVOUCHERNO", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetNextVoucherNo" + " , SPName : SP_GETNEXTVOUCHERNO  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// Update user details like first name etc...
        /// </summary>
        /// <param name="userObj">user details like first name ,last name etc... AS arraylist</param>
        /// <returns>positive integer when success other wise negative integer </returns>
        public static int UpdateUserDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.UpdateUserDetails" + " , SPName : SP_UPDATEPROFILEDETAILS", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_UPDATEPROFILEDETAILS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.UpdateUserDetails" + " , SPName : SP_UPDATEPROFILEDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// UPDATE PROFILE PIC
        /// </summary>
        /// <param name="objList">PIC PATH AS STRING</param>
        /// <returns>INT</returns>
        public static int UpdateProfilePic(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.UpdateProfilePic" + " , SPName : SP_UPDATEPROFILEPIC", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_UPDATEPROFILEPIC", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.UpdateProfilePic" + " , SPName : SP_UPDATEPROFILEPIC  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// Delete files based on seqno(s) status change prefinal step
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet DeleteFilesOrFolder(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.DeleteFilesOrFolder" + " , SPName : SP_DELETEFILESOFALLSEQNOS", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_DELETEFILESOFALLSEQNOS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.DeleteFilesOrFolder" + " , SPName : SP_DELETEFILESOFALLSEQNOS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// Delete files based on seqno(s) status change final step
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet DeleteFilesOrFolderStatusChange(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.DeleteFilesOrFolderStatusChange" + " , SPName : SP_DELETEFILESOFALLSEQNOSFINAL", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_DELETEFILESOFALLSEQNOSFINAL", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.DeleteFilesOrFolderStatusChange" + " , SPName : SP_DELETEFILESOFALLSEQNOSFINAL  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// get data table with full path of each seqno and their corresponding childs path too ,to delete files
        /// </summary>
        /// <returns></returns>
        public static DataSet GetPreDeleteStructure(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetPreDeleteStructure" + " , SPName : SP_GETALLCHILDRENSOFALLSEQNOSPREDELETE", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETALLCHILDRENSOFALLSEQNOSPREDELETE", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetPreDeleteStructure" + " , SPName : SP_GETALLCHILDRENSOFALLSEQNOSPREDELETE  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet InsertFolderStructure(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.InsertFolderStructure" + " , SPName : SP_INSERTFOLDERSTRUCTURE", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
               return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_INSERTFOLDERSTRUCTURE", objList.ToArray());
                
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.InsertFolderStructure" + " , SPName : SP_INSERTFOLDERSTRUCTURE  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        public static int InsertFolderOnly(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.InsertFolderOnly" + " , SPName : SP_INSERTFOLDERONLY", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_INSERTFOLDERONLY", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.InsertFolderOnly" + " , SPName : SP_INSERTFOLDERONLY  " + ex.Message.ToString(), LogMessageType.Error);
                return -100;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetFilePathBySeqNo(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetFilePathBySeqNo" + " , SPName :SP_FINDROOTOFFILE", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_FINDROOTOFFILE", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetFilePathBySeqNo" + " , SPName : SP_FINDROOTOFFILE  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetFilePermissionsBySeqNo(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetFilePathBySeqNo" + " , SPName :SP_CHECKFILEPERMISSIONS", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_CHECKFILEPERMISSIONS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetFilePathBySeqNo" + " , SPName : SP_CHECKFILEPERMISSIONS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetBreadCrumbsData(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetBreadCrumbsData" + " , SPName :SP_GETBREADCRUMBSDATA", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETBREADCRUMBSDATA", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetBreadCrumbsData" + " , SPName : SP_GETBREADCRUMBSDATA  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveUpdateLanguage(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.SaveUpdateLanguage" + " , SPName : SP_SAVEUPDATELANGUAGE", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_SAVEUPDATELANGUAGE", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.SaveUpdateLanguage" + " , SPName : SP_SAVEUPDATELANGUAGE  " + ex.Message.ToString(), LogMessageType.Error);
                return -100;
            }

        }
        /// <summary>
        /// GET EMAIL BASED ON USERNAME OR EMAILID ITSELF
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetUserDetailsFromUserName(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetailsFromUserName" + " , SPName : SP_GETEUSERDETAILSFROMUSERNAME  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETEUSERDETAILSFROMUSERNAME", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetailsFromUserName" + " , SPName : SP_GETEUSERDETAILSFROMUSERNAME  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public static DataSet GetViewNamesByFID(ArrayList objlist)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetViewNamesByFID" + " , SPName : SP_GetViewNamesByFID  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GetViewNamesByFID", objlist.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetViewNamesByFID" + " , SPName : SP_GetViewNamesByFID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string strConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString.ToString();
            }
            set { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetManagementDetails()
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetManagementDetails" + " , SPName : SP_GETMANAGEMENTDETAILS  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETMANAGEMENTDETAILS");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetManagementDetails" + " , SPName : SP_GETMANAGEMENTDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetPasswordPolicyDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetPasswordPolicyDetails" + " , SPName : SP_GETPASSWORDPOLICYDETAILS  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETPASSWORDPOLICYDETAILS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetPasswordPolicyDetails" + " , SPName : SP_GETPASSWORDPOLICYDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// get user details by username to validate login
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static DataSet GetUserDetails(ArrayList objlist)
        {
            try
            {

                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetails" + " , SPName : SP_GETUSERDETAILSBYUSERNAME  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETUSERDETAILSBYUSERNAME", objlist.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetails" + " , SPName : SP_GETUSERDETAILSBYUSERNAME  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllUsers()
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllUsers" + " , SPName : SP_GETUSERDETAILS", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETUSERDETAILS");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetails" + " , SPName : SP_GETUSERDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// get user details by seqno(id)
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static DataSet GetUserDetailsById(ArrayList objlist)
        {
            try
            {

                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetails" + " , SPName : [SP_GETUSERDETAILSBYUSERID]  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "[SP_GETUSERDETAILSBYUSERID]", objlist.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetUserDetails" + " , SPName : [SP_GETUSERDETAILSBYUSERID]  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyWBS"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static DataSet GetViewDetailByFeature(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetViewDetailByFeature" + " , SPName :SP_GETFEATUREDATABYFEATUREID ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETFEATUREDATABYFEATUREID", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetViewDetailByFeature" + " , SPName : [SP_GETFEATUREDATABYFEATUREID]  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        public static DataSet GetDocumentViewDetailByFeature(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetDocumentViewDetailByFeature" + " , SPName :SP_GETDOCUMENTGRID ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETDOCUMENTGRID", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetDocumentViewDetailByFeature" + " , SPName : SP_GETDOCUMENTGRID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertObjectToString(ArrayList obj)
        {
            string str = string.Empty;
            if (obj.Count > 0)
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    str += (i + 1) + ")" + obj[i] + ",";
                }
            }
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllLanguages()
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllLanguages" + " , SPName : [SP_GETALLLANGUAGES]  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "[SP_GETALLLANGUAGES]");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllLanguages" + " , SPName : [SP_GETALLLANGUAGES]  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllRoles()
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllRoles" + " , SPName : [SP_GETALLROLES]  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "[SP_GETALLROLES]");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllRoles" + " , SPName : [SP_GETALLROLES]  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// RETURNING ALL USERGROUPS 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllGroups()
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllGroups" + " , SPName : SP_GETALLGROUPS  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETALLGROUPS");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllGroups" + " , SPName : SP_GETALLGROUPS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        public static DataSet GetAllEventTypes()
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllEventTypes" + " , SPName : SP_GETALLEVENTTYPES  ", LogMessageType.Informational);
                //ErrorLogs("{Info} " + DateTime.Now + " - VerityHelper.GetFeaturesForRole" + " , SPName : sp_GetFeaturesforRole  " + ConvertObjectToString(objlist));
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETALLEVENTTYPES");
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetAllEventTypes" + " , SPName : SP_GETALLEVENTTYPES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyWBS"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static DataSet GetRolesList(int CompanyWBS, int RoleId)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetRolesList" + " , SPName : sp_GetRoles  " + CompanyWBS + RoleId, LogMessageType.Informational);
                ArrayList obj = new ArrayList();
                obj.Add(CompanyWBS); obj.Add(RoleId);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "sp_GetRoles", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetRolesList" + " , SPName : sp_GetRoles  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static int SaveRoles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.SaveRoles" + " , SPName : sp_SaveRoleManagement  " + objList.ToString(), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "sp_SaveRoleManagement", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.SaveRoles" + " , SPName : sp_SaveRoleManagement  " + ex.Message.ToString(), LogMessageType.Error);
                return 0;
            }
        }
        /// <summary>
        /// Save the Users 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveUsers(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveUsers" + " , SPName : SP_SAVEUSERS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVEUSERS", objList.ToArray());
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0 && dset.Tables[0].Columns.Contains("Seqno"))
                    return Convert.ToInt32(dset.Tables[0].Rows[0][0]);
                else
                    return -1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveUsers" + " , SPName : SP_SAVEUSERS  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataSet CheckDuplicateUser(ArrayList obj)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SP_CHECKDUPLICATEUSER" + " - " + ConvertObjectToString(obj), LogMessageType.Informational);
            try
            {

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_CHECKDUPLICATEUSER", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SP_CHECKDUPLICATEUSER" + " - " + ex.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param emailaddress="obj"></param>
        /// <returns></returns>
        public static DataSet CheckDuplicateEmailAddress(ArrayList obj)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SP_CHECKDUPLICATEEMAILADDRESS" + " - " + ConvertObjectToString(obj), LogMessageType.Informational);
            try
            {

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_CHECKDUPLICATEEMAILADDRESS", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SP_CHECKDUPLICATEEMAILADDRESS" + " - " + ex.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int ChangePassword(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.ChangePassword" + " , SPName : SP_SAVEUSERS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_UPDATEPASSWORD", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.ChangePassword" + " , SPName : SP_UPDATEPASSWORD  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public static DataSet CheckStatus(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CheckStatus" + " , SPName : SP_CHECKSTATUS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_CHECKSTATUS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CheckStatus" + " , SPName : SP_CHECKSTATUS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int ChangeUserStatus(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.ChangeUserStatus" + " , SPName : SP_CHANGEUSERSTATUS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_CHANGEUSERSTATUS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.ChangeUserStatus" + " , SPName : SP_CHANGEUSERSTATUS  " + ex.Message.ToString(), LogMessageType.Error);
                return 0;
            }
        }

        public static DataSet GetGroupDetailsByID(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetGroupDetailsByID" + " , SPName : SP_GETGROUPDETAILSBYID  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETGROUPDETAILSBYID", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetGroupDetailsByID" + " , SPName : SP_GETGROUPDETAILSBYID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        public static DataSet SaveUserGroup(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveUserGroup" + " , SPName : SP_SAVEUSERGROUP  " + ConvertObjectToString(objList), LogMessageType.Informational);
               return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVEUSERGROUP", objList.ToArray());
                
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveUserGroup" + " , SPName : SP_SAVEUSERGROUP  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }


        public static DataSet GetGlobalPreferences(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetGlobalPreferences" + " , SPName : SP_GETGLOBALPREFERENCESBYFEATUREID  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETGLOBALPREFERENCESBYFEATUREID", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetGlobalPreferences" + " , SPName : SP_GETGLOBALPREFERENCESBYFEATUREID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        public static int UpdateGlobalPreferences(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.UpdateGlobalPreferences" + " , SPName : SP_UPDATEGLOBALPREFERENCES  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_UPDATEGLOBALPREFERENCES", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.UpdateGlobalPreferences" + " , SPName : SP_UPDATEGLOBALPREFERENCES  " + ex.Message.ToString(), LogMessageType.Error);
                return -100;
            }
        }

        public static DataSet GetEmailSettingsDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetEmailSettingsDetails" + " , SPName : SP_GETEMAILSETTINGSDETAILS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETEMAILSETTINGSDETAILS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetEmailSettingsDetails" + " , SPName : SP_GETEMAILSETTINGSDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        public static int SaveEmailSettingsDeatils(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveEmailSettingsDeatils" + " , SPName : SP_UPDATEEMAILSETTINGSDETAILS  " + ConvertObjectToString(objList), LogMessageType.Informational);

                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_UPDATEEMAILSETTINGSDETAILS", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveEmailSettingsDeatils" + " , SPName : SP_UPDATEEMAILSETTINGSDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return -100;
            }
        }

        public static long InsertNotifStatus(ArrayList ObjList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.InsertNotifStatus" + " , SPName : SP_SAVENOTIFICATIONS  " + ConvertObjectToString(ObjList), LogMessageType.Informational);

                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVENOTIFICATIONS", ObjList.ToArray());
                if (dset != null && dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0 && dset.Tables[0].Columns.Contains("Seqno"))
                    return Convert.ToInt64(dset.Tables[0].Rows[0][0]);
                else
                    return -1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.InsertNotifStatus" + " , SPName : SP_SAVENOTIFICATIONS  " + ex.Message.ToString(), LogMessageType.Error);

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetEmailTemplateByTemplateType(ArrayList objList)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetEmailTemplateByTemplateType" + " , SPName : SP_GETEMAILTEMPLATEBYTEMPLATETYPE  ", LogMessageType.Informational);
            try
            {
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETEMAILTEMPLATEBYTEMPLATETYPE", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetEmailTemplateByTemplateType" + " , SPName : SP_GETEMAILTEMPLATEBYTEMPLATETYPE  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetFeatureDataBySeqno(ArrayList objList)
        {
            AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetFeatureDataBySeqno" + " , SPName : SP_GETFEATUREDATABYSEQNO  ", LogMessageType.Informational);
            try
            {
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETFEATUREDATABYSEQNO", objList.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetFeatureDataBySeqno" + " , SPName : SP_GETFEATUREDATABYSEQNO  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static DataSet GetSmsDetailsByCountry(ArrayList objlist)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetSmsDetailsByCountry" + " , SPName : SP_GETSMSDETAILSBYCOUNTRY", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETSMSDETAILSBYCOUNTRY", objlist.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetSmsDetailsByCountry" + " , SPName : SP_GETSMSDETAILSBYCOUNTRY  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetLanguageDetailsById(ArrayList objList)
        {

            try
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetLanguageDetailsById" + " , SPName : SP_GETANGUAGEDETAILSBYID", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETANGUAGEDETAILSBYID", objList.ToArray());
            }

            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite(DateTime.Now + " - VerityHelper.GetLanguageDetailsById" + " , SPName : SP_GETANGUAGEDETAILSBYID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataSet GetEmailTemplateDetails(ArrayList obj)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetEmailTemplateDetails" + " , SPName : SP_GETEMAILTEMPLATEDETAILS  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETEMAILTEMPLATEDETAILS", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetEmailTemplateDetails" + " , SPName : SP_GETEMAILTEMPLATEDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataSet GetEmailTemplateByID(ArrayList obj)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetEmailTemplateByID" + " , SPName : SP_GETEMAILTEMPLATEBYID  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETEMAILTEMPLATEBYID", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetEmailTemplateByID" + " , SPName : SP_GETEMAILTEMPLATEBYID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataSet GetViewDetailsByFeatureID(ArrayList obj)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetViewDetailsByFeatureID" + " , SPName : SP_GETVIEWINFOBYFEATUREID  ", LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETVIEWINFOBYFEATUREID", obj.ToArray());// new SqlParameter("@Select", Select), new SqlParameter("@where", Where));

            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetViewDetailsByFeatureID" + " , SPName : SP_GETVIEWINFOBYFEATUREID  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int SaveNotificationTemplate(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveNotificationTemplate" + " , SPName : SP_SAVENOTIFICATIONTEMPLATE  " + ConvertObjectToString(objList), LogMessageType.Informational);


                SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_SAVENOTIFICATIONTEMPLATE", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveNotificationTemplate" + " , SPName : SP_SAVENOTIFICATIONTEMPLATE  " + ex.Message.ToString(), LogMessageType.Error);

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_TemplateType"></param>
        /// <param name="_FeatureID"></param>
        /// <param name="Msg"></param>
        /// <param name="DeviceID"></param>
        /// <param name="NotificationFrom"></param>
        /// <param name="_Seqno"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static string InsertNotificationLayer(int _TemplateType, int _FeatureID, string Msg, string DeviceID, string NotificationFrom, int _Seqno, int UserId)
        {
            try
            {
                ArrayList objList = new ArrayList();
                objList.Add(_TemplateType);
                objList.Add(_FeatureID); objList.Add(_Seqno);
                objList.Add(Msg); objList.Add(DeviceID); objList.Add(NotificationFrom);
                objList.Add(UserId);
                VerityHelper.InsertNotificationLayer(objList);
            }
            catch (Exception)
            {

                return "";
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int InsertNotificationLayer(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.InsertNotificationLayer" + " , SPName : SP_INSERTNOTIFICATIONLAYER  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_INSERTNOTIFICATIONLAYER", objList.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.InsertNotificationLayer" + " , SPName : SP_INSERTNOTIFICATIONLAYER  " + ex.Message.ToString(), LogMessageType.Error);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet SaveScheduleDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveScheduleDetails" + " , SPName : SP_SAVESCHEDULERS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVESCHEDULERS", objList.ToArray());
                return dset;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveScheduleDetails" + " , SPName : SP_SAVESCHEDULERS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        public static DataSet GetSchedulers(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetSchedulers" + " , SPName : SP_GETSCHEDULERS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETSCHEDULERS", objList.ToArray());
                return dset;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetSchedulers" + " , SPName : SP_GETSCHEDULERS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllSchedulers(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetAllSchedulers" + " , SPName : SP_GETALLSCHEDULERS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETALLSCHEDULERS", objList.ToArray());
                return dset;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetAllSchedulers" + " , SPName : SP_GETALLSCHEDULERS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetFavorites(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetFavorites" + " , SPName : SP_GETFAVORITES  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETFAVORITES", objList.ToArray());
                return dset;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetFavorites" + " , SPName : SP_GETFAVORITES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet SaveFavorites(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.SaveFavorites" + " , SPName : SP_SAVEFAVORITES  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_SAVEFAVORITES", objList.ToArray());
                return dset;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.SaveFavorites" + " , SPName : SP_SAVEFAVORITES  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetCheckinOutFilesDetails(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetCheckinOutFilesDetails" + " , SPName : SP_CHECKOUTCHECKINFILESDETAILS  " + ConvertObjectToString(objList), LogMessageType.Informational);
                DataSet dset = SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_CHECKOUTCHECKINFILESDETAILS", objList.ToArray());
                return dset;
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetCheckinOutFilesDetails" + " , SPName : SP_CHECKOUTCHECKINFILESDETAILS  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static int MoveFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.MoveFiles" + " , SPName : SP_MOVEFILES  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteNonQuery(VerityHelper.strConnection, "SP_MOVEFILES", objList.ToArray());                
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.MoveFiles" + " , SPName : SP_MOVEFILES  " + ex.Message.ToString(), LogMessageType.Error);
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet CopyFiles(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.CopyFiles" + " , SPName : SP_COPYFILESTONEWDESTINATION  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_COPYFILESTONEWDESTINATION", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.CopyFiles" + " , SPName : SP_COPYFILESTONEWDESTINATION  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataSet GetChildFolderElements(ArrayList objList)
        {
            try
            {
                AsyncLogHelper.AsyncLogWrite("{Info} " + DateTime.Now + " - VerityHelper.GetChildFolderElements" + " , SPName : SP_GETSUBFOLDESOFPARENT  " + ConvertObjectToString(objList), LogMessageType.Informational);
                return SqlHelper.ExecuteDataset(VerityHelper.strConnection, "SP_GETSUBFOLDESOFPARENT", objList.ToArray());
            }
            catch (Exception ex)
            {
                AsyncLogHelper.AsyncLogWrite("{Error} " + DateTime.Now + " - VerityHelper.GetChildFolderElements" + " , SPName : SP_GETSUBFOLDESOFPARENT  " + ex.Message.ToString(), LogMessageType.Error);
                return null;
            }
        }
    }
}

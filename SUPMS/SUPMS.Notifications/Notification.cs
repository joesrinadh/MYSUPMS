using VERITY.DMS.UTILITIES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VERITY.DMS;
using System.Configuration;

namespace SUPMS.Notifications
{
    public partial class Notification : ServiceBase
    {
        #region Declaration Variables
        public class NotifClass
        {
            public long _NotifSeqno = 0;
            public int _FeatureId = 0;
            public int _Type = 0;
            public int _featureSeqno = 0;
            public int _TemplateType = 0;
            public int _CreatedBy = 1;
            public bool NotificationSent = false;
            public string Exception = "";
        }


        public static int FeatureID = 0;
        public static int TemplateType = 0;
        public static int Seqno = 0; public static int CreatedBy = 0;
        public static string Message = "";
        public static long NotifSeqno = 0;
        public static bool IsStarted = false;
        public static string timeToSendReminders = ConfigurationManager.AppSettings["RemindersTime"];
        #endregion
        public Notification()
        {
            InitializeComponent();
        }

        static void Main(string[] args)
        {
            Notification service = new Notification();
            service.ServiceName = "DMS_Service";
            if (Environment.UserInteractive)
            {
                service.OnStart(args);

                Console.WriteLine("Press any key to stop program");
                Console.Read();
                service.OnStop();
            }
            else
            {
                ServiceBase.Run(service);
            }

        }
        private Timer _timer;
        private Timer _timer1;
        private Timer _timer2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Notification.WriteErrorLog("Notification Started On : " + DateTime.Now);
            //SendNotification();
            NotifyTimer.Enabled = true;
            try
            {

                // SendLeaseReminder();
                _timer = new Timer(1 * 60 * 1000); // every 1 minutes
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(SendNotification);
                _timer.Start();



            }
            catch (Exception ex)
            {
                Notification.WriteErrorLog("Notification Exception Occured in Sercie " + ex.Message.ToString() + ", " + DateTime.Now);

            }

            Notification.WriteErrorLog("Notification Ended On : " + DateTime.Now);

        }
      

      



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SendNotification(object sender, System.Timers.ElapsedEventArgs args)//object sender, System.Timers.ElapsedEventArgs args
        // public void SendNotification()
        {
            //  _timer1.Stop();
            try
            {
                //PushToIos("2ef4bbe0586dda6ffcd11e35b05c797bf96a5ae9a9d02dc808502b7e778cca83", "Restaurant updated");

                Notification.WriteErrorLog("SendNotification Method Started On : " + DateTime.Now);
               // ArrayList obj = new ArrayList();
                DataSet dset = VerityHelper.GetListOfPendingNotifications();

                if (dset != null ? dset.Tables.Count>0?dset.Tables[0].Rows.Count > 0:false:false)
                {
                    Notification.WriteErrorLog("SendNotification Method No Of Record[s] Found : " + dset.Tables[0].Rows.Count + " , " + DateTime.Now);
                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            TemplateType = Convert.ToInt32(Notification.ReturnString("TemplateType", dset, i));
                            FeatureID = Convert.ToInt32(Notification.ReturnString("FeatureID", dset, i));
                            Seqno = Convert.ToInt32(Notification.ReturnString("FeatureSeqno", dset, i));
                            CreatedBy = Convert.ToInt32(Notification.ReturnString("InsertedBy", dset, i));
                            Message = Notification.ReturnString("Message", dset, i);
                            DMS_Notification.SendNotification(TemplateType, FeatureID, Message, Seqno, 1);
                            Notification.WriteErrorLog(" Email Sent ,FeatureId:" + FeatureID + " And FeatureSeqno :" + Seqno + " And Template:" + TemplateType + ", : " + DateTime.Now);

                        }
                        catch (Exception ex)
                        {
                            Notification.WriteErrorLog(" Error Occured : " + ex.Message.ToString() + " ,FeatureId:" + FeatureID + " And FeatureSeqno :" + Seqno + " And Template:" + TemplateType + ", : " + DateTime.Now);

                        }
                    }

                }
                else
                {
                    Notification.WriteErrorLog("SendNotification Method No Record[s] Found :  , " + DateTime.Now);
                }
                Notification.WriteErrorLog("SendNotification Method Ended On : " + DateTime.Now);

                #region FILE EXPIRED DATED FILES STATUS CHANGE
                 ChangeExpiredFilesStatus();//change expired files status changes
                #endregion
             


            }
            catch (Exception ex)
            {
                Notification.WriteErrorLog("Exception occured at : " + ex.ToString() + DateTime.Now);

            }
            finally
            {
                _timer1.Start();
            }


        }


        /// <summary>
        /// FILE EXPIRED NOTIFICATIONS SEND
        /// </summary>

        public static void ChangeExpiredFilesStatus()
        {
            try
            {
                Notification.WriteErrorLog("ChangeExpiredFilesStatus method execution started: " + DateTime.Now);
                ArrayList objList = new ArrayList();
                objList.Add(1); objList.Add(1);
                DataSet dset = VerityHelper.ChangeExpiredFilesStatus(objList);
                Notification.WriteErrorLog("No of files expired count : " + (dset!=null?dset.Tables.Count>0?dset.Tables[0].Rows.Count.ToString():"0":"0") + " , " + DateTime.Now);
                if (dset != null ? dset.Tables.Count > 0 ? dset.Tables[0].Rows.Count > 0 : false : false)
                {
                    Notification.WriteErrorLog("SendNotification Method No Of Record[s] expired : " + dset.Tables[0].Rows.Count + " , " + DateTime.Now);
                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        // DMS_Notification caller = new AsyncMethodCaller(this.SendNotif);
                        //mail to file created user     
                        TemplateType = 11;//Convert.ToInt32(Notification.ReturnString("TemplateType", dset, i));
                        FeatureID = 101;//Convert.ToInt32(Notification.ReturnString("FeatureID", dset, i));
                        Seqno = Convert.ToInt32(ReturnString("SEQNO", dset, i, 0));//Convert.ToInt32(Notification.ReturnString("FeatureSeqno", dset, i));
                        CreatedBy = Convert.ToInt32(Notification.ReturnString("CREATEDBY", dset, i,0));//Convert.ToInt32(Notification.ReturnString("InsertedBy", dset, i));
                        Message = "'"+Notification.ReturnString("NAME", dset, i, 0)  + "' File has expired";// Notification.ReturnString("Message", dset, i);
                        DMS_Notification.SendNotification(TemplateType, FeatureID, Message, Seqno, 1);
                        Notification.WriteErrorLog(" Email Sent ,FeatureId:" + FeatureID + " And FeatureSeqno :" + Seqno + " And Template:" + TemplateType + ", : " + DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Notification.WriteErrorLog("Exception occured while Expired Files Status changes : " + ex.ToString() + DateTime.Now);
            }
        }
        /// <summary>  
        /// this function write Message to log file.  
        /// </summary>  
        /// <param name="Message"></param>  
        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static string ReturnString(string col, DataView db, int row)
        {
            if (db.Table.Columns.Contains(col))
                return db[row][col].ToString();
            return "";
        }
        public static string ReturnString(string col, DataSet dset, int row)
        {
            if (dset.Tables[0].Columns.Contains(col))
                return dset.Tables[0].Rows[row][col].ToString();
            return "";
        }
        public static string ReturnString(string col, DataSet dset, int row, int table)
        {
            if (dset.Tables[table].Columns.Contains(col))
                return dset.Tables[table].Rows[row][col].ToString();
            return "";
        }

        protected override void OnStop()
        {
        }
    }
}

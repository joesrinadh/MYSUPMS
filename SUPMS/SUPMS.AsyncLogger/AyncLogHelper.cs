#region Namespace Declaration
using System;
using System.Configuration;
using System.Collections;
using System.Threading.Tasks;

#endregion

namespace SUPMS.Infrastructure.AsyncLogger
{

    /// <summary>
    /// LogHelper class that encapsulates calls to all the Logger classes
    /// </summary>
    public static class AsyncLogHelper
    {
        private static string LogFile = String.Empty;
        private static string IsFileLoggingTurnedOn = String.Empty;
        private static string IsDatabaseLoggingTurnedOn = String.Empty;
        private static string IsSysLoggingTurnedOn = String.Empty;
        private static string IsEventLoggingTurnedOn = String.Empty;

        #region Async Operations

        /// <summary>
        /// BeginLog method
        /// </summary>
        /// <param name="objParameter">objParameter as object</param>
        /// <param name="callBack">callBack as AsyncCallback</param>
        /// <param name="state">state as object</param>
        /// <returns>Refernce of type IAsyncResult</returns>
        private static IAsyncResult BeginLog(object objParameter, AsyncCallback callBack, object state)
        {
            Task<object> taskInstance = new Task<object>((obj) =>
            {
                return DoLog(objParameter);
            }, state);

            if (callBack != null)
            {
                taskInstance.ContinueWith((taskObject) => callBack(taskObject));
            }

            taskInstance.Start();
            return taskInstance;
        }

        /// <summary>
        /// EndLog method
        /// </summary>
        /// <param name="result">result as IAsyncResult</param>
        /// <returns>Instance of object class</returns>
        private static object EndLog(IAsyncResult result)
        {
            object data = null;
            Task<object> taskInstance = result as Task<object>;

            if (taskInstance != null)
            {
                data = taskInstance.Result;
            }

            return data;
        }

        /// <summary>
        /// Callback method
        /// </summary>
        /// <param name="result">result as IAsyncResult</param>
        private static void Callback(IAsyncResult result)
        {
            EndLog(result);
        }

        /// <summary>
        /// DoLog method
        /// </summary>
        /// <param name="obj">obj as object</param>
        /// <returns>Instance of object class</returns>
        private static object DoLog(object obj)
        {
            ArrayList arrayList = (ArrayList)obj;
            string message = arrayList[0].ToString();
            LogMessageType messageType = (LogMessageType)arrayList[1];

            if (arrayList.Count == 2)
            {
                Write(message, LogLevel.INFO, messageType);
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Initializes the dependent properties
        /// </summary>
        private static void Initialize()
        {
            try
            {
                //LogFile = ConfigurationManager.AppSettings["LogFile"].ToString().Trim();
                IsFileLoggingTurnedOn =System.Configuration.ConfigurationManager.AppSettings["IsFileLogTurnedOn"].ToString().Trim();
                //IsDatabaseLoggingTurnedOn = ConfigurationManager.AppSettings["IsDBLogTurnedOn"].ToString().Trim();
                //IsSysLoggingTurnedOn = ConfigurationManager.AppSettings["IsSysLogTurnedOn"].ToString().Trim();
                //IsEventLoggingTurnedOn = ConfigurationManager.AppSettings["IsEventLogTurnedOn"].ToString().Trim();
            }
            catch
            {
                throw;
            }
        }

        public static string LogFileName
        {
            get;
            set;
        }


        public static async void WriteToLog(string message, LogLevel logLevel = LogLevel.DEBUG)
        {
            Initialize();
            //int p = await Task.Factory.StartNew(() => Write(message, logLevel), TaskCreationOptions.LongRunning);
            // This method runs asynchronously.
            int t = await Task.Run(() => Write(message, logLevel));
        }

        /// <summary>
        /// Asynchronous log writer method that logs messages asynchronously
        /// </summary>
        /// <param name="message">Message as string</param>
        /// <param name="messageType">messageType as LogMessageType</param>
        public static void AsyncLogWrite(string message, LogMessageType messageType)
        {
            Initialize();
            ArrayList arrayList = new ArrayList();
            arrayList.Add(message);
            arrayList.Add(messageType);

            object parameter = (object)arrayList;
            BeginLog(arrayList, Callback, null);
        }

        /// <summary>
        /// Writes messages to log based on the log target(s) selected
        /// </summary>
        /// <param name="message">Message as string</param>
        /// <param name="messageType">messageType as LogMessageType</param>
        internal static int Write(string message, LogLevel logLevel, LogMessageType messageType = LogMessageType.Informational)
        {
            string dateString = DateTime.Now.ToUniversalTime().ToString();
            string logMessage = String.Empty;

            if (IsFileLoggingTurnedOn.ToUpper().Equals("ON"))
            {
                try
                {
                    //FileLogger.ErrorLogs(message);
                    if (LogFileName == null)
                        FileLogger.Write(message, logLevel, LogFile);

                    else if (LogFileName != string.Empty)
                        FileLogger.Write(message, logLevel, LogFileName);
                    else
                        FileLogger.Write(message, logLevel, LogFile);
                }
                catch
                {
                    throw new Exception("Error while logging information to file ");
                }
            }


            if (IsDatabaseLoggingTurnedOn.ToUpper().Equals("ON"))
            {
                try
                {
                    DBLogger.Write(message, logLevel);
                }
                catch
                {
                    throw new Exception("Error while logging information to database");
                }
            }

            if (IsEventLoggingTurnedOn.ToUpper().Equals("ON"))
            {
                try
                {
                    EventLogger.Write(message, logLevel);
                }
                catch
                {
                    throw new Exception("Error while logging information to event log");
                }
            }

            if (IsSysLoggingTurnedOn.ToUpper().Equals("ON"))
            {
                try
                {
                    //Code to write log message to the sys log
                }
                catch
                {
                    throw new Exception("Error while logging information to sys log");
                }
            }

            return 1;
        }
    }
}

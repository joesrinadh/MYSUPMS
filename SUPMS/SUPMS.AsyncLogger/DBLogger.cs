#region Namespace Declaration
using System;
using System.Configuration;
using System.Data;
#endregion

/// <summary>
/// Core.Logging namespace that contains methods to log information on the selected log target
/// </summary>
namespace SUPMS.Infrastructure.AsyncLogger
{
    /// <summary>
    /// DBLogger class to log messages to the database
    /// </summary>
    internal static class DBLogger
    {
        static readonly object lockObj = new object();

        private static readonly String source = System.Environment.MachineName;
        private static string LogDBConnectionString =ConfigurationManager.ConnectionStrings["LogDBConnectionString"].ToString();

        /// <summary>
        /// Writes messages to the database log
        /// </summary>
        /// <param name="message">message as string</param>
        /// <param name="logLevel">logLevel as LogLevel</param>
        public static void Write(string message, LogLevel logLevel)
        {
            String severity = logLevel.ToString();
            String insertSQL = "Insert into DB_Log (Text, Source, Severity) Values (" + "'" + message + "'" + ", " + "'" + source + "'" + ", " + "'" + severity + "'" + " )";

            DataHelper.DBProvider = DataProvider.SqlServer;

            try
            {
                lock (lockObj)
                {
                    DataHelper.ExecuteNonQuery(LogDBConnectionString, CommandType.Text, insertSQL);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Writes messages to the database log
        /// </summary>
        /// <param name="message">message to write as text</param>
        /// <param name="logEventPriorityType">logEventPriorityType as MessageType</param>
        public static void Write(string message, MessageType logEventPriorityType)
        {
            String severity = logEventPriorityType.ToString();
            String insertSQL = "Insert into DB_Log (Text, Source, Severity) Values (" + "'" + message + "'" + ", " + "'" + source + "'" + ", " + "'" + severity + "'" + " )";

            DataHelper.DBProvider = DataProvider.SqlServer;

            try
            {
                lock (lockObj)
                {
                    DataHelper.ExecuteNonQuery(LogDBConnectionString, CommandType.Text, insertSQL);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
#region Namespace Declaration

#endregion
/// <summary>
/// Core.Logging namespace that contains methods to log information on the selected log target
/// </summary>

using System;
using System.IO;
namespace SUPMS.Infrastructure.AsyncLogger
{
    /// <summary>
    /// FileLogger logs messages to a text file
    /// </summary>
    internal static class FileLogger
    {
        static readonly object lockObj = new object();
        static readonly int FILE_SIZE = 1024 * 1024;

        private static string GetLogFileName(string fileName)
        {
            string logFile = string.Empty;

            if (fileName.Length > 0 && false == Path.IsPathRooted(fileName))
            {
                logFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName);
            }
            else
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory + "Logs";
                if (!Directory.Exists(appPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(appPath);
                }
                string Filename = "SUPMSLog" + DateTime.Now.ToString("dd-MM-yyyy"); //dateAndTime.ToString("dd/MM/yyyy")
                logFile = appPath + "\\" + Filename + ".txt";
            }
            return logFile;
        }

        private static Boolean ShouldRolloverFile(string filename)
        {
            var FileSize = Convert.ToDecimal((new System.IO.FileInfo(filename)).Length);

            if (FileSize > FILE_SIZE)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        public static void ErrorLogs(string Message)
        {
            bool result = false;
            try
            {

                //string appPath = HttpContext.Current.Server.MapPath("") + "\\Logs\\";// Environment.ExpandEnvironmentVariables("%AppData%\\FOTLog");
                string appPath = AppDomain.CurrentDomain.BaseDirectory + "Logs";
                if (System.Configuration.ConfigurationManager.AppSettings["LogErrorType"].ToString() == "File")
                {
                    if (!Directory.Exists(appPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(appPath);
                    }
                    string Filename = "SUPMSLog" + DateTime.Now.ToString("dd-MM-yyyy"); //dateAndTime.ToString("dd/MM/yyyy")
                    string fullpath = appPath + "\\" + Filename + ".txt";

                    if (!File.Exists(fullpath))
                    {
                        // File.Create(fullpath);
                        System.IO.FileStream f = System.IO.File.Create(fullpath);
                        f.Close();
                        TextWriter tw = new StreamWriter(fullpath);
                        tw.WriteLine(Message);
                        tw.Close();
                    }
                    else if (File.Exists(fullpath))
                    {
                        using (StreamWriter w = File.AppendText(fullpath))
                        {
                            w.WriteLine(Message);
                            w.Close();
                        }
                    }
                }
            }
            catch
            {
            }

        }
        /// <summary>
        /// Writes messages to a text file
        /// </summary>
        /// <param name="message">message as text</param>
        /// <param name="logLevel">logLevel as LogLevel</param>
        /// <param name="fileName">fileName as string</param>
        public static void Write(string message, LogLevel logLevel, string fileName)
        {
            string logText = String.Format("{0} --- {1} --- {2}", logLevel, DateTime.Now, message);
            string logFile = GetLogFileName(fileName);

            //if (ShouldRolloverFile(logFile))
            //    logFile = logFile + "_1";
            try
            {
                lock (lockObj)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFile, true))
                    {
                        file.WriteLine(logText);
                        file.Close();
                    }
                }
            }
            catch
            {

            }
        }
    }
}
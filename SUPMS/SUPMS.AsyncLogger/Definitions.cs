#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace SUPMS.Infrastructure.AsyncLogger
{
    /// <summary>
    /// Log message enumeration that is used to indicate the log message type.
    /// </summary>
    public enum LogMessageType
    {
        /// <value>Informational message</value>
        Informational = 1,
        /// <value>Warning message</value>
        Warning = 2,
        /// <value>Error message</value>
        Error = 3,
        /// <value>Critical message</value>
        Critical = 4
    }

    public enum LogLevel
    {
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        ERROR = 4,
        SEVERE = 5
    }

    /// <summary>
    /// LogEventPriorityType enumeration
    /// </summary>
    public enum LogEventPriorityType
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Informational = 4
    }

    /// <summary>
    /// DataProvider enumeration
    /// </summary>
    public enum DataProvider
    {
        Oracle, SqlServer, OleDb, Odbc
    }

    /// <summary>
    /// Message Type enumerator
    /// </summary>
    public enum MessageType
    {
        /// <value>Informational message</value>
        Informational = 1,
        /// <value>Warning message</value>
        Warning = 2,
        /// <value>Error message</value>
        Error = 3,
        /// <value>Critical message</value>
        Critical = 4
    }

    /// <summary>
    /// LogTarget enumeration
    /// </summary>
    public enum LogTarget
    {
        /// <value>Event Logger</value>
        Event = 1,
        /// <value>File Logger</value>
        File = 2,
        /// <value>DB Logger</value>
        Database = 3,
        /// <value>Sys Logger</value>
        SysLog = 4
    }
}

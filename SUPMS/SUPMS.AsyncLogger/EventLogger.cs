#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
#endregion

/// <summary>
/// Core.Logging namespace that contains methods to log information on the selected log target
/// </summary>
namespace SUPMS.Infrastructure.AsyncLogger
{
    /// <summary>
    /// EventLogger logs messages to the EventLog
    /// </summary>
    internal static class EventLogger
    {
        static readonly object lockObj = new object();

        private static readonly String sourceName = "App_Log";
        private static readonly String logName = "App_Log";

        /// <summary>
        /// Static constructor
        /// </summary>
        static EventLogger()
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, logName);
            }
        }

        /// <summary>
        /// Writes messages to the event log
        /// </summary>
        /// <param name="message">message as string</param>
        /// <param name="logLevel">logLevel as LogLevel</param>
        public static void Write(String message, LogLevel logLevel)
        {
            EventLogEntryType eventType = ConvertLogLevelToEventLogEntryType(logLevel);
            try
            {
                lock (lockObj)
                {
                    EventLog.WriteEntry(sourceName, message, eventType);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Writes messages to the event log
        /// </summary>
        /// <param name="message">message as string</param>
        /// <param name="messageType">messageType as MessageType</param>
        public static void Write(String message, MessageType messageType)
        {
            EventLogEntryType eventType = ConvertMessageTypeToEventLogEntryType(messageType);

            try
            {
                lock (lockObj)
                {
                    EventLog.WriteEntry(sourceName, message, eventType);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts LogLevel to EventLogEntryType
        /// </summary>
        /// <param name="logLevel">logLevel as LogLevel</param>
        /// <returns>EventLogEntryType</returns>
        private static EventLogEntryType ConvertLogLevelToEventLogEntryType(LogLevel logLevel)
        {
            EventLogEntryType eventLogEntryType = EventLogEntryType.Error;
            switch (logLevel)
            {
                case LogLevel.ERROR:
                case LogLevel.SEVERE:
                    eventLogEntryType = EventLogEntryType.Error;
                    break;
                case LogLevel.INFO:
                    eventLogEntryType = EventLogEntryType.Information;
                    break;
                case LogLevel.WARN:
                    eventLogEntryType = EventLogEntryType.Warning;
                    break;
                default:
                    eventLogEntryType = EventLogEntryType.SuccessAudit;
                    break;
            }
            return eventLogEntryType;
        }

        /// <summary>
        /// Converts MessageType to EventLogEntryType
        /// </summary>
        /// <param name="messageType">messageType as MessageType</param>
        /// <returns>EventLogEntryType</returns>
        private static EventLogEntryType ConvertMessageTypeToEventLogEntryType(MessageType messageType)
        {
            EventLogEntryType eventLogEntryType = EventLogEntryType.Error;
            switch (messageType)
            {
                case MessageType.Error:
                case MessageType.Critical:
                    eventLogEntryType = EventLogEntryType.Error;
                    break;
                case MessageType.Informational:
                    eventLogEntryType = EventLogEntryType.Information;
                    break;
                case MessageType.Warning:
                    eventLogEntryType = EventLogEntryType.Warning;
                    break;
                default:
                    eventLogEntryType = EventLogEntryType.SuccessAudit;
                    break;
            }
            return eventLogEntryType;
        }
    }
}

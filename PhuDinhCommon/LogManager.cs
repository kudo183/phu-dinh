using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using log4net;

namespace PhuDinhCommon
{
    public enum event_type
    {
        /*
        etFIXInbound   = 0x1,
        etFIXOutbound  = 0x1 << 1,
        etFIXSession   = 0x1 << 2,
        etFrontEnd     = 0x1 << 3,
        etRoboTrader   = 0x1 << 4,
        etPositions    = 0x1 << 5,
        etMarketData   = 0x1 << 6,
        etOrders       = 0x1 << 7,
        etTrades       = 0x1 << 8,
        etReports      = 0x1 << 9,
        etRSSFeed      = 0x1 << 10,
        etRiskManager  = 0x1 << 11,
        etFIX          = etFIXInbound | etFIXOutbound | etFIXSession,
        etDefault      = etMarketData | etOrders | etTrades | etFrontEnd | etRoboTrader | etPositions | etReports | etRSSFeed,
        etAll          = etDefault | etFIX,
        etUndefined
        */

        et_Administrative = 0x1,
        et_Internal = 0x1 << 1,
        et_Trades = 0x1 << 2,
        et_Orders = 0x1 << 3,
        et_Market_Data_Control = 0x1 << 4,
        et_Inside_Market = 0x1 << 5,
        et_Market_Depth = 0x1 << 6,
        et_Positions = 0x1 << 7,
        et_Accounts = 0x1 << 8,
        et_RSS_Feed = 0x1 << 9,
        et_RoboTrader = 0x1 << 10,
        et_RiskManager = 0x1 << 11,
        et_MarginCall = 0x1 << 12,
        et_AutoX = 0x1 << 13,
        et_Alert = 0x1 << 14,
        et_Performance = 0x1 << 15,
        et_RTDService = 0x1 << 16,
        et_Undefined = 0x1 << 31
    }

    [Flags]
    public enum severity_type
    {
        st_error = 0x1,
        st_warning = 0x1 << 1,
        st_info = 0x1 << 2,
        st_debug = 0x1 << 3,
        st_extra = 0x1 << 4,
        st_default = st_error | st_warning | st_info,
        st_verbose = st_default | st_debug,
        st_all = st_verbose | st_extra,
        st_undefined
    }

    public interface ILogDataHelper
    {
        LogData GetLogDataFromAccount(string fixName, string currency);
        LogData GetLogDataFromAccount(string id);
    }

    public class LogDataUtils
    {
        public ILogDataHelper Helper { get; set; }
    }

    public class LogData
    {
        public string Account { get; set; }
    }

    // LogOut params
    [Serializable]
    public class LogParams : EventArgs
    {
        private DateTime eventDate = DateTime.Now;

        public LogParams()
        {
        }

        public LogParams(event_type evt, severity_type st, string message, string src)
            : this(evt, st, message, src, null, null)
        {
        }

        public LogParams(event_type evt, severity_type st, string message, string src, LogData data)
            : this(evt, st, message, src, null, data)
        {
        }

        public LogParams(event_type evt, severity_type st, string message, DateTime date, LogData data)
            : this(evt, st, message, null, null, data)
        {
            eventDate = date;
        }

        public LogParams(event_type evt, severity_type st, string message, string src, Exception ex, LogData data)
        {
            EventType = evt;
            SeverityType = st;
            Message = message;
            SourceMessage = src;
            Exception = ex;
            logData = data;
        }

        [XmlIgnore]
        public Exception Exception { get; set; }

        public event_type EventType { get; set; }

        [XmlIgnore]
        public string EventString
        {
            get { return EventType.ToStr(); }
        }

        public severity_type SeverityType { get; set; }

        [XmlIgnore]
        public string SeverityString
        {
            get { return SeverityType.ToStr(); }
        }

        public string Message { get; set; }

        public string SourceMessage { get; set; }

        public DateTime EventDate
        {
            get { return eventDate; }
            set { eventDate = value; }
        }

        [XmlIgnore]
        public string EventDateString
        {
            get { return eventDate.ToString("MM/dd/yyyy HH:mm:ss.ffff"); }
        }

        private readonly LogData logData;

        [XmlIgnore]
        public string AccountString
        {
            get { return (logData != null) ? logData.Account : string.Empty; }
        }
    }

    public interface IRestoreFromDiskHandler
    {
        bool Handle(LogParams log);
    }

    internal class EmptyHandler : IRestoreFromDiskHandler
    {
        public bool Handle(LogParams log)
        {
            return false;
        }
    }

    // LogManager
    public class LogManager
    {
        // Create a logger for use in this class
        private static readonly ILog Logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly bool IsErrorLoggingEnabled = Logger.IsErrorEnabled;
        private static readonly bool IsDebugLoggingEnabled = Logger.IsDebugEnabled;
        private static readonly bool IsInfoLoggingEnabled = Logger.IsInfoEnabled;
        private static readonly bool IsWarnLoggingEnabled = Logger.IsWarnEnabled;

        #region Event

        public static event EventHandler<LogParams> LogMessage
        {
            add
            {
                lock (logMessageEventLock)
                    logMessageEvent += value;
            }
            remove
            {
                lock (logMessageEventLock)
                    logMessageEvent -= value;
            }
        }
        private static EventHandler<LogParams> logMessageEvent;
        private static readonly object logMessageEventLock = new object();

        private static void OnLogMessage(LogParams e)
        {
            EventHandler<LogParams> handler = logMessageEvent;

            if (handler != null)
            {
                try
                {
                    handler(null, e);
                }
                catch (Exception ex)
                {
                    if (IsErrorLoggingEnabled)
                        Logger.Error("Logger failed to invoke LogMessage event.", ex);
                }
            }
        }

        #endregion

        private static readonly List<FileInfo> LogFiles = new List<FileInfo>();
        private const string LogFileExt = "log";
        private static DirectoryInfo LogDir
        {
            get
            {
                if (logDir == null)
                {
                    string logPath = log4net.GlobalContext.Properties["LogPath"] as string;
                    logDir = !string.IsNullOrEmpty(logPath) ? new DirectoryInfo(logPath) : new DirectoryInfo("Log");
                }
                return logDir;
            }
        } private static DirectoryInfo logDir;

        private const string Separator = " :: ";

        static public IRestoreFromDiskHandler RestoreFromDiskHandler
        {
            get { return _restoreFromDiskHandler; }
            set { _restoreFromDiskHandler = value; }
        } static private IRestoreFromDiskHandler _restoreFromDiskHandler = new EmptyHandler();

        public static List<LogParams> RestoreFromDisk(DateTime before, int days)
        {
            List<LogParams> list = new List<LogParams>();
            if (days <= 0)
                return list;

            if (LogFiles.Count > 0)
                LogFiles.Clear();
            DateTime boundaryDate = DateTime.Now.AddDays(-days);
            foreach (var file in LogDir.GetFiles("*." + LogFileExt))
            {
                if (file.LastWriteTime > boundaryDate)
                {
                    LogFiles.Add(file);
                }
            }
            LogFiles.Sort((x, y) => x.LastWriteTime.CompareTo(y.LastWriteTime));
            LogFiles.RemoveAt(LogFiles.Count - 1);


            string data = null;
            foreach (FileInfo logFile in LogFiles)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(logFile.OpenRead(), Encoding.ASCII))
                    {
                        LogParams tmp = null;
                        while (reader.Peek() >= 0)
                        {
                            data = reader.ReadLine();
                            int pos = 0, curPos = 0;
                            severity_type st;
                            event_type et;
                            DateTime dateTime;
                            string account = null;

                            pos = data.IndexOf(Separator, curPos);
                            if (pos < curPos + 1 ||
                                !DateTime.TryParseExact(data.Substring(curPos, pos - curPos), "yyyy-MM-dd HH:mm:ss,fff",
                                                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) ||
                                dateTime <= boundaryDate)
                            {
                                continue;
                            }
                            if (dateTime >= before)
                            {
                                return list;
                            }
                            curPos = pos + Separator.Length;

                            pos = data.IndexOf(Separator, curPos);
                            if (pos < curPos + 1)
                            {
                                continue;
                            }
                            curPos = pos + Separator.Length;

                            pos = data.IndexOf(Separator, curPos);
                            if (pos < curPos + 1)
                            {
                                continue;
                            }
                            switch (data.Substring(curPos, pos - curPos))
                            {
                                case "ERROR":
                                    st = severity_type.st_error;
                                    if (!Singleton<Settings>.Instance.Logging.ErrorCustomSettings.LogToGrid)
                                        continue;
                                    break;
                                case "WARN ":
                                    st = severity_type.st_warning;
                                    if (!Singleton<Settings>.Instance.Logging.WarningCustomSettings.LogToGrid)
                                        continue;
                                    break;
                                case "INFO ":
                                    st = severity_type.st_info;
                                    if (!Singleton<Settings>.Instance.Logging.InfoCustomSettings.LogToGrid)
                                        continue;
                                    break;
                                case "DEBUG":
                                    st = severity_type.st_debug;
                                    if (!Singleton<Settings>.Instance.Logging.DebugCustomSettings.LogToGrid)
                                        continue;
                                    break;
                                default:
                                    continue;
                            }
                            curPos = pos + Separator.Length;

                            pos = data.IndexOf(Separator, curPos);
                            if (pos < curPos + 1)
                            {
                                continue;
                            }
                            et = Utils.EventTypeFromShortStr(data.Substring(curPos, pos - curPos));
                            curPos = pos + Separator.Length;

                            pos = data.IndexOf(Separator, curPos);
                            if (pos >= curPos)
                            {
                                account = data.Substring(curPos, pos - curPos);
                                curPos = pos + Separator.Length;
                            }

                            tmp = new LogParams(et, st, data.Substring(curPos), dateTime, new LogData { Account = account });
                            if (!_restoreFromDiskHandler.Handle(tmp))
                                list.Add(tmp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log(event_type.et_Internal, severity_type.st_error,
                        String.Format("LogManager.Restore: {0}", ex));
                }
            }
            return list;
        }

        public static void RemoveFromDisk(int days)
        {
            if (days < 0)
                return;

            try
            {
                if (days == 0)
                    Logger.Logger.Repository.Shutdown();

                DateTime boundaryDate = DateTime.Now.AddDays(-days);
                FileInfo[] files = LogDir.GetFiles("*." + LogFileExt);
                for (int i = 0; i < files.Length; ++i)
                {
                    if (days == 0 || files[i].LastWriteTime <= boundaryDate)
                        files[i].Delete();
                }
            }
            catch (Exception ex)
            {
                Log(event_type.et_Internal, severity_type.st_error,
                    String.Format("LogManager.RemoveFromDisk: {0}", ex));
            }

        }

        private static void Show(event_type et, severity_type st, string message, string source, Exception ex, LogData data)
        {
            Show(et, st, message, source, ex, data, false);
        }

        private static void Show(event_type et, severity_type st, string message, string source, Exception ex, LogData data, bool toFileOnly)
        {
            LogParams logParams = new LogParams(et, st, message, source, data);

            string msg = logParams.EventType.ToShortStr() + " :: " + (data != null ? data.Account : string.Empty) + " :: " + logParams.Message;

            if (!string.IsNullOrEmpty(logParams.SourceMessage))
                msg += "[" + logParams.SourceMessage + "]";

            if (((int)st & (int)severity_type.st_error) != 0)
            {
                if (IsErrorLoggingEnabled && Singleton<Settings>.Instance.Logging.ErrorCustomSettings.LogToFile)
                    Logger.Error(msg, ex);

                if (!toFileOnly && Singleton<Settings>.Instance.Logging.ErrorCustomSettings.LogToGrid)
                    OnLogMessage(logParams);
            }
            else if (((int)st & (int)severity_type.st_warning) != 0)
            {
                if (IsWarnLoggingEnabled && Singleton<Settings>.Instance.Logging.WarningCustomSettings.LogToFile)
                    Logger.Warn(msg, ex);

                if (!toFileOnly && Singleton<Settings>.Instance.Logging.WarningCustomSettings.LogToGrid)
                    OnLogMessage(logParams);
            }
            else if (((int)st & (int)severity_type.st_info) != 0)
            {
                if (IsInfoLoggingEnabled && Singleton<Settings>.Instance.Logging.InfoCustomSettings.LogToFile)
                    Logger.Info(msg, ex);

                if (!toFileOnly && Singleton<Settings>.Instance.Logging.InfoCustomSettings.LogToGrid)
                    OnLogMessage(logParams);
            }
            else if (((int)st & (int)severity_type.st_debug) != 0)
            {
                if (IsDebugLoggingEnabled && Singleton<Settings>.Instance.Logging.DebugCustomSettings.LogToFile)
                    Logger.Debug(msg, ex);

                if (!toFileOnly && Singleton<Settings>.Instance.Logging.DebugCustomSettings.LogToGrid)
                    OnLogMessage(logParams);
            }
            else if (((int)st & (int)severity_type.st_extra) != 0)
            {
                if (IsDebugLoggingEnabled && Singleton<Settings>.Instance.Logging.ExtraCustomSettings.LogToFile)
                    Logger.Debug(msg, ex);

                if (!toFileOnly && Singleton<Settings>.Instance.Logging.ExtraCustomSettings.LogToGrid)
                    OnLogMessage(logParams);
            }
        }

        static public void Log(event_type et, severity_type st, string message)
        {
            Show(et, st, message, null, null, null);
        }

        static public void Log(event_type et, severity_type st, string message, Exception ex)
        {
            Show(et, st, message, null, ex, null);
        }

        static public void Log(event_type et, severity_type st, string message, string source)
        {
            Show(et, st, message, source, null, null);
        }

        static public void Log(event_type et, severity_type st, string message, string source, Exception ex)
        {
            Show(et, st, message, source, ex, null);
        }

        static public void Log(event_type et, severity_type st, string message, LogData data)
        {
            Show(et, st, message, null, null, data);
        }

        static public void Log(event_type et, severity_type st, string message, Exception ex, LogData data)
        {
            Show(et, st, message, null, ex, data);
        }

        static public void Log(event_type et, severity_type st, string message, string source, LogData data)
        {
            Show(et, st, message, source, null, data);
        }

        static public void Log(event_type et, severity_type st, string message, string source, Exception ex, LogData data)
        {
            Show(et, st, message, source, ex, data);
        }

        static public void LogToFile(event_type et, severity_type st, string message, LogData data)
        {
            Show(et, st, message, null, null, data, true);
        }
    }
}

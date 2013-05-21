using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PhuDinhCommon
{
    [Serializable]
    public sealed class Settings
    {
        /// <summary>
        /// Gets or sets application settings for logging.
        /// </summary>
        /// <value>Application settings for logging.</value>
        /// <remarks>
        /// When caller try set settings to null. Settings reverted to default values.
        /// </remarks>
        [XmlElement("logging")]
        public LoggingSettings Logging
        {
            get
            {
                return logging ?? (logging = new LoggingSettings());
            }
            set
            {
                if (value == null)
                    value = new LoggingSettings();
                logging = value;
            }
        }

        private LoggingSettings logging;

        /// <summary>
        /// Configuration settings for logging
        /// </summary>
        [Serializable]
        public class LoggingSettings
        {
            private event_type currentEventType = event_type.et_Undefined;
            private severity_type currentSeverityType = severity_type.st_undefined;
            //private string logFile = "log.log";
            //private string logDirectory = "./log";

            private AdvancedLoggingDetails errorLoggingAdvancedSettings =
                new AdvancedLoggingDetails
                {
                    SeverityType = severity_type.st_error,
                    LogToFile = true,
                    LogToGrid = true
                };

            private AdvancedLoggingDetails warningLoggingAdvancedSettings =
                new AdvancedLoggingDetails
                {
                    SeverityType = severity_type.st_warning,
                    LogToFile = true,
                    LogToGrid = true
                };

            private AdvancedLoggingDetails infoLoggingAdvancedSettings =
                new AdvancedLoggingDetails
                {
                    SeverityType = severity_type.st_info,
                    LogToFile = true,
                    LogToGrid = true
                };

            private AdvancedLoggingDetails debugLoggingAdvancedSettings =
                new AdvancedLoggingDetails
                {
                    SeverityType = severity_type.st_debug,
                    LogToFile = true,
                    LogToGrid = false
                };

            private AdvancedLoggingDetails extraLoggingAdvancedSettings =
                new AdvancedLoggingDetails
                {
                    SeverityType = severity_type.st_extra,
                    LogToFile = true,
                    LogToGrid = false
                };

            public event_type CurrentEventType
            {
                get { return currentEventType; }
                set { currentEventType = value; }
            }

            /// <summary>
            /// Gets or sets filter for filtering severity types when logging.
            /// </summary>
            public severity_type CurrentSeverityType
            {
                get { return currentSeverityType; }
                set { currentSeverityType = value; }
            }

            ///// <summary>
            ///// Gets or sets directory for storing log files.
            ///// </summary>
            //public string LogDirectory
            //{
            //    get { return logDirectory; }
            //    set { logDirectory = value; }
            //}

            ///// <summary>
            ///// Gets or sets name of log file.
            ///// </summary>
            //public string LogFile
            //{
            //    get { return logFile; }
            //    set { logFile = value; }
            //}

            public int LoadDays
            {
                get { return loadDays; }
                set
                {
                    int old = loadDays;
                    loadDays = value;
                }
            }
            private int loadDays;

            public int KeepOnDiskDays
            {
                get { return keepOnDiskDays; }
                set { keepOnDiskDays = value; }
            }
            private int keepOnDiskDays = 7;


            public AdvancedLoggingDetails ErrorCustomSettings
            {
                get { return errorLoggingAdvancedSettings; }
                set { errorLoggingAdvancedSettings = value; }
            }

            public AdvancedLoggingDetails WarningCustomSettings
            {
                get { return warningLoggingAdvancedSettings; }
                set { warningLoggingAdvancedSettings = value; }
            }

            public AdvancedLoggingDetails InfoCustomSettings
            {
                get { return infoLoggingAdvancedSettings; }
                set { infoLoggingAdvancedSettings = value; }
            }

            public AdvancedLoggingDetails DebugCustomSettings
            {
                get { return debugLoggingAdvancedSettings; }
                set { debugLoggingAdvancedSettings = value; }
            }

            public AdvancedLoggingDetails ExtraCustomSettings
            {
                get { return extraLoggingAdvancedSettings; }
                set { extraLoggingAdvancedSettings = value; }
            }

            public bool RiskLimitAlerts
            {
                get { return riskLimitAlerts; }
                set { riskLimitAlerts = value; }
            }
            private bool riskLimitAlerts = true;
        }
    }
}

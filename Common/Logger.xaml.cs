using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Common
{
    /// <summary>
    /// Interaction logic for Logger.xaml
    /// </summary>
    public partial class LoggerViewer : UserControl
    {
        public LoggerViewer()
        {
            InitializeComponent();
            listBox.DataContext = Logger.LogData;
        }
    }

    public static class Logger
    {
        private const string DATETIME_FORMAT = "dd/MM/yy hh:mm:ss.fff";
        public static ObservableCollection<string> LogData
        {
            get { return _logData; }
        }
        private static readonly ObservableCollection<string> _logData = new ObservableCollection<string>();

        [Conditional("DEBUG")]
        public static void LogDebug(string msg, [CallerMemberName] string callerName = null)
        {
            _logData.Add(string.Format("{0} DEBUG {1} {2}", System.DateTime.Now.ToString(DATETIME_FORMAT), callerName, msg));
        }

        public static void LogInfo(string msg, [CallerMemberName] string callerName = null)
        {
            _logData.Add(string.Format("{0} INFO {1} {2}", System.DateTime.Now.ToString(DATETIME_FORMAT), callerName, msg));
        }

        public static void LogError(string msg, [CallerMemberName] string callerName = null)
        {
            _logData.Add(string.Format("{0} ERROR {1} {2}", System.DateTime.Now.ToString(DATETIME_FORMAT), callerName, msg));
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            var cb = new CommandBinding(ApplicationCommands.Copy, CopyCmdExecuted, CopyCmdCanExecute);

            this.listBox.CommandBindings.Add(cb);
        }

        void CopyCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            var lb = e.OriginalSource as ListBox;
            string copyContent = String.Empty;
            // The SelectedItems could be ListBoxItem instances or data bound objects depending on how you populate the ListBox.  
            foreach (var item in lb.SelectedItems)
            {
                copyContent += item;
                // Add a NewLine for carriage return  
                copyContent += Environment.NewLine;
            }
            Clipboard.SetText(copyContent);
        }

        void CopyCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var lb = e.OriginalSource as ListBox;
            // CanExecute only if there is one or more selected Item.  
            if (lb.SelectedItems.Count > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
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

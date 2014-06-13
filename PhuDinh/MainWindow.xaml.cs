using System.Configuration;
using PhuDinhCommon;
using System.Globalization;
using System.Threading;
using System.Windows;
using LogManager = PhuDinhCommon.LogManager;

namespace PhuDinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_info, new string('*', 20));
            LogManager.Log(event_type.et_Internal, severity_type.st_info, "Start main window");

            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");

#if DEBUG
            Title = string.Format("{0} - {1} - {2}", "Debug", ConfigurationManager.AppSettings["DataSource"], ConfigurationManager.AppSettings["InitialCatalog"]);
#else
            Title = string.Format("{0} - {1} - {2}", "Chuong trinh quan ly", ConfigurationManager.AppSettings["DataSource"], ConfigurationManager.AppSettings["InitialCatalog"]);
#endif

            PhuDinhData.TonKhoManager.UpdateTonKho();
        }
    }
}

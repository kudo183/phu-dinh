using System;
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
            log4net.GlobalContext.Properties["name"] = DateTime.Now.ToString("yyyy_MM_dd-hh_mm_ss") + ".log";

            LogManager.Log(event_type.et_Internal, severity_type.st_info, new string('*', 20));
            LogManager.Log(event_type.et_Internal, severity_type.st_info, "Start main window");

            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");

#if DEBUG
            Title = string.Format("{0}", "Debug**********");
#else
            Title = string.Format("{0}", "Chuong trinh quan ly");
            PhuDinhData.TonKhoManager.UpdateTonKho();
#endif
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using PhuDinhCommon;
using System.Globalization;
using System.Threading;
using System.Windows;
using PhuDinhEFClientContext;
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

            Common.ServiceLocator.Instance.Initialize(new Dictionary<Type, Type>()
            {
                { typeof(IClientContextManager<>), typeof(EFContextManager<>) },
                { typeof(IClientContext), typeof(EFContext) }
            });

#if DEBUG
            //ConfigurationManager.AppSettings["DataSource"] = ".";
            ConfigurationManager.AppSettings["InitialCatalog"] = "PhuDinh_test";
#endif

            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");

#if DEBUG
            Title = string.Format("***Debug*** DB:{0} Server:{1}"
                , ConfigurationManager.AppSettings["InitialCatalog"]
                , ConfigurationManager.AppSettings["DataSource"]);
#else
            Title = string.Format("{0}", "Chuong trinh quan ly");
            PhuDinhData.TonKhoManager.UpdateTonKho();
#endif
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using PhuDinhCommon;
using PhuDinhEFClientContext;

namespace PhuDinhConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.ServiceLocator.Instance.Initialize(new Dictionary<Type, Type>()
            {
                { typeof(IClientContextManager<>), typeof(EFContextManager<>) },
                { typeof(IClientContext), typeof(EFContext) }
            });

            log4net.GlobalContext.Properties["name"] = DateTime.Now.ToString("yyyy_MM_dd-hh_mm_ss") + ".log";
#if DEBUG
            //ConfigurationManager.AppSettings["DataSource"] = ".";
            ConfigurationManager.AppSettings["InitialCatalog"] = "PhuDinh_test";
#endif
            PhuDinhData.TonKhoManager.UpdateTonKho();
            PhuDinhData.CongNoManager.UpdateCongNo();
        }
    }
}

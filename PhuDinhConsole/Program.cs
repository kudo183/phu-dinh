using System;
using System.Collections.Generic;
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
            PhuDinhData.TonKhoManager.UpdateTonKho();
        }
    }
}

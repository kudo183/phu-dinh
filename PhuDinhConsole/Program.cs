using System;

namespace PhuDinhConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.GlobalContext.Properties["name"] = DateTime.Now.ToString("yyyy_MM_dd-hh_mm_ss") + ".log";
            PhuDinhData.TonKhoManager.UpdateTonKho();
        }
    }
}

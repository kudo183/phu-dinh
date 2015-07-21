using System;
using System.Data.Services.Client;
using Common;

namespace PhuDinhODataClientContext
{
    internal static class ContextFactory
    {
        public static DataServiceContextEx CreateContext()
        {
            var context = new DataServiceContextEx(new Uri("http://luoithepvinhphat.com:8888/odata")
                , PhuDinhDataEntity.PhuDinhDataEntityEDM.GetPhuDinhDataEntityEDM());

            context.ReceivingResponse += context_ReceivingResponse;

            return context;
        }

        static void context_ReceivingResponse(object sender, ReceivingResponseEventArgs e)
        {
            var r = e.ResponseMessage as HttpWebResponseMessage;

            Logger.LogDebug(r.Response.ResponseUri + "   " + r.Response.ContentLength.ToString());
        }
    }
}

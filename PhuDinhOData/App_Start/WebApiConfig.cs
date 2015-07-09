using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using PhuDinhDataEntity;
using log4net;

namespace PhuDinhOData
{
    public static class WebApiConfig
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new MethodOverrideHandler());

            config.Routes.MapODataServiceRoute("odata", "odata", PhuDinhDataEntityEDM.GetPhuDinhDataEntityEDM());

            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.AddODataQueryFilter();
        }

        public class MethodOverrideHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                string requestContent = request.Content.ReadAsStringAsync().Result;

                var result = base.SendAsync(request, cancellationToken);

                string responseContent = "";
                if (result.Result.Content != null)
                    responseContent = result.Result.Content.ReadAsStringAsync().Result;

                _logger.Info(string.Format("Request: ({0})\n{1}", requestContent.Length, requestContent));
                _logger.Info(string.Format("Response: ({0})\n{1}", responseContent.Length, responseContent));

                return result;
            }
        }
    }
}
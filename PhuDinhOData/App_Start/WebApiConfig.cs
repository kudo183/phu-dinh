using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using PhuDinhDataEntity;

namespace PhuDinhOData
{
    public static class WebApiConfig
    {
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
                var result = base.SendAsync(request, cancellationToken);
                return result;
            }
        }
    }
}
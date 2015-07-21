using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using PhuDinhCommon;
using PhuDinhEFClientContext;

namespace PhuDinhWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Error", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            Common.ServiceLocator.Instance.Initialize(new Dictionary<Type, Type>()
            {
                { typeof(IClientContextManager<>), typeof(EFContextManager<>) },
                { typeof(IClientContext), typeof(EFContext) }
            });

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
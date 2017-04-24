using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Routing;
using LegacyStandalone.Web.MyConfigurations;
using LegacyStandalone.Web.MyConfigurations.Exceptions;

namespace LegacyStandalone.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new MyExceptionLogger());
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new MyExceptionHandler());

            AreaRegistration.RegisterAllAreas();

            Bootstrapper.Run();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DatabaseInitializer.SeedData();
        }
    }
}

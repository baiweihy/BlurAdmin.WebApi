using System.Web.Http;
using LegacyStandalone.Web.MyConfigurations.Mapping;

namespace LegacyStandalone.Web.MyConfigurations
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }
    }
}
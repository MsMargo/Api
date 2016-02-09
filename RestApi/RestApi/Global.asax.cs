using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestApi.App_Start;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace RestApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapperConfig.RegisterMappings();

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}

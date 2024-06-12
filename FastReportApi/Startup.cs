using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FastReportApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var corsPolicy = new EnableCorsAttribute("*", "*", "*");

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            // Habilitar el enrutamiento de atributos
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.EnableCors(corsPolicy);

            appBuilder.UseWebApi(config);
        }
    }
}

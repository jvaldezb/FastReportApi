using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using PrintConfiguration;

namespace FastReportApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {

            var corsPolicy = new EnableCorsAttribute(FormServer.configuracion.AllowedDomain1, FormServer.configuracion.AllowedDomain2, "*", "*");  

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            // Habilitar el enrutamiento de atributos
            config.MapHttpAttributeRoutes();

            config.EnableCors(corsPolicy);

            // Agregar el mensaje delegador para la restricción de dominio
            if (FormServer.configuracion.OnlyOrigin)
                config.MessageHandlers.Add(new DomainRestrictionHandler());

            appBuilder.UseWebApi(config);
        }
    }
}

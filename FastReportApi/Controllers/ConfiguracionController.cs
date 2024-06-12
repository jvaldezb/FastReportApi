using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;

namespace FastReportApi.Controllers
{
    [RoutePrefix("configuracion")]
    public class ConfiguracionController : ApiController
    {
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {

            
            //return id.ToString();
            //return Request.CreateResponse(HttpStatusCode.OK, id.ToString());

            try
            {
                // Realizar operaciones para obtener la configuración con el id proporcionado
                // Por ahora, simplemente devolvemos el id como respuesta
                EventoComun.RaiseReportCommand(id);

                return Request.CreateResponse(HttpStatusCode.OK, id.ToString());
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un código de estado adecuado
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;

namespace WebPrinter.Controllers
{

    [RoutePrefix("imprimir")]
    public class ImprimirController : ApiController
    {
        
        //[HttpPost]
        //[Route("Print_frx_data")]
        //public async Task<HttpResponseMessage> PrintFrxData()
        //{
        //    try
        //    {
        //        // Leer el cuerpo de la solicitud
        //        string requestBody = await Request.Content.ReadAsStringAsync();

        //        // Convertir el cuerpo de la solicitud a un objeto JSON
        //        JObject requestData = JObject.Parse(requestBody);

        //        // Obtener los valores de los tres parámetros
        //        string nombreArchivo = requestData["nombreArchivo"].ToString();
        //        string archivoBase64String = requestData["archivoBase64String"].ToString();
        //        string listaTablas = requestData["listaTablas"].ToString();

        //        //JArray listaTablas = (JArray)requestData["lista_tablas"];

        //        // Realizar alguna lógica de negocio con los parámetros recibidos
        //        EventoComun.RaisePrintRemoteFrxData(new RemoteFrxData{
        //            FileName = nombreArchivo,
        //            File64String = archivoBase64String,
        //            JsonData = listaTablas
        //        });

        //        // Enviar una respuesta
        //        return Request.CreateResponse(HttpStatusCode.OK, "hecho");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Enviar una respuesta de error si ocurre una excepción
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}


        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Imprimir()
        {
            try
            {
                // Leer el cuerpo de la solicitud
                string requestBody = await Request.Content.ReadAsStringAsync();

                // Convertir el cuerpo de la solicitud a un objeto JSON
                JObject requestData = JObject.Parse(requestBody);

                // Ejecutar el evento de manera asíncrona
                _ = Task.Run(() =>
                  {
                      try
                      {
                          EventoComun.RaisePrintLocalFrxData(new LocalFrxData
                          {
                              JsonData = requestBody
                          });
                      }
                      catch (Exception ex)
                      {
                        // Log de la excepción interna del evento
                        // Log.Error(ex); // Suponiendo que tengas un sistema de logging
                    }
                  });

                // Enviar una respuesta
                return Request.CreateResponse(HttpStatusCode.OK, "hecho");
            }
            catch (Exception ex)
            {
                // Enviar una respuesta de error si ocurre una excepción
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

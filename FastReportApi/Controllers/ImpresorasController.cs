using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FastReportApi.Controllers
{
    [RoutePrefix("Impresoras")]
    public class ImpresorasController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            List<string> listaImpresoras = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                listaImpresoras.Add(printer);
            }

            var payload = new
            {
                impresoras = listaImpresoras.ToArray()
            };

            return Ok(payload);
        }
    }
}

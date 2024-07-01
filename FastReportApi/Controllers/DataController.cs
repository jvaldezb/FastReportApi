using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities;

namespace FastReportApi.Controllers
{
    [RoutePrefix("Data")]
    public class DataController : ApiController
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
                pid = HardwareInfo.GetSerialNumberMainboard()
            };

            return Ok(payload);
        }
    }
}

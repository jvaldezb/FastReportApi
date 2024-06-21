using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastReportApi
{
    public class DomainRestrictionHandler : DelegatingHandler
    {
        private readonly List<string> _allowedDomains = new List<string>
        {
            FormServer.configuracion.AllowedDomain1,
            FormServer.configuracion.AllowedDomain2
        };

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin"))
            {
                var origin = request.Headers.GetValues("Origin").FirstOrDefault();

                if (!_allowedDomains.Contains(origin))
                {
                    var response = request.CreateResponse(HttpStatusCode.Forbidden, "Acceso denegado: Dominio no permitido");
                    return Task.FromResult(response);
                }
            }
            else
            {
                // Si no hay encabezado 'Origin', rechaza la solicitud
                var response = request.CreateResponse(HttpStatusCode.Forbidden, "Acceso denegado: Solicitud directa no permitida");
                return Task.FromResult(response);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}

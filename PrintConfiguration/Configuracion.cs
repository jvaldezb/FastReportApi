using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrintConfiguration
{
    public class Configuracion
    {
        private string _puerto;

        public string Puerto
        {
            get { return _puerto; }
            set
            {
                // Validar que el valor sea un puerto válido
                int puerto;
                if (int.TryParse(value, out puerto) && puerto >= IPEndPoint.MinPort && puerto <= IPEndPoint.MaxPort)
                    _puerto = value;
                else
                    throw new ArgumentException("El valor ingresado no es un puerto válido.");
            }
        }

        // Propiedad para manejar la selección entre imprimir y diseñar
        public bool Imprimir { get; set; }
        public bool Disenar { get; set; }
        public string AllowedDomain1 { get; set; }
        public string AllowedDomain2 { get; set; }
        public bool OnlyOrigin { get; set; }
    }
}

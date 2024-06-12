using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastReportApi
{
    class DatosPrueba
    {
        List<TablasReporte> _listaTablaJson = new List<TablasReporte>();

        public string getJsonData()
        {
            _listaTablaJson = new List<TablasReporte>();
            var maestros = GetListMaestro();
            var detalles = GetListDetalle(maestros);

            _listaTablaJson.Add(new TablasReporte { NombreTabla = "maestros", ListaTabla = maestros });
            _listaTablaJson.Add(new TablasReporte { NombreTabla = "detalles", ListaTabla = detalles });

            var objJson = Newtonsoft.Json.JsonConvert.SerializeObject(_listaTablaJson);
            return objJson;
        }

        List<Maestro> GetListMaestro()
        {
            // Creamos una lista de maestros
            List<Maestro> maestros = new List<Maestro>
            {
                new Maestro(1, "Maestro A"),
                new Maestro(2, "Maestro B"),
                new Maestro(3, "Maestro C"),
                new Maestro(4, "Maestro D"),
                new Maestro(5, "Maestro E")
            };

            return maestros;


        }

        List<Detalle> GetListDetalle(List<Maestro> maestros)
        {
            List<Detalle> detalles = new List<Detalle>();
            // Agregamos algunos detalles a cada maestro
            foreach (var maestro in maestros)
            {
                for (int j = 1; j <= 3; j++)
                {
                    detalles.Add(new Detalle(maestro.Id, j, $"Detalle {j} de maestro {maestro.Nombre}"));
                }
            }
            return detalles;
        }

        public string GetBase64String(string path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            string file = Convert.ToBase64String(bytes);
            return file;
        }
    }
}

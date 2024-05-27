using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPrinter
{
    public static class Util
    {
        public static string RemoveJObjects(string jsonData)
        {
            // remover JObject 
            JArray jsonArray = JArray.Parse(jsonData);

            foreach (JObject item in jsonArray)
            {
                List<JProperty> propertiesToRemove = new List<JProperty>();
                foreach (JProperty property in item.Properties())
                {
                    if (property.Value.Type == JTokenType.Object)
                    {
                        propertiesToRemove.Add(property);
                    }
                }

                foreach (JProperty propertyToRemove in propertiesToRemove)
                {
                    item.Remove(propertyToRemove.Name);
                }
            }

            // Convertir el JSON modificado de nuevo a una lista de objetos
            List<dynamic> objects = JsonConvert.DeserializeObject<List<dynamic>>(jsonArray.ToString());

            // dar formato de tabla si es que es sólo objeto
            jsonData = objects[0].ToString();

            return jsonData;
        }

        public static string ConvertJsonArray(string jsonData)
        {
            jsonData = jsonData.TrimStart();
            if (jsonData.Length > 0)
            {
                if (jsonData[0] == '{')
                    jsonData = "[" + jsonData + "]";
            }

            return jsonData;
        }

        public static string NormalizeJsonTable(string jsonData)
        {
            jsonData = ConvertJsonArray(jsonData);
            jsonData = RemoveJObjects(jsonData);
            jsonData = ConvertJsonArray(jsonData);

            return jsonData;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimQLTask
{
    class SimQLProgram
    {
        static void Main(string[] args)
        {
            var json = Console.In.ReadToEnd();
            foreach (var result in ExecuteQueries(json))
                Console.WriteLine(result);
        }

        public static IEnumerable<string> ExecuteQueries(string json)
        {
            var jObject = JObject.Parse(json);
            var data = (JObject)jObject["data"];
            var queries = jObject["queries"].ToObject<string[]>();
            return queries.Select(q => TryGetValue(data, q));
        }

        public static string TryGetValue(JObject data, string q)
        {
                JObject tmp = data;
                int i = 0;
 
            foreach (string s in q.Split('.'))
                {
               
                if (i < q.Split('.').Length - 1)
                    {
                    if (tmp?[s] == null || (tmp[s] is JValue))
                            return q + " = ";
                        tmp = (JObject) tmp[s];
                    }
                    else
                    {

                    if (tmp?[s] == null || tmp[s] is JObject)
                            return q + " = ";
                
                            return (q + " = " + (tmp[s] as JValue).ToString(CultureInfo.InvariantCulture));
                    }
                    i++;
                }

            //return null;
            return q + " = ";
        } 
    }
}

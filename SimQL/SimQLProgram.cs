﻿using System;
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
            return queries.Select(q => TryGetValue(data, q)).Where(q => q != null);
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
                            return null;
                        tmp = (JObject) tmp[s];
                    }
                    else
                    {
                    Console.WriteLine("3 " + tmp[s]);
                    if (tmp?[s] == null )
                            return null;

                        var jValue = tmp[s] as JValue;
                        if (jValue != null)
                            return (q + " = " + jValue.ToString(CultureInfo.InvariantCulture));
                        else return null;
                    }
                    i++;
                }

            //return null;
            return tmp[q] == null ? null : q + " = ";
        } 
    }
}

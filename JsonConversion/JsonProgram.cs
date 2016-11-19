using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using EvalTask;

namespace JsonConversion
{
	class JsonProgram
	{
		static void Main()
		{
//			string json = Console.In.ReadToEnd();
		    string json = @"{
	'version': '2',
	'constants': { 'a': 3, 'b': 4 },
	'products': {
		'1': {
			'name': 'product-name',
			'price': '3 + a + b',
			'count': 100,
		}
	}
}";
//		    string json = @"{""version"":""2"",""products"":{""642572671"":{""name"":""\t\t\t\t\t\t\t\t\t\t"",""price"":26755360,""count"":2147483647},""462028247"":{""name"":""\t\t\t\t\t\t\t\t\t\t"",""price"":1812829817,""count"":1583821338},""1064089862"":{""name"":""jtXpDL4AA"",""price"":1,""count"":1765575149},""441937189"":{""name"":""LPAI"",""price"":2119059550,""count"":260983550},""1493811026"":{""name"":""M"",""price"":1208992471,""count"":1},""1"":{""name"":"""",""price"":1,""count"":1},""1031623038"":{""name"":""XuNL"",""price"":188661436,""count"":0},""0"":{""name"":""Vz"",""price"":2147483647,""count"":1}}}";
			JObject v2 = JObject.Parse(json);
		    var v3 = convertToJson3(v2);
			Console.Write(v3);
		}

	    private static string convertToJson3(JObject json2)
	    {
            var json3 = new JObject();
            json3.Add("version", "3");
	        var products = json2.GetValue("products");
            var newProducts = new JArray();
            foreach (var child in products.Children())
            {
                var prod = new JObject();
                var jp = (JProperty) child;
                prod.Add("id", int.Parse(jp.Name));
                if (child.HasValues)
                {
                    var children = child.Value<JToken>().Children();
                    var ch = children.ToList()[0];
                    prod.Add("name", ch.Value<string>("name"));
                    JToken jConstants;
                    double price = json2.TryGetValue("constants", out jConstants)
                        ? calcPrice(ch.Value<string>("price"), jConstants)
                        : ch.Value<double>("price");
                    prod.Add("price", price);
                    prod.Add("count", ch.Value<int>("count"));
                }
                newProducts.Add(prod);
            }
            json3.Add("products", newProducts);
	        return json3.ToString(Formatting.None);
	    }

//	    private static IList<Tuple<string, double>> parseConstants(JToken jConstants)
//	    {
//            var constants = new List<Tuple<string, double>>();
//	        var children = jConstants.Children();
//	        foreach (var child in children)
//	        {
//	            string varNamme = ((JProperty) child).Name;
//                double varValue = child.First.Value<double>();
//	        }
//	        return null;
//	    }

	    private static double calcPrice(string expression, JToken jConstants)
	    {
	        return 0.0;
	    }
	}
}

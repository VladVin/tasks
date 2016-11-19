using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace JsonConversion
{
	class JsonProgram
	{
		static void Main()
		{
			string json = Console.In.ReadToEnd();
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
                prod.Add("id", jp.Name);
                if (child.HasValues)
                {
                    var children = child.Value<JToken>().Children();
                    var ch = children.ToList()[0];
                    prod.Add("name", ch.Value<string>("name"));
                    prod.Add("price", ch.Value<double>("price"));
                    prod.Add("count", ch.Value<long>("count"));
                }
                newProducts.Add(prod);
            }
            json3.Add("products", newProducts);
	        return json3.ToString(Formatting.None);
	    }
	}
}

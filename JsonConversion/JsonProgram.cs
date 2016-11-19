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
			string json = Console.In.ReadToEnd();
//		    string json = @"{""version"":""2"",""constants"":{""p"":375570429.0,""BDT5b"":1895861230.0,""pk6rq0teL"":2147483647.0,""d801p5J6"":1994942430.0,""GuuguI"":0.0},""products"":{""0"":{""name"":""RQbl0WfVY"",""price"":""(-84.06633771214)*(p)"",""count"":881891749},""1"":{""name"":""kDaf0Z"",""price"":""(12.1639563293028)+(p)"",""count"":2147483647},""304322859"":{""name"":""KcOi9dvy"",""price"":""(pk6rq0teL)/(-37.0159578216336)"",""count"":1},""1408114756"":{""name"":""v4N"",""price"":""(-85.7739047081088)/(37.9361148634628)"",""count"":1},""1889271802"":{""name"":""wtXpL"",""price"":""(13.8310322136763)*(58.0251485845191)"",""count"":0},""687275581"":{""name"":""bp"",""price"":""(-92.3887056263111)-(d801p5J6)"",""count"":0},""1732466776"":{""name"":""IohrwvzQ"",""price"":""(d801p5J6)+(41.3040054688715)"",""count"":2147483647},""2147483647"":{""name"":""Abap"",""price"":""(-59.916888624391)-(-66.5829328664499)"",""count"":2147483647},""517841970"":{""name"":""KD9S4"",""price"":""(58.4912672445603)/(p)"",""count"":2147483647},""1362982700"":{""name"":""9pH"",""price"":""(-54.4264007147524)/(-45.6487527795363)"",""count"":1}}}";
//		    string json = @"{""version"":""2"",""products"":{""642572671"":{""name"":""\t\t\t\t\t\t\t\t\t\t"",""price"":26755360,""count"":2147483647},""462028247"":{""name"":""\t\t\t\t\t\t\t\t\t\t"",""price"":1812829817,""count"":1583821338},""1064089862"":{""name"":""jtXpDL4AA"",""price"":1,""count"":1765575149},""441937189"":{""name"":""LPAI"",""price"":2119059550,""count"":260983550},""1493811026"":{""name"":""M"",""price"":1208992471,""count"":1},""1"":{""name"":"""",""price"":1,""count"":1},""1031623038"":{""name"":""XuNL"",""price"":188661436,""count"":0},""0"":{""name"":""Vz"",""price"":2147483647,""count"":1}}}";
			JObject v2 = JObject.Parse(json);
		    var v3 = ConvertToJson3(v2);
			Console.Write(v3);
		}

	    private static string ConvertToJson3(JObject json2)
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
                        ? CalcPrice(ch.Value<string>("price"), jConstants)
                        : ch.Value<double>("price");
                    prod.Add("price", price);
                    prod.Add("count", ch.Value<int>("count"));
                }
                newProducts.Add(prod);
            }
            json3.Add("products", newProducts);
	        return json3.ToString(Formatting.None);
	    }

//	    private static IList<Tuple<string, double>> ParseConstants(JToken jConstants)
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

	    private static double CalcPrice(string expression, JToken jConstants)
	    {
	        var evalStr = Evaluator.Evaluate(expression, jConstants);
            return double.Parse(evalStr.Replace(".", ","));
	    }
	}
}

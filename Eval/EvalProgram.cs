using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			var expression = Console.ReadLine();
			var json = Console.In.ReadToEnd();

			var literals = JObject.Parse(json);
			expression = Evaluator.SubstituteConstants(expression, literals);

          	var output = Evaluator.Evaluate(expression);
			Console.WriteLine(output);
		}
	}
}

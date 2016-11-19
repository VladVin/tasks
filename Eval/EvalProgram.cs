using System;
using Newtonsoft.Json.Linq;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			var expression = Console.ReadLine();
			var json = Console.In.ReadToEnd();

			if (json != "")
			{
				var literals = JObject.Parse(json);
				expression = Evaluator.SubstituteConstants(expression, literals);
			}
			var output = "";
			try
			{
				output = Evaluator.Evaluate(expression);
			}
			catch (System.Data.SyntaxErrorException ignored)
			{
				output = "error";
			}
			Console.WriteLine(output);
		}
	}
}

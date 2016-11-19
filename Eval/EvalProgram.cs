using System;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			var input = Console.In.ReadToEnd();
			var output = Evaluator.Evaluate(input);
			Console.WriteLine(output);
		}
	}
}

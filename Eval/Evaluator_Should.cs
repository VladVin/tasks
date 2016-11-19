using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace EvalTask
{
	[TestFixture]
	public class Evaluator_Should
	{
		[Test]
		public void AddTwoIntegers()
		{
			var input = "3 + 5";
			var output = Evaluator.Evaluate(input);
			Assert.AreEqual("8", output);

		}

		[Test]
		public void MultiplyTwoIntegers()
		{
			var input = "3 * 5";
			var output = Evaluator.Evaluate(input);
			Assert.AreEqual("15", output);

		}

		[Test]
		public void EvaluateAccordingToBraces()
		{
			var input = "(2 + 3) * 5";
			var output = Evaluator.Evaluate(input);
			Assert.AreEqual("25", output);

		}

		[Test]
		public void SubstituteConstantsCorrectly()
		{
			var input = "{'a' : 42}";
			var expression = "a";
			var literals = JObject.Parse(input);
			var output = Evaluator.SubstituteConstants(expression, literals);
			Assert.AreEqual("42", output);
		}

		[Test]
		public void PrintFloatingPointsWithDot()
		{
			var input = "0.1";
			var output = Evaluator.Evaluate(input);
			Assert.AreEqual(input, output);
		}
	}
}

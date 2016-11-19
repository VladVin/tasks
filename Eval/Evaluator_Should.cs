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
	}
}

using NUnit.Framework;

namespace SimQLTask
{
	[TestFixture]
	public class SimQLProgram_Should
	{
		[Test]
		public void SumEmptyDataToZero()
		{
			var results = SimQLProgram.ExecuteQueries(
				"{" +
				"'data': [], " +
				"'queries': ['sum(item.cost)', 'sum(itemsCount)']}");
			Assert.AreEqual(new[] {0, 0}, results);
		}

		[Test]
		public void SumSingleItem()
		{
			var results = SimQLProgram.ExecuteQueries(
				"{" +
				"'data': [{'itemsCount':42}, {'foo':'bar'}], " +
				"'queries': ['sum(itemsCount)']}");
			Assert.AreEqual(new[] { 42 }, results);
		}
        
	    [Test]
	    public void testShouldBe_Green()
	    {
            var results = SimQLProgram.ExecuteQueries(
                "{" +
                "'data': {'empty': {}, 'ab':'0', 'x1': '1', 'x2': '2', 'y1': {'y2': {'y3': '3'}}}, " +
                "'queries': ['empty', 'xyz', 'x1.x2', 'y1.y2.z', 'empty.foobar']}");
            Assert.AreEqual(new string[0], results);
        }

        [Test]
        public void testFrom_example()
        {
            var results = SimQLProgram.ExecuteQueries(
                "{" +
                "'data': {'a': {'x':3.14, 'b': { 'c':15}, 'c': { 'c':9}}, 'z':42 }," +
                "'queries': ['a.b.c', 'z', 'a.x']}");
            Assert.AreEqual(new[] {"a.b.c = 15", "z = 42", "a.x = 3.14" }, results);
        }
    }
}
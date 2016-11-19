namespace EvalTask
{
	public static class Evaluator
	{
		public static string Evaluate(string expression)
		{
			System.Data.DataTable table = new System.Data.DataTable();
			table.Columns.Add("expression", string.Empty.GetType(), expression);
			System.Data.DataRow row = table.NewRow();
			table.Rows.Add(row);
			return row["expression"] as string;
		}
	}
}
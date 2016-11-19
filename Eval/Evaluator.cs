using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

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

		public static string SubstituteConstants(string expression, JObject constants)
		{
			foreach (var literal in constants.Children())
			{
				expression = Regex.Replace(expression,
				                           (literal as JProperty).Name,
				                           literal.First.Value<double>().ToString());
			}
			return expression;
		}
	}
}
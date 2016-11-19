using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace EvalTask
{
	public static class Evaluator
	{
		public static string Evaluate(string expression)
		{
			try
			{
				System.Data.DataTable table = new System.Data.DataTable();
				table.Locale = CultureInfo.InvariantCulture;
				table.Columns.Add("expression", string.Empty.GetType(), expression);
				System.Data.DataRow row = table.NewRow();
				table.Rows.Add(row);
				return row["expression"] as string;
			}
			catch
			{
				return "error";
			}
		}

		public static string SubstituteConstants(string expression, JToken constants)
		{
		    return constants.Children()
                .OrderBy(token => -(token as JProperty).Name.Length)
                .Aggregate(expression, (current, literal) =>
                    Regex.Replace(current, (literal as JProperty).Name,
                        literal.First.Value<double>().ToString(CultureInfo.InvariantCulture)));
		}

		public static string Evaluate(string expression, JToken constants)
		{
			return Evaluate(SubstituteConstants(expression, constants));
		}
	}
}
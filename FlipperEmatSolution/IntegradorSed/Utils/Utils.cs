using CsvHelper;
using Emat.IntegracaoSedConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emat.IntegracaoSedConsoleApp.Utils
{
	public static class Utils
	{
		public static string ToCsv<T>(this IEnumerable<T> items)
			where T : class
		{
			var csvBuilder = new StringBuilder();
			var properties = typeof(T).GetProperties();
			foreach (T item in items)
			{
				string line = string.Join(",", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
				csvBuilder.AppendLine(line);
			}
			return csvBuilder.ToString();
		}

		private static string ToCsvValue<T>(this T item)
		{
			if (item == null) return "\"\"";

			if (item is string)
			{
				return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
			}
			double dummy;
			if (double.TryParse(item.ToString(), out dummy))
			{
				return string.Format("{0}", item);
			}
			return string.Format("\"{0}\"", item);
		}

		public static void ExportarParaCsv(string fullPath, List<Lancamento> lancamentos)
		{
			using (var writer = new StreamWriter($"{fullPath}.csv"))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(lancamentos);
			}
		}

		//private static void _storeDataInCsvFile()
		//{
		//	StringBuilder output = new StringBuilder();
		//	output.AppendLine(string.Join(",", new String[] { "1", "2", "3" }));
		//	// CSV File Output = 1,2,3
		//}
		//private static long _countLinesInFile(string f)
		//{
		//	long count = 0;
		//	using (StreamReader r = new StreamReader(f))
		//	{
		//		string line;
		//		while ((line = r.ReadLine()) != null)
		//		{
		//			count++;
		//		}
		//	}
		//	return count;
		//}

		public static void ExportTxtFile(string fileContent, string fullPath)
		{
			File.WriteAllText(fullPath, fileContent);
		}
	}
}

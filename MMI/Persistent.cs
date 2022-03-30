using MMI.Models;
using Spectre.Console;

namespace MMI
{
	public class Persistent
	{
		public static Quotation CurrentQuotation = new();
		public static Table QuotationTable = new();
	}
}
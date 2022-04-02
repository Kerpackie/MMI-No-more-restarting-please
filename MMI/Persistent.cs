using MMI.Models;
using Spectre.Console;

namespace MMI
{
	/// <summary>
	/// This whole class just exists, to exist - It could also be made into a Singleton Service and injected into
	/// The main application. However, laziness is a virtue.
	///
	/// The objects are instantiated so that they are persistent across the application, and are primarily used for
	/// transferring data between the various modules.
	/// </summary>
	public class Persistent
	{
		public const string apiUri = "https://mmi20220402025319.azurewebsites.net/api/";
		public static Quotation CurrentQuotation = new();
		public static Table QuotationTable = new();
		public static Table CustomerTable = new();
	}
}
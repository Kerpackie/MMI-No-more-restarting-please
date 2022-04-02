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
		/// <summary>
		/// A persistent implementation of a table, that is used to store information about the current quotation that is
		/// being generated.
		/// </summary>
		public static Table QuotationTable = new();
		
		/// <summary>
		/// A persistent implementation of a table, that is used to store information about the current customer that is
		/// being generated.
		/// </summary>
		public static Table CustomerTable = new();
	}
}
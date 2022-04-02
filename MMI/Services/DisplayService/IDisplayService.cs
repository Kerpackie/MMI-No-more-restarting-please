using System.Collections.Generic;
using MMI.Models;
using Spectre.Console;

namespace MMI.Services.DisplayService
{
	/// <summary>
	/// The interface for the display service.
	/// </summary>
	public interface IDisplayService
	{
		void MenuFiglet();
		void TableInit(Table table);
		void RenderQuotationCriteriaTable();
		void RenderCustomerTable();
		void QuotationTableClear();
		void UpdateQuotationTableCells(int row, string menuItem, Quotation quotation);
		void UpdateCustomerTableCells(int row, string menuItem);
		public void CustomerTable(Table table);
		Table DisplayQuotation(Table table, Quotation quotation);
		Table DisplayExpiringReport(Table table, List<Quotation> quotations);
		public void RenderQuotationTable(Quotation quotation);
		public void RenderExpiringTable(List<Quotation> quotations);
		
		// Rules
		void RuleRedLeft(string text);
		void RuleRedRight(string text);
		void RuleRedCentre(string text);
		void RuleGreenLeft(string text);
		void RuleGreenRight(string text);
		void RuleGreenCentre(string text);
		
		// Loading prompts
		void LoadingPrompt();

	}
}
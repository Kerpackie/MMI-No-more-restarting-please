using Spectre.Console;

namespace MMI.Services.DisplayService
{
	public class DisplayService : IDisplayService
	{
		
		public void MenuFiglet()
		{
			AnsiConsole.Write(
				new FigletText("Munster Motor Insurance")
					.Centered()
					.Color(Color.Red));
		}

		public void TableInit()
		{
			var table = Persistent.QuotationTable;
			
			// Add some columns
			table.AddColumn("Criteria").Centered();
			table.AddColumn("Value").Centered();
			
			// Add some rows
			table.AddRow("Sex", Persistent.CurrentQuotation.Sex);
			table.AddRow("Age", Persistent.CurrentQuotation.Age.ToString());
			table.AddRow("County", Persistent.CurrentQuotation.County);
			table.AddRow("Vehicle Model", Persistent.CurrentQuotation.Model);
			table.AddRow("Emissions Class", Persistent.CurrentQuotation.Emissions);
			table.AddRow("Insurance Category", Persistent.CurrentQuotation.InsuranceCategory);
			table.AddRow("Total Cost:", Persistent.CurrentQuotation.TotalCost.ToString());

		}
		
		public void RenderQuotationCriteriaTable()
		{
			AnsiConsole.Clear();
			MenuFiglet();
			
			var table = Persistent.QuotationTable;
			AnsiConsole.Write(table);
		}

		public void QuotationTableClear()
		{
			var table = Persistent.QuotationTable;
			
			var t = table.Rows.Count;

			for (int i = 0; i < t; i++)
			{
				table.RemoveRow(0);
			}
		}

		public void UpdateQuotationTableCells(int row, string menuItem)
		{
			var table = Persistent.QuotationTable;
			table.UpdateCell(row, 1, menuItem);
			
			Persistent.CurrentQuotation.QuotationTotalCost();
			table.UpdateCell(6, 1, Persistent.CurrentQuotation.TotalCost.ToString());
		}

	}
}
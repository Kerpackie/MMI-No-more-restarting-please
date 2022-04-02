using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MMI.Models;
using Spectre.Console;

namespace MMI.Services.DisplayService
{
	public class DisplayService : IDisplayService
	{
		private readonly ILogger<DisplayService> _logger;

		public DisplayService(ILogger<DisplayService> logger)
		{
			_logger = logger;
		}
		
		public void MenuFiglet()
		{
			_logger.LogDebug("Clearing screen");
			AnsiConsole.Clear();
			
			_logger.LogDebug("Printing figlet");
			AnsiConsole.Write(
				new FigletText("Munster Motor Insurance")
					.Centered()
					.Color(Color.Red));
		}

		public void TableInit(Table table)
		{
			_logger.LogDebug("Adding table headers");
			// Add some columns
			table.AddColumn("Criteria").Centered();
			table.AddColumn("Value").Centered();
			
			_logger.LogDebug("Adding table rows");
			// Add some rows
			table.AddRow("Sex", string.Empty);
			table.AddRow("Age", string.Empty);
			table.AddRow("County", string.Empty);
			table.AddRow("Vehicle Model", string.Empty);
			table.AddRow("Emissions Class", string.Empty);
			table.AddRow("Insurance Category", string.Empty);
			table.AddRow("Total Cost:", string.Empty);

		}
		
		public void RenderQuotationCriteriaTable()
		{
			MenuFiglet();
			_logger.LogDebug("Rendering Quotation table");
			var table = Persistent.QuotationTable;
			AnsiConsole.Write(table);
		}

		public void RenderCustomerTable()
		{
			MenuFiglet();
			_logger.LogDebug("Rendering Customer table");
			var table = Persistent.CustomerTable;
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

		public void UpdateQuotationTableCells(int row, string menuItem, Quotation quotation)
		{
			_logger.LogDebug("Updating Quotation table cell: {row} {menuItem}", row, menuItem);
			var table = Persistent.QuotationTable;
			table.UpdateCell(row, 1, menuItem);
			
			quotation.QuotationTotalCost();
			table.UpdateCell(6, 1, quotation.TotalCost.ToString());
		}

		public void UpdateCustomerTableCells(int row, string menuItem)
		{
			_logger.LogDebug("Updating Customer table cell: {row} {menuItem}", row, menuItem);
			var table = Persistent.CustomerTable;
			table.UpdateCell(row, 1, menuItem);
		}

		public void CustomerTable(Table table)
		{
			_logger.LogDebug("Adding Customer table headers");
			table.AddColumn("Criteria").Centered();
			table.AddColumn("Value").Centered();
			
			_logger.LogDebug("Adding Customer table rows");
			table.AddRow("Customer:", "");
			table.AddRow("First Name:", "");
			table.AddRow("Last Name:", "");
			table.AddRow("Address:", "");
			table.AddRow("Street:", "");
			table.AddRow("County:", "");
			table.AddRow("Eircode:", "");
			table.AddRow("Phone:", "");
			table.AddRow("Mobile:", "");
		}

		public Table DisplayQuotation(Table table, Quotation quotation)
		{
			_logger.LogInformation("Displaying Quotation");
			
			_logger.LogDebug("Adding Quotation table headers");
			// Add some columns
			table.AddColumn("Criteria").Centered();
			table.AddColumn("Value").Centered();

			_logger.LogDebug("Adding Quotation rows");
			// Add some rows
			table.AddRow("Sex", quotation.Sex);
			table.AddRow("Age", quotation.Age.ToString());
			table.AddRow("County", quotation.County);
			table.AddRow("Vehicle Model", quotation.Model);
			table.AddRow("Emissions Class", quotation.Emissions);
			table.AddRow("Insurance Category", quotation.InsuranceCategory);
			table.AddRow("Total Cost:", quotation.TotalCost.ToString());
			table.AddRow("Valid Until:", quotation.ValidUntil.Date.ToString("dd/MM/yyyy"));

			_logger.LogDebug("Adding Customer Rows");
			table.AddEmptyRow();
			table.AddRow("Customer:", "");
			table.AddRow("Name:", quotation.Customer.FirstName + " " + quotation.Customer.Surname);
			table.AddRow("Address:", "");
			table.AddRow("Street:", quotation.Customer.Street);
			table.AddRow("County:", quotation.Customer.County);
			table.AddRow("Eircode:", quotation.Customer.Eircode);
			table.AddRow("Phone:", "");
			table.AddRow("Mobile:", quotation.Customer.PhoneNumber);
			
			return table;
		}

		public Table DisplayExpiringReport(Table table, List<Quotation> quotations)
		{
			_logger.LogInformation("Displaying Expiring Report");
			
			table.AddColumn("Policy ID:").Centered();
			table.AddColumn("Customer:").Centered();
			table.AddColumn("Vehicle Model:").Centered();
			table.AddColumn("Insurance Category:").Centered();
			table.AddColumn("Total Cost:").Centered();
			table.AddColumn("Valid Until:").Centered();
			table.AddColumn("Phone Number:").Centered();

			if (quotations != null)
			{
				_logger.LogDebug("Adding Expiring Report rows for {quotations.Count} Quotations", quotations.Count);
				foreach (var quotation in quotations)
				{
					table.AddRow(quotation.Id.ToString(), quotation.Customer.FirstName + " " + quotation.Customer.Surname,
						quotation.Model, quotation.InsuranceCategory, quotation.TotalCost.ToString(),
						quotation.ValidUntil.Date.ToString("dd/MM/yyyy"), quotation.Customer.PhoneNumber);
				}
			}

			if (quotations == null)
			{
				_logger.LogError("Unable to display Expiring Report - Quotations is null");
			}
			return table;
		}

		public void RenderQuotationTable(Quotation quotation)
		{
			MenuFiglet();
			_logger.LogInformation("Rendering Quotation Table");
			var table = DisplayQuotation(new Table(), quotation); 
			AnsiConsole.Write(table);
		}

		public void RenderExpiringTable(List<Quotation> quotations)
		{
			MenuFiglet();
			_logger.LogInformation("Rendering Expiring Table");
			var table = DisplayExpiringReport(new Table(), quotations); 
			AnsiConsole.Write(table);
		}

		public void RuleRedLeft(string text)
		{
			var rule = new Rule($"[red]{text}[/]");
			rule.Alignment = Justify.Left;
			AnsiConsole.Write(rule);
		}

		public void RuleRedRight(string text)
		{
			var rule = new Rule($"[red]{text}[/]");
			rule.Alignment = Justify.Right;
			AnsiConsole.Write(rule);
		}

		public void RuleRedCentre(string text)
		{
			var rule = new Rule($"[red]{text}[/]");
			rule.Alignment = Justify.Center;
			AnsiConsole.Write(rule);
		}

		public void RuleGreenLeft(string text)
		{
			var rule = new Rule($"[green]{text}[/]");
			rule.Alignment = Justify.Left;
			AnsiConsole.Write(rule);
		}

		public void RuleGreenRight(string text)
		{
			var rule = new Rule($"[green]{text}[/]");
			rule.Alignment = Justify.Right;
			AnsiConsole.Write(rule);
		}

		public void RuleGreenCentre(string text)
		{
			var rule = new Rule($"[green]{text}[/]");
			rule.Alignment = Justify.Center;
			AnsiConsole.Write(rule);
		}

		public void LoadingPrompt()
		{
			RuleRedLeft("Loading... This may take a few seconds...");
		}
	}
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MMI.Services.DisplayService;
using Spectre.Console;

namespace MMI.Services.MenuService
{
	public class MenuService : IMenuService
	{
		private readonly IDisplayService _displayService;

		public MenuService(IDisplayService displayService)
		{
			_displayService = displayService;
		}
		
		

		public void DisplayMainMenu()
		{
			_displayService.MenuFiglet();
			
			var menuItems = new [] { "Generate New Quotation", "Exit" };
			
			var menu = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select a menu option:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));
			
			MainMenuRouting(menu);
		}

		public void DisplayGenerateQuotation()
		{
			throw new System.NotImplementedException();
		}

		public void MainMenuRouting(string menuOption)
		{
			switch (menuOption)
			{
				case "Generate New Quotation":
					AnsiConsole.Write(new Rule("Quotation Criteria").LeftAligned());
					SelectQuotationCriteriaMenu();
					break;
				case "Exit":
					break;
					// Exit application
			}
		}

		public void GenerateQuotationRouting(string menuOption)
		{
			switch (menuOption)
			{
				case "Sex":
					CriteriaSelectionMenuGender();
					break;
				case "Age":
					CriteriaSelectionMenuAge();
					break;
				case "County":
					CriteriaSelectionMenuCounty();
					break;
				case "Model":
					CriteriaSelectionMenuMake();
					break;
				case "Emissions Category":
					CriteriaSelectionMenuEmissionsCategory();
					break;
				case "Insurance Category":
					CriteriaSelectionMenuInsuranceCategory();
					break;
				case "Exit":
					break;
				// Exit application
			}
		}

		public void SelectQuotationCriteriaMenu()
		{
			_displayService.RenderQuotationCriteriaTable();
			var menuItems = new [] { "Sex", "Age", "County", "Model", "Emissions Category", "Insurance Category" };
			
			var menuItem = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select a menu option:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));

			GenerateQuotationRouting(menuItem);
		}

		public void CriteriaSelectionMenuGender()
		{
			var menuItems = new [] { "Male", "Female" };
			
			var menuOption = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select a sex:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));

			Persistent.CurrentQuotation.Sex = menuOption;
			Persistent.QuotationTable.UpdateCell(0, 1, menuOption);
			
			SelectQuotationCriteriaMenu();
		}

		public void CriteriaSelectionMenuAge()
		{
			var customerAge = AnsiConsole.Prompt(
				new TextPrompt<int>("[green]Customer Age:[/]"));

			Persistent.CurrentQuotation.Age = customerAge;
			Persistent.QuotationTable.UpdateCell(1, 1, customerAge.ToString());
			
			SelectQuotationCriteriaMenu();
		}

		public void CriteriaSelectionMenuCounty()
		{
			var menuItems = new [] { "Limerick", "Tipperary", "Cork", "Clare", "Kerry", "Waterford" };
			
			var menuOption = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select a county:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));

			Persistent.CurrentQuotation.County = menuOption;
			Persistent.QuotationTable.UpdateCell(2, 1, menuOption);

			SelectQuotationCriteriaMenu();
		}

		public void CriteriaSelectionMenuMake()
		{
			var menuItems = new [] { "BMW", "Opel", "Toyota", "Renault" };
			
			var menuOption = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select a Vehicle Make:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));
			
			var vehicleMakeList = new List<string>();
			
			switch (menuOption)
			{
				case "BMW":
					var bmw = new[] {"Convertible", "Gran Turismo", "X6", "Z4 Roadster"};
					vehicleMakeList.AddRange(bmw);
					break;
				case "Opel":
					var opel = new[] {"Corsa", "Astra", "Vectra"};
					vehicleMakeList.AddRange(opel);
					break;
				case "Toyota":
					var toyota = new[] {"Auris", "Yaris", "Corolla, Avensis"};
					vehicleMakeList.AddRange(toyota);
					break;
				case "Renault":
					var renault = new[] {"Fleunce", "Megane", "Clio"};
					vehicleMakeList.AddRange(renault);
					break;
			}

			var customerVehicle = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title($"Please select a {menuOption} Model:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(vehicleMakeList));

			Persistent.CurrentQuotation.Model = customerVehicle;
			
			var cellVehicle = $"{menuOption}-{customerVehicle}";
			Persistent.QuotationTable.UpdateCell(3, 1, cellVehicle);
			
			SelectQuotationCriteriaMenu();
		}

		public void CriteriaSelectionMenuEmissionsCategory()
		{
			var menuItems = new [] { "Low", "Medium", "High" };
			
			var menuOption = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select a vehicle emissions category:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));

			Persistent.CurrentQuotation.Emissions = menuOption;
			Persistent.QuotationTable.UpdateCell(4, 1, menuOption);

			SelectQuotationCriteriaMenu();
		}

		public void CriteriaSelectionMenuInsuranceCategory()
		{
			var menuItems = new [] { "Fully Comprehensive", "Third Party Fire and Theft" };
			
			var menuOption = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please select an Insurance Category:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuItems));

			Persistent.CurrentQuotation.InsuranceCategory = menuOption;
			Persistent.QuotationTable.UpdateCell(5, 1, menuOption);

			SelectQuotationCriteriaMenu();
		}
	}
}
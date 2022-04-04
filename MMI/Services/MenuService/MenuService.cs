using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Logging;
using MMI.Models;
using MMI.Services.DisplayService;
using MMI.Services.FileService;
using MMI.Services.QuotationService;
using Serilog;
using Spectre.Console;

namespace MMI.Services.MenuService
{
	/// <summary>
	/// The implementation of the <see cref="IMenuService"/> interface.
	/// Handles the menu navigation and the menu display.
	/// </summary>
	public class MenuService : IMenuService
	{
		private readonly IDisplayService _displayService;
		private readonly IQuotationService _quotationService;
		private readonly IFileService _fileService;
		private readonly ILogger<MenuService> _logger;

		/// <summary>
		/// The constructor of the menu service
		/// </summary>
		/// <param name="displayService">The Display Service DI Container</param>
		/// <param name="quotationService">The Quotation Service DI Container</param>
		/// <param name="fileService">The File Service DI Container</param>
		/// <param name="logger">The logger DI Container</param>
		public MenuService(IDisplayService displayService,
			IQuotationService quotationService,
			IFileService fileService, 
			ILogger<MenuService> logger)
		{
			_displayService = displayService;
			_quotationService = quotationService;
			_fileService = fileService;
			_logger = logger;
		}
		
		/// <summary>
		/// Displays the main menu
		/// </summary>
		public void DisplayMainMenu()
		{
			_logger.LogInformation("Displaying main menu");
			
			_displayService.MenuFiglet();
			_displayService.RuleRedLeft("Main Menu");
			
			const string prompt = "Please select an option";
			var menuItems = new [] { "Generate New Quotation", "Search for Quotation", "Search for Policy", "Reports", "Exit" };

			var menu = DisplayMenuPrompt(prompt, menuItems);
			
			_logger.LogDebug("Menu selected: {menu}", menu);

			MainMenuRouting(menu);
		}

		/// <summary>
		/// Routing for the MainMenu() Method.
		/// </summary>
		/// <param name="menuOption">The option received from MainMenu()</param>
		public void MainMenuRouting(string menuOption)
		{
			switch (menuOption)
			{
				case "Generate New Quotation":
					AnsiConsole.Write(new Rule("Quotation Criteria").LeftAligned());
					SelectQuotationCriteriaMenu(new Quotation());
					break;
				case "Search for Quotation":
					SearchQuotationMenu();
					break;
				case "Search for Policy":
					SearchPolicyMenu();
					break;
				case "Reports":
					ReportsMenu();
					break;
				case "Exit":
					_displayService.RuleRedLeft("Exiting...");
					Thread.Sleep(1000);
					break;
			}
		}

		/// <summary>
		/// The Main Menu for Quotation Criteria Selection
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void SelectQuotationCriteriaMenu(Quotation quotation)
		{
			_logger.LogInformation("Displaying quotation criteria menu");
			
			_displayService.RenderQuotationCriteriaTable();
			_displayService.RuleRedLeft("Create New Quotation");
			
			const string prompt = "Please select a menu option";
			var menuItems = new [] { "Sex", "Age", "County", "Model", "Emissions Category", "Insurance Category", "Save Quotation", "Main Menu" };

			var menuItem = DisplayMenuPrompt(prompt, menuItems);
			
			_logger.LogDebug("Menu selected: {menu}", menuItem);

			switch (menuItem)
			{
				case "Sex":
					CriteriaSelectionMenuGender(quotation);
					break;
				case "Age":
					CriteriaSelectionMenuAge(quotation);
					break;
				case "County":
					CriteriaSelectionMenuCounty(quotation);
					break;
				case "Model":
					CriteriaSelectionMenuMake(quotation);
					break;
				case "Emissions Category":
					CriteriaSelectionMenuEmissionsCategory(quotation);
					break;
				case "Insurance Category":
					CriteriaSelectionMenuInsuranceCategory(quotation);
					break;
				case "Save Quotation":
					quotation.Customer = new Customer();
					AddCustomerMenu(quotation);
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// The Sex Selection Menu for <see cref="Quotation"/> Criteria
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void CriteriaSelectionMenuGender(Quotation quotation)
		{
			_logger.LogInformation("Displaying Sex Selection Menu");

			const string prompt = "Please select a sex";
			var menuItems = new [] { "Male", "Female" };

			var menuOption = DisplayMenuPrompt(prompt, menuItems);
			
			_logger.LogDebug("Menu Selected: {menu}", menuOption);

			quotation.Sex = menuOption;
			_logger.LogDebug("Quotation Property Sex set to: {menu}", menuOption);
			
			_displayService.UpdateQuotationTableCells(0, quotation.Sex, quotation);
			
			SelectQuotationCriteriaMenu(quotation);
		}

		/// <summary>
		/// The Age Selection Prompt for <see cref="Quotation"/> Criteria
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void CriteriaSelectionMenuAge(Quotation quotation)
		{
			_logger.LogInformation("Displaying Age Selection Menu");
			
			var customerAge = AnsiConsole.Prompt(
				new TextPrompt<int>("[green]Customer Age:[/]")
					.PromptStyle("green")
					.ValidationErrorMessage("[red]That's not a valid age[/]")
					.Validate(age =>
					{
						return age switch
						{
							<= 16 => ValidationResult.Error("[red]Must be 17 or older.[/]"),
							>= 88 => ValidationResult.Error("[red]Must be younger than 88.[/]"),
							_ => ValidationResult.Success(),
						};
					}));
			
			quotation.Age = customerAge;
			
			_logger.LogDebug("Quotation Property Age set to: {menu}", customerAge);
			
			_displayService.UpdateQuotationTableCells(1, quotation.Age.ToString(), quotation);
			SelectQuotationCriteriaMenu(quotation);
		}

		/// <summary>
		/// The County Selection Menu for <see cref="Quotation"/> Criteria
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void CriteriaSelectionMenuCounty(Quotation quotation)
		{
			_logger.LogInformation("Displaying County Selection Menu");

			const string prompt = "Please select a county";
			var menuItems = new [] { "Limerick", "Tipperary", "Cork", "Clare", "Kerry", "Waterford" };

			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			quotation.County = menuOption;
			
			_logger.LogDebug("Quotation Property County set to: {menu}", menuOption);
			
			_displayService.UpdateQuotationTableCells(2, quotation.County, quotation);

			SelectQuotationCriteriaMenu(quotation);
		}

		/// <summary>
		/// The Vehicle Model Selection Menu for <see cref="Quotation"/> Criteria
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void CriteriaSelectionMenuMake(Quotation quotation)
		{
			_logger.LogInformation("Displaying Make Selection Menu");
			
			const string prompt = "Please select a vehicle make";
			var menuItems = new [] { "BMW", "Opel", "Toyota", "Renault" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

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
					var toyota = new[] {"Auris", "Yaris", "Corolla", "Avensis"};
					vehicleMakeList.AddRange(toyota);
					break;
				case "Renault":
					var renault = new[] {"Fleunce", "Megane", "Clio"};
					vehicleMakeList.AddRange(renault);
					break;
			}

			_logger.LogInformation("Displaying Model Selection Menu");
			
			var vehiclePrompt = $"Please select a {menuOption} model";

			var customerVehicle = DisplayMenuPrompt(vehiclePrompt, vehicleMakeList.ToArray());

			quotation.Model = customerVehicle;
			
			_logger.LogDebug("Quotation Property Model set to: {menu}", customerVehicle);
			
			var cellVehicle = $"{menuOption}-{customerVehicle}";
			_displayService.UpdateQuotationTableCells(3, cellVehicle, quotation);
			
			SelectQuotationCriteriaMenu(quotation);
		}

		/// <summary>
		/// The Emissions Class Selection Menu for <see cref="Quotation"/> Criteria
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void CriteriaSelectionMenuEmissionsCategory(Quotation quotation)
		{
			_logger.LogInformation("Displaying Emissions Category Selection Menu");

			const string prompt = "Please select a vehicle emissions class";
			var menuItems = new [] { "Low", "Medium", "High" };

			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			quotation.Emissions = menuOption;
			
			_logger.LogDebug("Quotation Property Emissions set to: {menu}", menuOption);
			
			_displayService.UpdateQuotationTableCells(4, quotation.Emissions, quotation);

			SelectQuotationCriteriaMenu(quotation);
		}

		/// <summary>
		/// The Insurance Category Selection Menu for <see cref="Quotation"/> Criteria
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void CriteriaSelectionMenuInsuranceCategory(Quotation quotation)
		{
			_logger.LogInformation("Displaying Insurance Category Selection Menu");
			
			const string prompt = "Please select an insurance category:"; 
			var menuItems = new [] { "Fully Comprehensive", "Third Party Fire and Theft" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			quotation.InsuranceCategory = menuOption;
			
			_logger.LogDebug("Quotation Property InsuranceCategory set to: {menu}", menuOption);
			
			_displayService.UpdateQuotationTableCells(5, quotation.InsuranceCategory, quotation);

			SelectQuotationCriteriaMenu(quotation);
		}

		/// <summary>
		/// Prompt to progress from <see cref="Quotation"/> to the <see cref="Customer"/> Generation Menu
		/// to save the <see cref="Quotation"/>.
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public async void SaveQuotationToDatabase(Quotation quotation)
		{
			_logger.LogInformation("Save Quotation to Database Menu Loaded");
			
			_displayService.RuleRedLeft("Save Quotation?");
			if (!AnsiConsole.Confirm("Would you like to save the quotation to the database?"))
			{
				SelectQuotationCriteriaMenu(quotation);
			}

			var savedQuotation = await _quotationService.SaveQuotationAsync(quotation);
			ConvertQuotationToPolicy(savedQuotation);
		}

		/// <summary>
		/// The Main <see cref="Customer"/> Navigation Menu
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerMenu(Quotation quotation)
		{
			_logger.LogInformation("Add Customer Menu Loaded");
			
			_displayService.RenderCustomerTable();
			_displayService.RuleRedLeft("Create New Customer");

			const string prompt = "Please select a menu option";
			var menuItems = new [] { "First Name", "Surname", "Street", "County", "Eircode", "Phone Number", "Save Quotation", "Back", "Main Menu" };

			var menuItem = DisplayMenuPrompt(prompt, menuItems);

			switch (menuItem)
			{
				
				case "First Name":
					AddCustomerFirstName(quotation);
					break;
				case "Surname":
					AddCustomerLastName(quotation);
					break;
				case "Street":
					AddCustomerStreet(quotation);
					break;
				case "County":
					AddCustomerCounty(quotation);
					break;
				case "Eircode":
					AddCustomerEirCode(quotation);
					break;
				case "Phone Number":
					AddCustomerPhoneNumber(quotation);
					break;
				case "Save Quotation":
					SaveQuotationToDatabase(quotation);
					break;
				case "Back":
					SelectQuotationCriteriaMenu(quotation);
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Prompt for the <see cref="Customer"/> First Name
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerFirstName(Quotation quotation)
		{
			_logger.LogInformation("Add Customer First Name Menu Loaded");
			
			var name = AnsiConsole.Prompt(
				new TextPrompt<string>("[green]First Name:[/]"));
			
			quotation.Customer.FirstName = name;
			
			_logger.LogDebug("Quotation Property Customer FirstName set to: {menu}", name);
			
			_displayService.UpdateCustomerTableCells(1, quotation.Customer.FirstName);
			AddCustomerMenu(quotation);
		}

		/// <summary>
		/// Prompt for the <see cref="Customer"/> Last Name
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerLastName(Quotation quotation)
		{
			
			_logger.LogInformation("Add Customer Last Name Menu Loaded");
			
			var name = AnsiConsole.Prompt(
				new TextPrompt<string>("[green]Surname:[/]"));
			
			quotation.Customer.Surname = name;
			
			_logger.LogDebug("Quotation Property Customer Surname set to: {menu}", name);
			
			_displayService.UpdateCustomerTableCells(2, quotation.Customer.Surname);
			AddCustomerMenu(quotation);
		}

		/// <summary>
		/// Prompt for the <see cref="Customer"/> Street
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerStreet(Quotation quotation)
		{
			
			_logger.LogInformation("Add Customer Street Menu Loaded");
			
			var street = AnsiConsole.Prompt(
				new TextPrompt<string>("[green]Address Street:[/]"));
			
			quotation.Customer.Street = street;
			
			_logger.LogDebug("Quotation Property Customer Street set to: {menu}", street);
			
			_displayService.UpdateCustomerTableCells(4, quotation.Customer.Street);
			AddCustomerMenu(quotation);
		}

		/// <summary>
		/// Prompt for the <see cref="Customer"/> County
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerCounty(Quotation quotation)
		{
			
			_logger.LogInformation("Add Customer County Menu Loaded");
			
			var county = AnsiConsole.Prompt(
				new TextPrompt<string>("[green]Address County:[/]"));
			
			quotation.Customer.County = county;
			
			_logger.LogDebug("Quotation Property Customer County set to: {menu}", county);
			
			_displayService.UpdateCustomerTableCells(5, quotation.Customer.County);
			AddCustomerMenu(quotation);
		}

		/// <summary>
		/// Prompt for the <see cref="Customer"/> EirCode
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerEirCode(Quotation quotation)
		{
			
			_logger.LogInformation("Add Customer Eircode Menu Loaded");
			
			var eircode = AnsiConsole.Prompt(
				new TextPrompt<string>("[green]Address EirCode:[/]"));
			
			quotation.Customer.Eircode = eircode;
			
			_logger.LogDebug("Quotation Property Customer Eircode set to: {menu}", eircode);
			
			_displayService.UpdateCustomerTableCells(6, quotation.Customer.Eircode);
			AddCustomerMenu(quotation);
		}

		/// <summary>
		/// Prompt for the <see cref="Customer"/> Phone Number
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void AddCustomerPhoneNumber(Quotation quotation)
		{
			
			_logger.LogInformation("Add Customer Phone Number Menu Loaded");
			
			var phoneNumber = AnsiConsole.Prompt(
				new TextPrompt<string>("[green]Phone Number:[/]"));
			
			quotation.Customer.PhoneNumber = phoneNumber;
			
			_logger.LogDebug("Quotation Property Customer PhoneNumber set to: {menu}", phoneNumber);
			
			_displayService.UpdateCustomerTableCells(8, quotation.Customer.PhoneNumber);
			AddCustomerMenu(quotation);
		}

		/// <summary>
		/// Main Menu for selecting what criteria to search for a <see cref="Quotation"/> by
		/// </summary>
		public void SearchQuotationMenu()
		{
			
			_logger.LogInformation("Search Quotation Menu Loaded");
			
			_displayService.MenuFiglet();
			_displayService.RuleRedLeft("Search for Quotation");
			
			const string prompt = "What criteria would you like to search by?";
			var menuItems = new [] { "Search By ID", "Main Menu" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			switch (menuOption)
			{
				case "Search By ID":
					SearchQuotationMenuId();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// The Search Quotation Menu for searching by <see cref="Quotation"/> ID
		/// </summary>
		public async void SearchQuotationMenuId()
		{
			
			_logger.LogInformation("Search Quotation Menu ID Loaded");
			
			_displayService.MenuFiglet();
			_displayService.RuleRedLeft("Search for Quotation - ID");
			
			var id = AnsiConsole.Prompt(
				new TextPrompt<int>("[green]Quotation ID:[/]"));
			
			_displayService.LoadingPrompt();
			var quotation = await _quotationService.GetQuotationAsync(id);
			
			if (quotation == null)
			{
				
				_logger.LogError("Quotation ID not found");
				
				if (!AnsiConsole.Confirm("[red]Quotation not found - Try another ID?[/]"))
				{
					SearchQuotationMenu();
				}

				SearchPolicyMenuId();
			}
			else
			{
				_displayService.LoadingPrompt();
				ConvertQuotationToPolicy(quotation);
			}
			
		}

		/// <summary>
		/// Main Menu for selecting what criteria to search for a <see cref="Quotation"/> that represents a policy by
		/// </summary>
		public void SearchPolicyMenu()
		{
			
			_logger.LogInformation("Search Policy Menu Loaded");
			
			_displayService.MenuFiglet();
			_displayService.RuleRedLeft("Search for Policy");

			const string prompt = "What criteria would you like to search for:";
			var menuItems = new [] { "Search By ID", "Main Menu" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			switch (menuOption)
			{
				case "Search By ID":
					SearchPolicyMenuId();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Search Policy Menu for searching by <see cref="Quotation"/> ID
		/// </summary>
		public async void SearchPolicyMenuId()
		{
			
			_logger.LogInformation("Search Policy Menu ID Loaded");
			
			_displayService.MenuFiglet();
			_displayService.RuleRedLeft("Search for Policy - ID");
			
			var id = AnsiConsole.Prompt(
				new TextPrompt<int>("[green]Policy ID:[/]"));
			
			_displayService.LoadingPrompt();
			var policy = await _quotationService.GetPolicyAsync(id);

			if (policy == null)
			{
				
				_logger.LogError("Policy ID not found");
				
				if (!AnsiConsole.Confirm("[red]Policy not found - Try another ID?[/]"))
				{
					SearchPolicyMenu();
				}

				SearchPolicyMenuId();
			}
			
			_displayService.RenderQuotationTable(policy);
			
			_logger.LogInformation("Policy Loaded.. Menu Loaded");

			const string prompt = "What criteria would you like to search for:";
			var menuItems = new [] { "Search By ID", "Generate Policy Certificate", "Main Menu" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			switch (menuOption)
			{
				case "Search By ID":
					SearchPolicyMenuId();
					break;
				case "Generate Policy Certificate":
					_displayService.LoadingPrompt();
					_fileService.CreateCertificate(policy);
					DisplayMainMenu();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Menu to Convert a <see cref="Quotation"/>'s IsPolicy to true and extends the Valid Until date
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void ConvertQuotationToPolicy(Quotation quotation)
		{
			
			_logger.LogInformation("Convert Quotation to Policy Loaded");
			
			_displayService.RenderQuotationTable(quotation);
			_displayService.RuleRedLeft("Convert Quotation to Policy");
			
			const string prompt = "Would you like to convert this quotation to a policy?";
			var menuItems = new [] { "Convert to Policy", "Main Menu" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			switch (menuOption)
			{
				case "Convert to Policy":
					_displayService.LoadingPrompt();
					var updatedPolicy = _quotationService.UpdateQuotationAsync(quotation).Result;
					GeneratePolicyCertificate(updatedPolicy);
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Menu to Generate a new PDF Certificate for a Policy
		/// </summary>
		/// <param name="quotation">The current instantiated <see cref="Quotation"/> object</param>
		public void GeneratePolicyCertificate(Quotation quotation)
		{
			
			_logger.LogInformation("Generate Policy Certificate Loaded");
			
			_displayService.RenderQuotationTable(quotation);
			_displayService.RuleRedLeft("Create Policy Certificate");

			const string prompt = "Would you like to generate a certificate for the policy?";
			var menuItems = new [] { "Generate Certificate", "Main Menu" };
			
			var menuOption = DisplayMenuPrompt(prompt, menuItems);

			switch (menuOption)
			{
				case "Generate Certificate":
					_displayService.LoadingPrompt();
					_fileService.CreateCertificate(quotation);
					DisplayMainMenu();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// The Main Menu for Navigation of reports generation
		/// </summary>
		public void ReportsMenu()
		{
			
			_logger.LogInformation("Reports Menu Loaded");
			
			_displayService.MenuFiglet();
			_displayService.RuleRedLeft("Generate Reports");

			const string prompt = "Please select a menu option:";
			var menuItems = new [] { "Expiring Policies", "Expiring Quotations", "Main Menu" };
			
			var menuItem = DisplayMenuPrompt(prompt, menuItems);

			switch (menuItem)
			{
				case "Expiring Policies":
					SearchExpiringPoliciesMenu();
					break;
				case "Expiring Quotations":
					SearchExpiringQuotationsMenu();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Menu for handling the search of Expiring Quotations
		/// </summary>
		public void SearchExpiringQuotationsMenu()
		{
			
			_logger.LogInformation("Search Expiring Quotations Menu Loaded");
			
			_displayService.LoadingPrompt();
			var quotations = _quotationService.GetExpiringQuotationsAsync().Result;
			_displayService.RenderExpiringTable(quotations);
			_displayService.RuleRedLeft("Quotations Expiring (Next 3 Days)");

			const string prompt = "Please select a menu option";
			var menuItems = new [] { "Save Report", "Back", "Main Menu" };
			
			var menuItem = DisplayMenuPrompt(prompt, menuItems);

			switch (menuItem)
			{
				case "Save Report":
					_fileService.CreateExpiringSoonReport(quotations, "Quotations Expiring (Next 3 Days)");
					DisplayMainMenu();
					break;
				case "Back":
					ReportsMenu();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Menu for handling the search of Expiring Policies
		/// </summary>
		public void SearchExpiringPoliciesMenu()
		{
			
			_logger.LogInformation("Search Expiring Policies Menu Loaded");
			
			_displayService.LoadingPrompt();
			var policies = _quotationService.GetExpiringPoliciesAsync().Result;
			_displayService.RenderExpiringTable(policies);
			_displayService.RuleRedLeft("Policies Expiring (Next 30 Days)");

			const string prompt = "Please select a menu option";
			var menuItems = new [] { "Save Report", "Back", "Main Menu" };
			var menuItem = DisplayMenuPrompt(prompt, menuItems);

			switch (menuItem)
			{
				case "Save Report":
					_fileService.CreateExpiringSoonReport(policies, "Policies Expiring (Next 30 Days)");
					DisplayMainMenu();
					break;
				case "Back":
					ReportsMenu();
					break;
				case "Main Menu":
					DisplayMainMenu();
					break;
			}
		}

		/// <summary>
		/// Displays a prompt for the user to select a menu option
		/// </summary>
		/// <param name="prompt">The prompt to be shown to the user in the menu</param>
		/// <param name="menuOptions">An array of menu options.</param>
		/// <returns>The selected menu option.</returns>
		public string DisplayMenuPrompt(string prompt, string[] menuOptions)
		{
			var menuSelection = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title($"{prompt}:")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
					.AddChoices(menuOptions));

			return menuSelection;
		}
	}
}
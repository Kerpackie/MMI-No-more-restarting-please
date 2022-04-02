using MMI.Models;

namespace MMI.Services.MenuService
{
	public interface IMenuService
	{
		// Menu Figlet
		//void MenuFiglet();
		
		// Display Menu
		public void DisplayMainMenu();
		//Task DisplayCustomerSearchMenu();
		
		// Routing
		public void MainMenuRouting(string menuOption);
		//void GenerateQuotationRouting(string menuOption);

		
		// Generate Quotation
		public void SelectQuotationCriteriaMenu(Quotation quotation);
		public void CriteriaSelectionMenuGender(Quotation quotation);
		public void CriteriaSelectionMenuAge(Quotation quotation);
		public void CriteriaSelectionMenuCounty(Quotation quotation);
		public void CriteriaSelectionMenuMake(Quotation quotation);
		public void CriteriaSelectionMenuEmissionsCategory(Quotation quotation);
		public void CriteriaSelectionMenuInsuranceCategory(Quotation quotation);
		public void SaveQuotationToDatabase(Quotation quotation);
		
		// Add Customer
		public void AddCustomerMenu(Quotation quotation);
		public void AddCustomerFirstName(Quotation quotation);
		public void AddCustomerLastName(Quotation quotation);
		public void AddCustomerStreet(Quotation quotation);
		public void AddCustomerCounty(Quotation quotation);
		public void AddCustomerEirCode(Quotation quotation);
		public void AddCustomerPhoneNumber(Quotation quotation);
		
		// Search Quotation
		public void SearchQuotationMenu();
		public void SearchQuotationMenuId();
		
		// Search Policy
		public void SearchPolicyMenu();
		public void SearchPolicyMenuId();
		
		// Policy Generation
		public void ConvertQuotationToPolicy(Quotation quotation);
		public void GeneratePolicyCertificate(Quotation quotation);
		
		// Reports
		public void ReportsMenu();
		public void SearchExpiringQuotationsMenu();
		public void SearchExpiringPoliciesMenu();
		
		// Menu Prompts
		public string DisplayMenuPrompt(string prompt, string[] menuOptions);
		
	}
}
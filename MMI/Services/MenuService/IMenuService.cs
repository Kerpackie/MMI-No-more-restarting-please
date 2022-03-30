using System.Threading.Tasks;

namespace MMI.Services.MenuService
{
	public interface IMenuService
	{
		// Menu Figlet
		//void MenuFiglet();
		
		// Display Menu
		void DisplayMainMenu();
		void DisplayGenerateQuotation();
		//Task DisplayCustomerSearchMenu();
		
		// Routing
		void MainMenuRouting(string menuOption);
		void GenerateQuotationRouting(string menuOption);

		
		// Generate Quotation
		void SelectQuotationCriteriaMenu();
		void CriteriaSelectionMenuGender();
		void CriteriaSelectionMenuAge();
		void CriteriaSelectionMenuCounty();
		void CriteriaSelectionMenuMake();
		void CriteriaSelectionMenuEmissionsCategory();
		void CriteriaSelectionMenuInsuranceCategory();
		
		


	}
}
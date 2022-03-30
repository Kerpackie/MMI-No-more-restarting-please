namespace MMI.Services.DisplayService
{
	public interface IDisplayService
	{
		void MenuFiglet();
		void TableInit();
		void RenderQuotationCriteriaTable();
		void QuotationTableClear();
		void UpdateQuotationTableCells();
	}
}
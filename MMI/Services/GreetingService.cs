using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MMI.Services.DisplayService;
using MMI.Services.MenuService;

namespace MMI.Services
{
	/// <summary>
	/// Handles running the application.
	/// </summary>
	public class GreetingService : IGreetingService
	{
		private readonly IMenuService _menuService;
		private readonly IDisplayService _displayService;

		public GreetingService(IMenuService menuService, IDisplayService displayService)
		{
			_menuService = menuService;
			_displayService = displayService;
		}

		/// <summary>
		/// Runs on startup - Is primary entry point outside of Main();
		/// </summary>
		public void Run()
		{
			_displayService.TableInit(Persistent.QuotationTable);
			_displayService.CustomerTable(Persistent.CustomerTable);
			_menuService.DisplayMainMenu();
		}
	}
}
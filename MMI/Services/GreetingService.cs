using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MMI.Services.DisplayService;
using MMI.Services.MenuService;
using Spectre.Console;

namespace MMI.Services
{
	public class GreetingService : IGreetingService
	{
		private readonly ILogger<GreetingService> _log;
		private readonly IConfiguration _config;
		private readonly IMenuService _menuService;
		private readonly IDisplayService _displayService;

		public GreetingService(ILogger<GreetingService> log, IConfiguration config, IMenuService menuService, IDisplayService displayService)
		{
			_log = log;
			_config = config;
			_menuService = menuService;
			_displayService = displayService;
		}

		public void Run()
		{
			// for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
			// {
			// 	_log.LogError("Run number {runNumber}", i);
			// }

			var greeting = true;

			Console.WriteLine(Persistent.CurrentQuotation.Id);
			_displayService.TableInit();
			//AnsiConsole.Write(Persistent.QuotationTable);
			_menuService.DisplayMainMenu();
		}
	}
}
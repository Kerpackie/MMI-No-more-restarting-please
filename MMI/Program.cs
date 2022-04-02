using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMI.Services;
using MMI.Services.DisplayService;
using MMI.Services.FileService;
using MMI.Services.MenuService;
using MMI.Services.QuotationService;
using Serilog;

namespace MMI
{
	/// <summary>
	/// The program class, all standard stuff is here.
	/// </summary>
	class Program
	{
		static async Task Main(string[] args)
		{
			// Config builder
			var builder = new ConfigurationBuilder();
			BuildConfig(builder);

			// Logging initializer
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Build())
				.Enrich.FromLogContext()
				.WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
				//.WriteTo.Console()
				.CreateLogger();

			Log.Logger.Information("--- MMI started ---");

			// Host builder and services
			var host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					services.AddTransient<IGreetingService, GreetingService>();
					services.AddSingleton<IFileService, FileService>();
					services.AddSingleton<IMenuService, MenuService>();
					services.AddSingleton<IDisplayService, DisplayService>();
					services.AddSingleton<IQuotationService, QuotationService>();
				})
				.UseSerilog()
				.Build();

			var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
			svc.Run();
		}

		// Config builder
		static void BuildConfig(IConfigurationBuilder builder)
		{
			builder.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
				.AddEnvironmentVariables();
		}
	}
}
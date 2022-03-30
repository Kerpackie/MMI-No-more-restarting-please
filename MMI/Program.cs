using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMI.Data;
using MMI.Services;
using MMI.Services.DisplayService;
using MMI.Services.MenuService;
using Serilog;

namespace MMI
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder();
			BuildConfig(builder);

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Build())
				.Enrich.FromLogContext()
				.WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
				//.WriteTo.Console()
				.CreateLogger();

			Log.Logger.Information("Application Starting");

			var host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					// Add EF services to the services container
					//services.AddDbContext<DataContext>(options =>
						//options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));
						
					services.AddTransient<IGreetingService, GreetingService>();
					services.AddSingleton<IMenuService, MenuService>();
					services.AddSingleton<IDisplayService, DisplayService>();
				})
				.UseSerilog()
				.Build();

			var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
			svc.Run();
		}

		static void BuildConfig(IConfigurationBuilder builder)
		{
			builder.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
				.AddEnvironmentVariables();
		}
	}
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ThijsTijsma.WritableConfiguration.Json
{
	public static class ServiceCollectionExtensions
	{
		public static void AddWritableJsonConfiguration<T>(this IServiceCollection services, IConfigurationSection configurationSection, string fileName = "appsettings.json") where T : class, new()
		{
			services.Configure<T>(configurationSection);

			services.AddTransient<IWritableOptions<T>>(provider =>
			{
				var hostingEnvironment = provider.GetService<IHostingEnvironment>();
				var optionsMonitor = provider.GetService<IOptionsMonitor<T>>();

				return new WritableOptions<T>(hostingEnvironment, optionsMonitor, configurationSection.Key, fileName);
			});
		}
	}
}

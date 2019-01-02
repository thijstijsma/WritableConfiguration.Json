using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace ThijsTijsma.WritableConfiguration.Json
{
	public static class ServiceCollectionExtensions
	{
		public static void AddWritableJsonConfiguration<T>(this IServiceCollection services, IFileProvider fileProvider, IConfigurationSection configurationSection, string fileName = "appsettings.json") where T : class, new()
		{
			services.Configure<T>(configurationSection);

			services.AddTransient<IWritableOptions<T>>(provider =>
			{
				var optionsMonitor = provider.GetService<IOptionsMonitor<T>>();

				return new WritableOptions<T>(fileProvider, optionsMonitor, configurationSection.Key, fileName);
			});
		}
	}
}

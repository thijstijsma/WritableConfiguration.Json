using Microsoft.Extensions.Options;

namespace ThijsTijsma.WritableConfiguration.Json
{
	public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
	{
		void Write();
	}
}
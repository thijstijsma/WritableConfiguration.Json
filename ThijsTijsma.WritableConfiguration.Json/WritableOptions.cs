using System.IO;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThijsTijsma.WritableConfiguration.Json
{
	public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
	{
		private readonly IFileProvider _fileProvider;
		private readonly IOptionsMonitor<T> _optionsMonitor;
		private readonly string _sectionKey;
		private readonly string _fileName;

		public T Value => _optionsMonitor.CurrentValue;
		public T Get(string name) => _optionsMonitor.Get(name);

		public WritableOptions(IFileProvider fileProvider, IOptionsMonitor<T> optionsMonitor, string sectionKey, string fileName)
		{
			_fileProvider = fileProvider;
			_optionsMonitor = optionsMonitor;
			_sectionKey = sectionKey;
			_fileName = fileName;
		}

		public void Write()
		{
			var fileInfo = _fileProvider.GetFileInfo(_fileName);
			var physicalPath = fileInfo.PhysicalPath;

			JObject jObject;

			if (File.Exists(physicalPath))
			{
				jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
			}
			else
			{
				jObject = new JObject();
			}

			jObject[_sectionKey] = JObject.Parse(JsonConvert.SerializeObject(Value));
			File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
		}
	}
}
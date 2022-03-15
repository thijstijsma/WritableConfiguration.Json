# WritableConfiguration.Json

Store settings that are configured at runtime.

You can specify the name of the configuration section and the file you want to use (default is "appsettings.json").
Note that when you use a custom file you have to add that manually to the `ConfigurationBuilder`.

## Installation

Reference the [ThijsTijsma.WritableConfiguration.Json](https://www.nuget.org/packages/ThijsTijsma.WritableConfiguration.Json/) NuGet package.

## Usage
~~~~csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWritableJsonConfiguration<ExampleSettings>(configuration.GetSection("Example"), "example.json");
    }
}

public class SetupController : Controller
{
    private readonly IWritableOptions<ExampleSettings> _exampleSettings;

    public SetupController(IWritableOptions<ExampleSettings> exampleSettings)
    {
	    _exampleSettings = exampleSettings;
    });

    [HttpPost]
    public IActionResult SaveConnectionString([FromBody] string connectionString)
    {
        _exampleSettings.Value.ConnectionString = connectionString;
        _exampleSettings.Write();

        return View();
    }
}
~~~~
### Output

~~~~json
{
  "Example":
  {
    "ConnectionString": "Server=localhost"
  }
}
~~~~

## Credits

Inspired by StackOverflow answers from [Matze](https://stackoverflow.com/a/42705862/) and [ceferrari](https://stackoverflow.com/a/45986656/).

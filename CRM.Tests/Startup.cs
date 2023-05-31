using Microsoft.Extensions.Configuration;

namespace CRM.Tests
{
    public class Startup
    {
        static IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();


    }
}

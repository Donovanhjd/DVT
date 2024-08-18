using DVT.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a service collection
            var serviceCollection = new ServiceCollection();

            // Configure services
            Startup.ConfigureServices(serviceCollection);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
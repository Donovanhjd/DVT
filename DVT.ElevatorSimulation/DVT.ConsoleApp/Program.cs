using DVT.Application.Interfaces;
using DVT.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var serviceCollection = new ServiceCollection();

            Startup.ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var controlService = serviceProvider.GetService<IControlService>();
            await controlService?.StartSimulation()!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
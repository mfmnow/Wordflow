using MFM.WordFlow.Domain.Contracts;
using MFM.WordFlow.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MFM.Wordflow.App
{
    public class ServiceCollectionInitializer
    {
        public static ServiceProvider ConfigureServices()
        {
            //setup DI
            return new ServiceCollection()
                .AddLogging()
                .AddScoped<IModulesLoader, ModulesLoader>()
                .AddScoped<IModulesLoaderEngine, ModulesLoaderEngine>()
                .AddScoped<IModulesLoaderVisualizer, ModulesLoaderVisualizer>()
                .BuildServiceProvider();
        }
    }
}

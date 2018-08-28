using MFM.WordFlow.Domain.Contracts;
using MFM.WordFlow.Domain.Contracts.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MFM.Wordflow.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //setup DI
            var serviceProvider = ServiceCollectionInitializer.ConfigureServices();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            try
            {
                var json = File.ReadAllText("Modules.json");
                var results = JsonConvert.DeserializeObject<List<Module>>(json);
                var modulesLoader = serviceProvider.GetService<IModulesLoader>();
                var resultString = await modulesLoader.GetLoadingDiagram(results);
                Console.Write(resultString);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message,ex);
            }
            Console.ReadLine();
        }
    }
}

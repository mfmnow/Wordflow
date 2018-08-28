using MFM.WordFlow.Domain.Contracts;
using MFM.WordFlow.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFM.WordFlow.Domain.Services
{
    public class ModulesLoader : IModulesLoader
    {
        private readonly IModulesLoaderEngine _modulesLoaderEngine;
        private readonly IModulesLoaderVisualizer _modulesLoaderVisualizer;

        public ModulesLoader(IModulesLoaderEngine modulesLoaderEngine, IModulesLoaderVisualizer modulesLoaderVisualizer)
        {
            _modulesLoaderVisualizer = modulesLoaderVisualizer;
            _modulesLoaderEngine = modulesLoaderEngine;
        }

        public async Task<string> GetLoadingDiagram(List<Module> modules)
        {
            try
            {
                if (await _modulesLoaderEngine.SelfDependencyFound(modules))
                {
                    throw new Exception("SelfDependencyFound returned true. A module cannot depend on itself");
                }
                var orderedModules = await _modulesLoaderEngine.GetLoadingOrder(modules);
                return await _modulesLoaderVisualizer.GetLoadingDiagram(orderedModules);
            }
            catch (Exception ex)
            {
                //Log error
                throw ex;
            }       
        }        
    }
}

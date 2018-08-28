using MFM.WordFlow.Domain.Contracts;
using MFM.WordFlow.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFM.WordFlow.Domain.Services
{
    public class ModulesLoaderEngine : IModulesLoaderEngine
    {
        private List<Module> _loadedModules;

        public async Task<List<Module>> GetLoadingOrder(List<Module> modules)
        {
            try
            {
                _loadedModules = new List<Module>();
                foreach (Module iModule in modules.Where(m => m.DependenciesIds == null || m.DependenciesIds.Length == 0))
                {
                    await LoadModuleAndDependants(iModule, modules);
                }
                return _loadedModules;
            }
            catch (Exception ex)
            {
                //Log error
                throw ex;
            }
        }

        public async Task LoadModuleAndDependants(Module module, List<Module> modules)
        {
            if (!module.Loaded && await AllDependenciesLoaded(module, modules))
            {
                _loadedModules.Add(module);
                module.Loaded = true;
                foreach (Module iModule in modules.Where(m => m.DependenciesIds != null && m.DependenciesIds.Contains(module.Id)))
                {
                    await LoadModuleAndDependants(iModule, modules);
                }
            }
        }

        public Task<bool> AllDependenciesLoaded(Module module, List<Module> modules)
        {
            foreach (Module iModule in modules.Where(m => module.DependenciesIds != null && module.DependenciesIds.Contains(m.Id)))
            {
                if (!iModule.Loaded)
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(true);
        }

        public Task<bool> SelfDependencyFound(List<Module> modules)
        {
            return Task.FromResult(modules.Where(m => m.DependenciesIds != null && m.DependenciesIds.Contains(m.Id)).Count() > 0);
        }
    }
}

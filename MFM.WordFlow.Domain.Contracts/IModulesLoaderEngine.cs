using MFM.WordFlow.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFM.WordFlow.Domain.Contracts
{
    public interface IModulesLoaderEngine
    {
        Task<List<Module>> GetLoadingOrder(List<Module> modules);
        Task LoadModuleAndDependants(Module module, List<Module> modules);
        Task<bool> AllDependenciesLoaded(Module module, List<Module> modules);
        Task<bool> SelfDependencyFound(List<Module> modules);
    }
}

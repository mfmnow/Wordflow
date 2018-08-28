using MFM.WordFlow.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFM.WordFlow.Domain.Contracts
{
    public interface IModulesLoaderVisualizer
    {
        Task<string> GetLoadingDiagram(List<Module> modules);
    }
}

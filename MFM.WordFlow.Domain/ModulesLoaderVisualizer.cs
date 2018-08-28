using MFM.WordFlow.Domain.Contracts;
using MFM.WordFlow.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFM.WordFlow.Domain.Services
{
    public class ModulesLoaderVisualizer : IModulesLoaderVisualizer
    {
        public Task<string> GetLoadingDiagram(List<Module> modules)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Module module in modules)
            {
                stringBuilder.Append($"Module {module.Id} Loaded.");
                if (module.DependenciesIds != null && module.DependenciesIds.Length > 0)
                {
                    stringBuilder.Append($" Dependencies ({string.Join(",", module.DependenciesIds.Select(x => x.ToString()).ToArray())}).");
                }
                stringBuilder.AppendLine();
            }
            return Task.FromResult(stringBuilder.ToString());
        }
    }
}

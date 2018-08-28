using MFM.WordFlow.Domain.Contracts.Models;
using MFM.WordFlow.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MFM.WordFlow.Domain.UnitTests
{
    [TestClass]
    public class ModulesLoaderTester
    {
        [TestMethod]
        public async Task GetLoadingDiagram_Should_Not_Throw_An_Exception_On_Success()
        {
            var json = File.ReadAllText("mockedModules.json");
            var results = JsonConvert.DeserializeObject<List<Module>>(json);
            ModulesLoader modulesLoader = new ModulesLoader(new ModulesLoaderEngine(), new ModulesLoaderVisualizer());
            var resultString = await modulesLoader.GetLoadingDiagram(results);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetLoadingDiagram_Should_Throw_ArgumentNullException_If_Modules_Is_Null()
        {
            ModulesLoader modulesLoader = new ModulesLoader(new ModulesLoaderEngine(), new ModulesLoaderVisualizer());
            await modulesLoader.GetLoadingDiagram(null);
        }
    }
}

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
    public class ModulesLoaderEngineTester
    {
        private List<Module> _modules;
        
        private void ReloadResults(string fileName= "mockedModules.json")
        {
            var json = File.ReadAllText(fileName);
            _modules = JsonConvert.DeserializeObject<List<Module>>(json);
        }

        [TestMethod]
        public async Task GetLoadingOrder_Should_Return_Objects()
        {
            ReloadResults();
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
           var resultArray = await modulesLoaderEngine.GetLoadingOrder(_modules);
           Assert.IsTrue(resultArray.Count == 4);
           Assert.IsTrue(resultArray[0].Id == 1);
           Assert.IsTrue(resultArray[1].Id == 2);
           Assert.IsTrue(resultArray[2].Id == 3);
           Assert.IsTrue(resultArray[3].Id == 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetLoadingOrder_Should_Throw_ArgumentNullException_If_Modules_Is_Null()
        {
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            await modulesLoaderEngine.GetLoadingOrder(null);
        }

        [TestMethod]
        public async Task LoadModuleAndDependants_Execute_Successfully_If_Right_Params_Passed()
        {
            ReloadResults();
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            await modulesLoaderEngine.LoadModuleAndDependants(_modules[0], _modules);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LoadModuleAndDependants_Should_Throw_ArgumentNullException_If_Modules_Is_Null()
        {
            ReloadResults();
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            await modulesLoaderEngine.LoadModuleAndDependants(_modules[0], null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task LoadModuleAndDependants_Should_Throw_ArgumentNullException_If_Module_Is_Null()
        {
            ReloadResults();
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            await modulesLoaderEngine.LoadModuleAndDependants(null, _modules);
        }

        [TestMethod]
        public async Task AllDependenciesLoaded_Should_Return_True_If_No_Dependencies()
        {
            ReloadResults();
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            var result = await modulesLoaderEngine.AllDependenciesLoaded(_modules[1], _modules);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AllDependenciesLoaded_Should_Return_False_If_One_Dependency_Is_Not_Loaded()
        {
            ReloadResults();
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            var result = await modulesLoaderEngine.AllDependenciesLoaded(_modules[2], _modules);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task SelfDependencyFound_Should_Return_Right_Count()
        {
            ReloadResults("mockedModulesSelfDependency.json");
            ModulesLoaderEngine modulesLoaderEngine = new ModulesLoaderEngine();
            var result = await modulesLoaderEngine.SelfDependencyFound(_modules);
            Assert.IsTrue(result);
        }
    }
}

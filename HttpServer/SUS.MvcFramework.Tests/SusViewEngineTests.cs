using HttpServer.MvcFramework.ViewEngine;
using HttpServer.MvcFramework.ViewEngine.Contracts;
using NUnit.Framework;
using SUS.MvcFramework.Tests.TestViewModels;
using System;
using System.IO;

namespace SUS.MvcFramework.Tests
{
    public class SusViewEngineTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("CleanHtml")]
        [TestCase("Foreach")]
        [TestCase("IfFor")]
        [TestCase("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            TestViewModel viewModel = new TestViewModel
            {
                Name = "Pesho",
                Email = "pesho@pesho.de",
                DateOfBirth = new DateTime(1985, 5, 16),
                SomeNumber = 121
            };

            IViewEngine viewEngine = new SusViewEngine();

            string testFilePath = "ViewTests/"+ fileName + "-Test.html";

            var view = File.ReadAllText(testFilePath);
            var result = viewEngine.GetHtml(view,viewModel);

            string resultFilePath = "ViewTests/" + fileName + "-Result.html";
            var expectedResult = File.ReadAllText(resultFilePath);

            Assert.AreEqual(expectedResult,result);
            
        }
    }
}
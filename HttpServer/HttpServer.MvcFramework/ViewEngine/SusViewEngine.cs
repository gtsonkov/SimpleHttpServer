using HttpServer.MvcFramework.ViewEngine.Common.ConstantData;
using HttpServer.MvcFramework.ViewEngine.Contracts;
using HttpServer.MvcFramework.ViewEngine.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HttpServer.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        private const string CodeTemplatePart1Path = "./ViewEngine/Common/CodeTemplate-Part1.txt";
        private const string CodeTemplatePart2Path = "./ViewEngine/Common/CodeTemplate-Part2.txt";

        public string GetHtml(string templateCode, object viewModel)
        {
            string csharpCode = GenerateCsharpCode(templateCode);
            IView execupableOject = GenerateExecutableCode(csharpCode, viewModel);
            var html = execupableOject.GetHtml(viewModel);

            return html;
        }

        /// <summary>
        /// Collect all nessesery usings from CsharpLibs class (as constants)
        /// </summary>
        /// <param name="type">Type of CsharpLib class</param>
        /// <returns></returns>
        private string GetUsings(Type type)
        {
            StringBuilder sb = new StringBuilder();

            var usings = type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral);

            foreach (var item in usings)
            {
                sb.AppendLine((string)item.GetValue(null));
            }

            return sb.ToString();
        }

        private string GenerateCsharpCode(string templateCode)
        {
            string usings = GetUsings(typeof(CsharpLibs));

            string csharpCode = usings 
                + Environment.NewLine
                + File.ReadAllText(
                  CodeTemplatePart1Path 
                + Environment.NewLine
                + GetMethodBody(templateCode) 
                + Environment.NewLine
                + CodeTemplatePart2Path);

            return csharpCode;
        }

        private IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            var compileResult = CSharpCompilation.Create(CompilationConstants.CompilationAsamblyName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)) //What kind of projekt will be created, in this case will be created DLL
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location)) //.Net Base lilbrary will be refered to this project
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location)); // The HttpServer.MvcFramework Lib will be refered
            
            if (viewModel != null)
            {
                compileResult = compileResult.AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            //We shoud also add ALL .NetCore Standad Libs!!!

            var standadDotNetCoreLibs = Assembly.Load(
                new AssemblyName(CompilationConstants.DotNetSatndardLibName))
                .GetReferencedAssemblies(); //Get all .NetCore Standard Libs

            foreach (var lib in standadDotNetCoreLibs)
            {
                compileResult = compileResult.AddReferences(
                    MetadataReference
                    .CreateFromFile(Assembly.Load(lib)
                    .Location));
            }

            compileResult = compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));

            using (MemoryStream memStream = new MemoryStream())
            {
               var compilationResult = compileResult.Emit(memStream);

                if (!compilationResult.Success)
                {
                    var errorList = compilationResult
                        .Diagnostics
                        .Where(x => x.Severity == DiagnosticSeverity.Error)
                        .Select(x => x.GetMessage())
                        .ToList();

                    return new ErrorView(errorList, csharpCode);
                }

                memStream.Seek(0, SeekOrigin.Begin); //Read this stream from the begin

                var byteAssembly = memStream.ToArray();

                var assembly = Assembly.Load(byteAssembly);
                var viewType = assembly.GetType("ViewNamespace.ViewClass");

                var currIstance = Activator.CreateInstance(viewType);

                return currIstance as IView;
            }
        }

        private string GetMethodBody(string templateCode) => throw new NotImplementedException();
    }
}
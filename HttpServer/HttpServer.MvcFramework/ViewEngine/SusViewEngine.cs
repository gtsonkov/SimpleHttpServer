using HttpServer.MvcFramework.ViewEngine.Common.ConstantData;
using HttpServer.MvcFramework.ViewEngine.Contracts;
using System;
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
            IView execupableOject = GenerateExecutableCode(csharpCode);
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

        private IView GenerateExecutableCode(string csharpCode) => throw new NotImplementedException();

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

        private string GetMethodBody(string templateCode) => throw new NotImplementedException();
    }
}
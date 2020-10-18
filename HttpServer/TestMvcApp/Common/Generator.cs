using System.Text;

namespace TestMvcApp.Common
{
    public static class Generator
    {
        public static string GenerateInternPath(string requestPath)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ConstantData_TestMVC.DomainPath);
            sb.Append(requestPath);
            sb.Append(ConstantData_TestMVC.HtmlFileExtension);

            return sb.ToString();
        }
    }
}
namespace HttpServer.MvcFramework.ViewEngine.Contracts
{
    public interface IViewEngine
    {
        string GetHtml(string templateCode, object viewModel);
    }
}
namespace HttpServer.MvcFramework.ViewEngine.Contracts
{
    public interface IView
    {
        string GetHtml(object ViewModel);
    }
}
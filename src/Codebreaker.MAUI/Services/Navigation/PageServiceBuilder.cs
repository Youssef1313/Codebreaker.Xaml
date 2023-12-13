namespace Codebreaker.MAUI.Services.Navigation;

internal class PageServiceBuilder
{
    public PageServiceBuilder Configure<V>()
        where V : Page =>
        Configure<V>(typeof(V).Name);

    public PageServiceBuilder Configure<V>(string key)
        where V : Page
    {
        Routing.RegisterRoute(key, typeof(V));
        return this;
    }
}
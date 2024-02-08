using CommunityToolkit.Mvvm.Messaging;

namespace Codebreaker.WPF.Helpers;

internal static class PageExtensions
{
    public static void UnregisterAllOnUnloaded(this IMessenger messenger, FrameworkElement page) =>
        messenger.UnregisterAllOnUnloaded(page, page);

    public static void UnregisterAllOnUnloaded(this IMessenger messenger, FrameworkElement page, object messageRecepient)
    {
        void UnloadedCallback(object sender, RoutedEventArgs args)
        {
            messenger.UnregisterAll(messageRecepient);
            page.Unloaded -= UnloadedCallback;
        }

        page.Unloaded += UnloadedCallback;
    }

    public static void CallOnceOnUnloaded(this FrameworkElement page, Action<object, RoutedEventArgs> action)
    {
        void Callback(object sender, RoutedEventArgs args)
        {
            action?.Invoke(sender, args);
            page.Unloaded -= Callback;
        }

        page.Unloaded += Callback;
    }
}

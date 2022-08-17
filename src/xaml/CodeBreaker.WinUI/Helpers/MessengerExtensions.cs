using CommunityToolkit.Mvvm.Messaging;

namespace CodeBreaker.WinUI.Helpers;
internal static class MessengerExtensions
{
    public static void UnregisterAllOnUnloaded(this IMessenger messenger, FrameworkElement element) =>
        messenger.UnregisterAllOnUnloaded(element, element);

    public static void UnregisterAllOnUnloaded(this IMessenger messenger, FrameworkElement element, object messageRecepient)
    {
        void UnloadedCallback(object sender, RoutedEventArgs args)
        {
            messenger.UnregisterAll(messageRecepient);
            element.Unloaded -= UnloadedCallback;
        }

        element.Unloaded += UnloadedCallback;
    }
}

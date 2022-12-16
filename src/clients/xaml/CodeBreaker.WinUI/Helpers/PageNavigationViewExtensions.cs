using CodeBreaker.WinUI.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace CodeBreaker.WinUI.Helpers;

internal static class PageNavigationViewExtensions
{
    public static void HideNavigationPaneForThisPage(this Page page)
    {
        page.CallOnceOnUnloaded((_, _) => WeakReferenceMessenger.Default.Send(new ChangeNavigationPaneVisibility(true)));
        WeakReferenceMessenger.Default.Send(new ChangeNavigationPaneVisibility(false));
    }
}

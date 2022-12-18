using CodeBreaker.WinUI.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace CodeBreaker.WinUI.Helpers;

internal static class PageNavigationViewExtensions
{
    public static void HideNavigationPaneForThisPage(this Page page)
    {
        page.CallOnceOnUnloaded((_, _) => WeakReferenceMessenger.Default.Send(new ChangeNavigationPaneVisibilityMessage(true)));
        WeakReferenceMessenger.Default.Send(new ChangeNavigationPaneVisibilityMessage(false));
    }

    public static void ChangeNavigationPaneDisplayModeForThisPage(this Page page, NavigationViewPaneDisplayMode displayMode)
    {
        NavigationViewPaneDisplayMode? prevDisplayMode = null;

        try
        {
            prevDisplayMode = WeakReferenceMessenger.Default.Send<CurrentNavigationViewPaneDisplayModeRequestMessage>();
        }
        catch (InvalidOperationException)
        {
            // No recipient for the request registered, so noone can answer
        }

        if (prevDisplayMode.HasValue)
            page.CallOnceOnUnloaded((_, _) => WeakReferenceMessenger.Default.Send(new ChangeNavigationViewPaneDisplayModeMessage(prevDisplayMode.Value)));

        WeakReferenceMessenger.Default.Send(new ChangeNavigationViewPaneDisplayModeMessage(displayMode));
    }
}

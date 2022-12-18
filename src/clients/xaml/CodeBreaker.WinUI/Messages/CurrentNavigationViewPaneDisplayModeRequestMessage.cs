using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CodeBreaker.WinUI.Messages;

internal class CurrentNavigationViewPaneDisplayModeRequestMessage : RequestMessage<NavigationViewPaneDisplayMode>
{
    public NavigationViewPaneDisplayMode CurrentDisplayMode { get; init; }
}

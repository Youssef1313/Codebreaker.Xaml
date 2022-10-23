using System.Windows.Input;

namespace CodeBreaker.WinUI.CustomAttachedProperties;

/// <summary>
/// The service class for custom attached properties for a Confirm dialog.
/// </summary>
/// <remarks>
/// The name of the class is intentionally short, because it gets used in XAML-markup.
/// </remarks>
public class Confirm : DependencyObject
{
    #region EnabledProperty
    /// <summary>
    /// The property for enabling/disabling the Primary dialog.
    /// </summary>
    public static readonly DependencyProperty EnabledProperty =
        DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(Confirm),
            new (false)
        );
    public static void SetEnabled(Button element, bool value)
    {
        element.SetValue(EnabledProperty, value);

        if (value)
            element.Click += ElementClickCallback;
        else
            element.Click -= ElementClickCallback;
    }
    public static bool GetEnabled(Button element) =>
        (bool?)element.GetValue(EnabledProperty) ?? false;
    #endregion

    #region Title
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.RegisterAttached(
            "Title",
            typeof(string),
            typeof(Confirm),
            new("Confirmation required")
        );
    public static void SetTitle(Button element, string value) =>
        element.SetValue(TitleProperty, value);
    public static string? GetTitle(Button element) =>
        (string?)element.GetValue(TitleProperty);
    #endregion

    #region Content
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.RegisterAttached(
            "Content",
            typeof(string),
            typeof(Confirm),
            new("Are you sure?")
        );
    public static void SetContent(Button element, string value) =>
        element.SetValue(ContentProperty, value);
    public static string? GetContent(Button element) =>
        (string?)element.GetValue(ContentProperty);
    #endregion

    #region PrimaryButton
    /// <summary>
    /// The text for the Primary button.
    /// </summary>
    public static readonly DependencyProperty PrimaryTextProperty =
        DependencyProperty.RegisterAttached(
            "PrimaryText",
            typeof(string),
            typeof(Confirm),
            new("OK")
        );
    public static void SetPrimaryText(Button element, string value) =>
        element.SetValue(PrimaryTextProperty, value);
    public static string? GetPrimaryText(Button element) =>
        (string?)element.GetValue(PrimaryTextProperty);

    /// <summary>
    /// The command for the Primary button.
    /// </summary>
    public static readonly DependencyProperty PrimaryCommandProperty =
        DependencyProperty.RegisterAttached(
            "PrimaryCommand",
            typeof(ICommand),
            typeof(Confirm),
            new (null)
        );
    public static void SetPrimaryCommand(Button element, ICommand value) =>
        element.SetValue(PrimaryCommandProperty, value);
    public static ICommand? GetPrimaryCommand(Button element) =>
        (ICommand?)element.GetValue(PrimaryCommandProperty);

    /// <summary>
    /// The command parameter for the Primary button.
    /// </summary>
    public static readonly DependencyProperty PrimaryCommandParameterProperty =
        DependencyProperty.RegisterAttached(
            "PrimaryCommandParameter",
            typeof(object),
            typeof(Confirm),
            new(null)
        );
    public static void SetPrimaryCommandParameter(Button element, object? value) =>
        element.SetValue(PrimaryCommandProperty, value);
    public static object? GetPrimaryCommandParameter(Button element) =>
        (object?)element.GetValue(PrimaryCommandProperty);
    #endregion

    #region SecondaryButton
    /// <summary>
    /// The text for the Secondary button.
    /// </summary>
    public static readonly DependencyProperty SecondaryTextProperty =
        DependencyProperty.RegisterAttached(
            "SecondaryText",
            typeof(string),
            typeof(Confirm),
            new(null)
        );
    public static void SetSecondaryText(Button element, string value) =>
        element.SetValue(SecondaryTextProperty, value);
    public static string? GetSecondaryText(Button element) =>
        (string?)element.GetValue(SecondaryTextProperty);

    /// <summary>
    /// The command for the Secondary button.
    /// </summary>
    public static readonly DependencyProperty SecondaryCommandProperty =
        DependencyProperty.RegisterAttached(
            "SecondaryCommand",
            typeof(ICommand),
            typeof(Confirm),
            new (null)
        );
    public static void SetSecondaryCommand(Button element, ICommand value) =>
        element.SetValue(SecondaryCommandProperty, value);
    public static ICommand? GetSecondaryCommand(Button element) =>
        (ICommand?)element.GetValue(SecondaryCommandProperty);

    /// <summary>
    /// The command parameter for the Secondary button.
    /// </summary>
    public static readonly DependencyProperty SecondaryCommandParameterProperty =
        DependencyProperty.RegisterAttached(
            "SecondaryCommandParameter",
            typeof(object),
            typeof(Confirm),
            new(null)
        );
    public static void SetSecondaryCommandParameter(Button element, object? value) =>
        element.SetValue(SecondaryCommandProperty, value);
    public static object? GetSecondaryCommandParameter(Button element) =>
        (object?)element.GetValue(SecondaryCommandProperty);
    #endregion

    #region CloseButton
    /// <summary>
    /// The text for the Close button.
    /// </summary>
    public static readonly DependencyProperty CloseTextProperty =
        DependencyProperty.RegisterAttached(
            "CloseText",
            typeof(string),
            typeof(Confirm),
            new("Cancel")
        );
    public static void SetCloseText(Button element, string value) =>
        element.SetValue(CloseTextProperty, value);
    public static string? GetCloseText(Button element) =>
        (string?)element.GetValue(CloseTextProperty);

    /// <summary>
    /// The command for the Close button.
    /// </summary>
    public static readonly DependencyProperty CloseCommandProperty =
        DependencyProperty.RegisterAttached(
            "CloseCommand",
            typeof(ICommand),
            typeof(Confirm),
            new(null)
        );
    public static void SetCloseCommand(Button element, ICommand value) =>
        element.SetValue(CloseCommandProperty, value);
    public static ICommand? GetCloseCommand(Button element) =>
        (ICommand?)element.GetValue(CloseCommandProperty);

    /// <summary>
    /// The command parameter for the Close button.
    /// </summary>
    public static readonly DependencyProperty CloseCommandParameterProperty =
        DependencyProperty.RegisterAttached(
            "CloseCommandParameter",
            typeof(object),
            typeof(Confirm),
            new(null)
        );
    public static void SetCloseCommandParameter(Button element, object? value) =>
        element.SetValue(CloseCommandProperty, value);
    public static object? GetCloseCommandParameter(Button element) =>
        (object?)element.GetValue(CloseCommandProperty);
    #endregion


    // ----


    private static void ElementClickCallback(object sender, RoutedEventArgs e) =>
        ShowDialogAsync((Button)sender);

    private static async void ShowDialogAsync(Button element) =>
        await new ContentDialog()
        {
            Title = GetTitle(element),
            Content = GetContent(element),
            DefaultButton = ContentDialogButton.Primary,
            CloseButtonText = GetCloseText(element),
            CloseButtonCommand = GetCloseCommand(element),
            CloseButtonCommandParameter = GetCloseCommandParameter(element),
            PrimaryButtonText = GetPrimaryText(element),
            PrimaryButtonCommand = GetPrimaryCommand(element),
            PrimaryButtonCommandParameter = GetPrimaryCommandParameter(element),
            SecondaryButtonText = GetSecondaryText(element),
            SecondaryButtonCommand = GetSecondaryCommand(element),
            SecondaryButtonCommandParameter = GetSecondaryCommandParameter(element),
            XamlRoot = element.XamlRoot
        }.ShowAsync();
}
